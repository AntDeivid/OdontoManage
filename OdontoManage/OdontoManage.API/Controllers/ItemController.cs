using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.API.Controllers;

[Route("itens")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]


public class ItemController (IItemService service) : ControllerBase
{
    [HttpPost]
    [Authorize]

    public ActionResult<ItemDto> Post([FromBody] ItemCreateDto item)
    {
        var response =  service.Create(item);
        return Ok(response);
    }
    
    [HttpGet("by-name")]
    [Authorize]
    public ActionResult<ItemDto> Get([FromQuery] [Required] string name)
    {
        var response = service.GetByName(name);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<ItemDto> Get([FromQuery] [Required] Guid id)
    {
        var response = service.GetById(id);
        return Ok(response);
    }

    
    [HttpGet]
    [Authorize]
    public ActionResult<List<ItemDto>> GetAll()
    {
        var response = service.GetAll();
        return Ok(response);
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<ItemDto> Put(Guid id, [FromBody] ItemUpdateDto item)
    {
        var exist = service.GetById(id);

        if (exist == null)
        {
            return BadRequest();
        }
      
        var response = service.Update(id, item);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<Guid> Delete(Guid id)
    {
        service.Delete(id);
        return NoContent();
    }
}