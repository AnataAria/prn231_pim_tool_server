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
        public PIMDatabaseContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Server=(local);uid=sa;pwd=12345;database=PIMTool;TrustServerCertificate=True";
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
                .WithMany(e => e.Projects);

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Leader)
                .WithMany(e => e.Groups)
                .HasForeignKey(g => g.LeaderId);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.GroupProject)
                .WithOne(g => g.Project)
                .HasForeignKey<Project>(p => p.GroupId);

            modelBuilder.Entity<User>().HasIndex(u => u.Username)
            .IsUnique();
        }
    }
}
