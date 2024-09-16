using AutoMapper;
using Microsoft.Extensions.Logging;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Services;

public class ItemService : IItemService
{
    
    private readonly IMapper _mapper;
    private readonly IItemRepository _repository;


    public ItemService(IMapper mapper, IItemRepository repository, ILogger<ItemService> logger)
    {
        _mapper = mapper;
        _repository = repository;
        
    }
    
    public ItemDto? Create(ItemCreateDto item)
    {
        var exist = _repository.GetByName(item.Name);

        if (exist != null)
        {
            throw new Exception($"{item.Name} already exists in Stock!");
        }
        
        var stockEntity = _mapper.Map<Item>(item);
        var createdStock = _repository.Save(stockEntity);
        return _mapper.Map<ItemDto>(createdStock);
    }

    public ItemDto? GetById(Guid id)
    {
        var exist = _repository.GetById(id);
        if (exist == null)
        {
            throw new Exception($"Item with id: {id} does not exist!");
        }
        return _mapper.Map<ItemDto>(exist);
    }

    public ItemDto? GetByName(string item)
    {
        var exist = _repository.GetByName(item);
        if (exist == null)
        {
            throw new Exception($"Item with name: {item} does not exist!");
        }
        return _mapper.Map<ItemDto>(exist);
    }

    public List<ItemDto> GetAll()
    {
        var exist = _repository.GetAll();
        return _mapper.Map<List<ItemDto>>(exist);
    }

    public ItemDto? Update(Guid id, ItemUpdateDto item)
    {
        var exist = _repository.GetById(id);
        if (exist == null)
        {
            throw new Exception($"Item with id: {id} does not exist!");
        }
        
        exist.Name = item.Name;
        exist.Amount = item.Amount;
        exist.Price = item.Price;
        
        var updatedStock = _repository.Update(exist);
        return _mapper.Map<ItemDto>(updatedStock);
    }

    public void Delete(Guid id)
    {
        var exist = _repository.GetById(id);
        if (exist == null)
        {
            throw new Exception($"Item with id: {id} does not exist!");
        }

        _repository.Delete(id);
    }
}