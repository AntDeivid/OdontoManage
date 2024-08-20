using System.ComponentModel.DataAnnotations;

namespace OdontoManage.Core.Models;

public class Base
{
    [Key] [Required] public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool Active { get; set; } = true;
}