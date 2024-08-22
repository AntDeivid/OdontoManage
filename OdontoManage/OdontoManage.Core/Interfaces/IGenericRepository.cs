namespace OdontoManage.Core.Interfaces;

public interface IGenericRepository<T>
{
    T Save(T obj);
    
    IQueryable<T> GetAll();
    
    T? GetById(Guid id);
    
    T Update(T obj);
    
    void Delete(Guid id);
}