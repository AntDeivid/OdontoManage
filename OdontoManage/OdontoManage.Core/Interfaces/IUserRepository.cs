using OdontoManage.Core.Models;

namespace OdontoManage.Core.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    User? GetByUsername(string username);
}