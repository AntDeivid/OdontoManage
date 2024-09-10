using OdontoManage.Core.Models;

namespace OdontoManage.Core.Interfaces;

public interface IAddressRespository : IGenericRepository<Address>
{
    Address? GetAddressByCode(string code);
}