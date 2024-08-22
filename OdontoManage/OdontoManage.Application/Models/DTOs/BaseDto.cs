namespace OdontoManage.Application.Models.DTOs;

public class BaseDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool Active { get; set; } = true;
}