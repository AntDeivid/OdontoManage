using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.Application.Models.Responses;

public class LoginResponse
{
    public bool Authenticated { get; set; }
    public string? AccessToken { get; set; }
    public string? Message { get; set; }
    public UserDto? User { get; set; }

}