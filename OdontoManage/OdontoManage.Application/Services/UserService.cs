using System.Security.Authentication;
using AutoMapper;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Application.Models.Requests;
using OdontoManage.Application.Models.Responses;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public LoginResponse Login(LoginRequest request)
    {
        var savedUser = _userRepository.GetByUsername(request.Username);
        if (savedUser == null) throw new AuthenticationException("Invalid username or password");
        
        var salt = savedUser.Salt;
        var hashedPassword = User.HashPassword(request.Password, salt);
        
        if (savedUser.Password != hashedPassword) throw new AuthenticationException("Invalid username or password");
        
        var token = _tokenService.GenerateToken(savedUser);
        
        return new LoginResponse
        {
            Authenticated = true,
            AccessToken = token,
            Message = "User authenticated",
            User = _mapper.Map<UserDto>(savedUser)
        };
    }

    public UserDto Create(UserCreateDto userCreateDto)
    {
        var existing = _userRepository.GetByUsername(userCreateDto.Username);
        if (existing != null)
        {
            throw new Exception("Username already exists");
        }
        var user = _mapper.Map<User>(userCreateDto);
        user.SetPassword(userCreateDto.Password);
        var createdUser = _userRepository.Save(user);
        return _mapper.Map<UserDto>(createdUser);
    }

    public UserDto GetByUsername(string username)
    {
        var user = _userRepository.GetByUsername(username);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return _mapper.Map<UserDto>(user);
    }

    public List<UserDto> GetAll()
    {
        var users = _userRepository.GetAll();
        return _mapper.Map<List<UserDto>>(users);
    }

    public UserDto Update(Guid id, UserUpdateDto userDto)
    {
        var user = _userRepository.GetById(id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        user.Name = userDto.Name;
        user.Username = userDto.Username;
        user.SetPassword(userDto.Password);
        var updatedUser = _userRepository.Update(user);
        return _mapper.Map<UserDto>(updatedUser);
    }

    public void Delete(string username)
    {
        var user = _userRepository.GetByUsername(username);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        _userRepository.Delete(user.Id);
    }
}