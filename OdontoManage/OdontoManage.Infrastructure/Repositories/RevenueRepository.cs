using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;

namespace OdontoManage.Infrastructure.Repositories;

public class RevenueRepository : GenericRepository<Revenue>, IRevenueRepository
{
    public RevenueRepository(OdontoManageDbContext dbContext) : base(dbContext)
    {
    }
}