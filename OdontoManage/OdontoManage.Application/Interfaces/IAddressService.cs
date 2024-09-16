using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Interfaces;

public interface IAddressService
{
    AddressDto? Create(AddressCreateDto address);
    
    AddressDto? Update(Guid id,AddressUpdateDto address);
    
    void Delete(Guid id);
    
    AddressDto? GetById(Guid id);
    
    AddressDto? GetByCode(string code);
    
    List<AddressDto> GetAll();
}