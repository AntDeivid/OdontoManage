using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;

namespace OdontoManage.Infrastructure.Repositories;

public class ItemRepository : GenericRepository<Item>, IItemRepository
{
    public ItemRepository(OdontoManageDbContext dbContext) : base(dbContext)
    {
    }

    public Item? GetByName(string name)
    {
        return (_dbContext.Stocks ?? throw new InvalidOperationException()).FirstOrDefault(x => x.Name == name);
    }
}