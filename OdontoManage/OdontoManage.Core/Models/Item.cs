using System.ComponentModel.DataAnnotations;

namespace OdontoManage.Core.Models;

public class Item : Base
{
    [Required]
    public string Name { get; set; }
    [Required]
    public uint Amount { get; set; }
    [Required]
    public float Price { get; set; }
}