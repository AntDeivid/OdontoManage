using System.ComponentModel.DataAnnotations;

namespace OdontoManage.Core.Models;

public class Base
{
    [Key] [Required] public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool Active { get; set; } = true;
}