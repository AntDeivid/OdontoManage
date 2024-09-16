using OdontoManage.Core.Models;

namespace OdontoManage.Core.Interfaces;

public interface IItemRepository : IGenericRepository<Item>
{
    Item? GetByName(string name);
}