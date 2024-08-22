using OdontoManage.Application.Models.DTOs;
using OdontoManage.Application.Models.Requests;
using OdontoManage.Application.Models.Responses;

namespace OdontoManage.Application.Interfaces;

public interface IUserService
{
    LoginResponse Login(LoginRequest request);
    UserDto Create(UserCreateDto userCreateDto);
    UserDto GetByUsername(string username);
    List<UserDto> GetAll();
    UserDto Update(Guid id, UserUpdateDto userDto);
    void Delete(string username);
}