using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccessLayer.BusinessObject;
public class Group
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public virtual Employee? Leader { get; set; }

    [Required]
    public long LeaderId { get; set; }

    [Required]
    public long Version { get; set; }

    public virtual Project? Project {get; set;}
}
