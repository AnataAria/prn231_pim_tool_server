using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.BusinessObject
{
    public class Asset
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string? FilePath { get; set; }

        [MaxLength(50)]
        public string? FileType { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
