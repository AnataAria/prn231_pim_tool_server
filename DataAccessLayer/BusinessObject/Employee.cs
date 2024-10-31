using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.BusinessObject;
public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Visa { get; set; }

    [Required]
    [MaxLength(255)]
    public string? FirstName { get; set; }

    [Required]
    [MaxLength(255)]
    public string? LastName { get; set; }

    [Required]
    public DateTime BirthDay { get; set; }

    [Required]
    public long Version { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = [];
    public virtual ICollection<Group> Groups { get; set; } = [];
}
