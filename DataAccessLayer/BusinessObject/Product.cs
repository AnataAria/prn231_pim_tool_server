using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.BusinessObject
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? SKU { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Range(0, 100000)]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public int CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; } = [];
        public ICollection<ProductAttribute> ProductAttributes { get; set; } = [];
        public ICollection<Asset> Assets { get; set; } = [];
    }
}
