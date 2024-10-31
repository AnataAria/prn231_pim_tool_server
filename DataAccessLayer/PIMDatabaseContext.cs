using DataAccessLayer.BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Attribute = DataAccessLayer.BusinessObject.Attribute;

namespace DataAccessLayer
{
    public class PIMDatabaseContext : DbContext
    {
        private readonly IConfiguration configuration;
        public PIMDatabaseContext() { }
        public PIMDatabaseContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
