using Microsoft.EntityFrameworkCore;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;

namespace OdontoManage.Infrastructure.Repositories;

public class SchedulingRepository : GenericRepository<Scheduling>, ISchedulingRepository
{
    public SchedulingRepository(OdontoManageDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<Scheduling> GetByInterval(DateOnly start, DateOnly end)
    {
        return _dbContext.Schedules
            .Include(s => s.Patient)
            .Include(s => s.Dentist)
            .Where(x => x.Date >= start && x.Date <= end)
            .OrderBy(x => x.Date);
    }
}