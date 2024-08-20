using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;

namespace OdontoManage.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(OdontoManageDbContext dbContext) : base(dbContext) { }
    
    public User? GetByUsername(string username)
    {
        return (_dbContext.Users ?? throw new InvalidOperationException()).FirstOrDefault(u => u.Username == username);
    }
}