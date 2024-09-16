namespace OdontoManage.Application.Models.DTOs;

public class AddressCreateDto
{
    public string Street { get; set; }
    
    public string Neighborhood { get; set; } 
    
    public string City { get; set; }
    
    public string State { get; set; }
    
    public string ZipCode { get; set; } 
}