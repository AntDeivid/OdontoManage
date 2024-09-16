using System.ComponentModel.DataAnnotations;

namespace OdontoManage.Core.Models;

public class Address : Base
{
    [Required]
    public string Street { get; set; }
    
    [Required]
    public string Neighborhood { get; set; } 
    
    [Required]
    public string City { get; set; }
    
    [Required]
    public string State { get; set; }
    
    [Required]
    public string ZipCode { get; set; } 
}