using DataAccessLayer.BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer
{
    public class PIMDatabaseContext : DbContext
    {
        private readonly IConfiguration configuration;
        private readonly string _connectionString;
        public PIMDatabaseContext() {
            _connectionString = "Server=(local);uid=sa;pwd=12345;database=PIMTool;TrustServerCertificate=True";
        }
        public PIMDatabaseContext(IConfiguration configuration, DbContextOptions<PIMDatabaseContext> options) : base(options)
        {
            this.configuration = configuration;
            _connectionString = this.configuration.GetConnectionString("DefaultConnection") ?? "Server=(local);uid=sa;pwd=12345;database=PIMTool;TrustServerCertificate=True";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Employees)
                .WithMany(e => e.Projects)
                .UsingEntity<Dictionary<string, object>>(
            j => j.HasOne<Employee>().WithMany().OnDelete(DeleteBehavior.NoAction),
            j => j.HasOne<Project>().WithMany().OnDelete(DeleteBehavior.NoAction)
        );

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Leader)
                .WithMany(e => e.Groups)
                .HasForeignKey(g => g.LeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.GroupProject)
                .WithOne(g => g.Project)
                .HasForeignKey<Project>(p => p.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasIndex(u => u.Username)
            .IsUnique();

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", PasswordHash = "$2y$10$uFZ64K25vYOZBOzoIquHGuK8Sea4Nhlllg2JW53T04Owp3r0sc0re", Role = UserRole.Admin },
                new User { Id = 2, Username = "jane", PasswordHash = "$2y$10$uFZ64K25vYOZBOzoIquHGuK8Sea4Nhlllg2JW53T04Owp3r0sc0re", Role = UserRole.Manager }
            );
        }
    }
}
