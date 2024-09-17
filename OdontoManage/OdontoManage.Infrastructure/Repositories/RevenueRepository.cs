using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;

namespace OdontoManage.Infrastructure.Repositories;

public class RevenueRepository(OdontoManageDbContext dbContext) 
    : GenericRepository<Revenue>(dbContext), IRevenueRepository;
