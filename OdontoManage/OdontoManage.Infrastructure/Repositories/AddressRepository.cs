using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;

namespace OdontoManage.Infrastructure.Repositories;

public class AddressRepository : GenericRepository<Address>, IAddressRespository
{
    public AddressRepository(OdontoManageDbContext dbContext) : base(dbContext){ }
    
    public Address? GetAddressByCode(string code)
    {
        return (_dbContext.Addresses?? throw new InvalidOperationException()).FirstOrDefault(x => x.ZipCode == code);
    }
}