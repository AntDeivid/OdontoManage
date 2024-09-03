using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;

namespace OdontoManage.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : Base
{

    internal readonly OdontoManageDbContext _dbContext;

    public GenericRepository(OdontoManageDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual T Save(T obj)
    {
        _dbContext.Set<T>().Add(obj);
        _dbContext.SaveChanges();
        return obj;
    }

    public virtual IQueryable<T> GetAll()
    {
        return _dbContext.Set<T>();
    }

    public virtual T? GetById(Guid id)
    {
        return _dbContext.Set<T>().FirstOrDefault(t => t.Id == id);
    }

    public virtual T Update(T obj)
    {
        _dbContext.Set<T>().Update(obj);
        _dbContext.SaveChanges();
        return obj;
    }

    public virtual void Delete(Guid id)
    {
        T? objToDelete = _dbContext.Set<T>().FirstOrDefault(t => t.Id == id);
        if (objToDelete == null) throw new EntryPointNotFoundException();
        _dbContext.Remove(objToDelete);
        _dbContext.SaveChanges();
    }
}