using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.API.Controllers;

[Route("adresses")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]

public class AddressController (IAddressService service) :ControllerBase
{
    [HttpPost]
    [Authorize]

    public ActionResult<AddressDto> Post([FromBody] AddressCreateDto address)
    {
        var response = service.Create(address);
        return Ok(response);
    }

    [HttpGet("by-zipcode")]
    [Authorize]
    public ActionResult<AddressDto> Get(string zipCode)
    {
        var response = service.GetByCode(zipCode);
        return Ok(response);
    }
    
    [HttpGet("{id}")]
    [Authorize]
    public ActionResult<AddressDto> Get([FromQuery] [Required] Guid id)
    {
        var response = service.GetById(id);
        return Ok(response);
    }
    
    [HttpGet]
    [Authorize]
    public ActionResult<List<AddressDto>> GetAll()
    {
        var response = service.GetAll();
        return Ok(response);
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<AddressDto> Put(Guid id, [FromBody] AddressUpdateDto item)
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