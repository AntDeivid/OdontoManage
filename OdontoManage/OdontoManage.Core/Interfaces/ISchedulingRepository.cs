using OdontoManage.Core.Models;

namespace OdontoManage.Core.Interfaces;

public interface ISchedulingRepository : IGenericRepository<Scheduling>
{
    IQueryable<Scheduling> GetByInterval(DateOnly start, DateOnly end);
}