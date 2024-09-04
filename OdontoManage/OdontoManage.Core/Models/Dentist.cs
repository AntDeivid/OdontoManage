using System.ComponentModel.DataAnnotations;

namespace OdontoManage.Core.Models;

public class Dentist : Base
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string CPF { get; set; }
    [Required]
    public string CRO { get; set; }
    public string Specialty { get; set; }
    [Required]
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}