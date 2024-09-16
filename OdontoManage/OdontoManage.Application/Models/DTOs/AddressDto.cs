namespace OdontoManage.Application.Models.DTOs;

public class AddressDto
{
    Guid Id { get; set; }
    
    public string Street { get; set; } 
    
    public string Neighborhood { get; set; } 
    
    public string City { get; set; }
    
    public string State { get; set; }
    
    public string zipCode { get; set; }
}