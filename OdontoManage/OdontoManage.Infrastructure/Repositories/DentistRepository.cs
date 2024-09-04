using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;

namespace OdontoManage.Infrastructure.Repositories;

public class DentistRepository : GenericRepository<Dentist>, IDentistRepository
{
    public DentistRepository(OdontoManageDbContext dbContext) : base(dbContext) { }
    
    
}