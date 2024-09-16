using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Interfaces;

public interface IItemService
{
    public ItemDto? Create(ItemCreateDto item);
    
    public ItemDto? Update(Guid id,ItemUpdateDto item);
    
    public void Delete(Guid id);
    
    public ItemDto? GetById(Guid id);
    
    public List<ItemDto> GetAll();
    
    public ItemDto? GetByName(string name);
}