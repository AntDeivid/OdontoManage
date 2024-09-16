namespace OdontoManage.Application.Models.DTOs;

public class ItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public uint Amount { get; set; }
    public float Price { get; set; } 
}