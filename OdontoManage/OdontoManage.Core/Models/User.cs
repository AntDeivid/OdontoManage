using System.ComponentModel.DataAnnotations;

namespace OdontoManage.Core.Models;

public class User : Base
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string Password { get; set; }
    [Required]
    public required byte[] Salt { get; set; }
}