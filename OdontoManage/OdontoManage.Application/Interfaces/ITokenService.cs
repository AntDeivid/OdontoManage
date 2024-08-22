using OdontoManage.Core.Models;

namespace OdontoManage.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}