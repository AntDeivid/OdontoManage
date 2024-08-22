using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Application.Models.Requests;
using OdontoManage.Application.Models.Responses;

namespace OdontoManage.API.Controllers;

[Route("users")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Login a user
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Logged user</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
    {
        var response = userService.Login(request);
        return Ok(response);
    }

    /// <summary>
    /// Saves a new user
    /// </summary>
    /// <param name="userCreateDto"></param>
    /// <returns>Saved user</returns>
    [HttpPost]
    [Authorize]
    public ActionResult<UserDto> Create([FromBody] UserCreateDto userCreateDto)
    {
        var response = userService.Create(userCreateDto);
        return Ok(response);
    }
    
    /// <summary>
    /// Get a user by username
    /// </summary>
    /// <param name="username">Username to search</param>
    /// <returns>User found</returns>
    [HttpGet("{username}")]
    [Authorize]
    public ActionResult<UserDto> GetByUsername(string username)
    {
        var response = userService.GetByUsername(username);
        return Ok(response);
    }
    
    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>All users</returns>
    [HttpGet]
    [Authorize]
    public ActionResult<List<UserDto>> GetAll()
    {
        var response = userService.GetAll();
        return Ok(response);
    }

    /// <summary>
    /// Update a user
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="userUpdateDto"></param>
    /// <returns>Updated user</returns>
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<UserDto> Update(Guid id, [FromBody] UserUpdateDto userUpdateDto)
    {
        var response = userService.Update(id, userUpdateDto);
        return Ok(response);
    }
    
    /// <summary>
    /// Delete a user
    /// </summary>
    /// <param name="username">Username to delete</param>
    /// <returns>No content</returns>
    [HttpDelete("{username}")]
    [Authorize]
    public ActionResult Delete(string username)
    {
        userService.Delete(username);
        return NoContent();
    }
}