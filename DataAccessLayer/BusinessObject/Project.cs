using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.BusinessObject;
public class Project
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    public long? GroupId { get; set; }
    public virtual Group? GroupProject { get; set; }
    [Required]
    public long ProjectNumber { get; set; }

    [MaxLength(255)]
    [Required]
    public string? Name { get; set; }
    public virtual ICollection<Employee> Employees { get; set; } = [];

    [MaxLength(50)]
    [Required]
    public string? Customer { get; set; }

    [MaxLength(3)]
    [Required]
    public string? Status { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime? EndDate { get; set; }
    [Required]
    public long Version { get; set; }
}
