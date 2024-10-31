using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.BusinessObject
{
    public class ProductAttribute
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int AttributeId { get; set; }
        public Attribute? Attribute { get; set; }

        [MaxLength(250)]
        public string? Value { get; set; }
    }
}
