using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.API.Controllers;

[Route("dentists")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class DentistController : ControllerBase
{
 
    private readonly IDentistService _dentistService;

    public DentistController(IDentistService dentistService)
    {
        _dentistService = dentistService;
    }
    
    /// <summary> Saves a new dentist </summary>
    /// <param name="dentistDto"></param>
    /// <returns> Saved dentist </returns>
    [HttpPost]
    public ActionResult<DentistDto> Create([FromBody] DentistDto dentistDto)
    {
        var response = _dentistService.Create(dentistDto);
        return Ok(response);
    }
    
    /// <summary> Get all dentists, paged </summary>
    /// <param name="page"> Page number </param>
    /// <param name="pageSize"> Page size </param>
    /// <returns> All dentists </returns>
    [HttpGet("all-no-pagination")]
    public ActionResult<List<DentistDto>> GetAllPaged([FromQuery] int page, [FromQuery] int pageSize)
    {
        var response = _dentistService.GetAllPaged(page, pageSize);
        return Ok(response);
    }
    
    /// <summary> Get all dentists, without pagination </summary>
    /// <returns> All dentists </returns>
    [HttpGet]
    public ActionResult<List<DentistDto>> GetAll()
    {
        var response = _dentistService.GetAll();
        return Ok(response);
    }
    
    /// <summary> Update a dentist </summary>
    /// <param name="id"> Dentist id </param>
    /// <param name="dentistDto"> Dentist data </param>
    /// <returns> Updated dentist </returns>
    [HttpPut("{id}")]
    public ActionResult<DentistDto> Update(Guid id, [FromBody] DentistDto dentistDto)
    {
        var response = _dentistService.Update(id, dentistDto);
        return Ok(response);
    }
    
    /// <summary> Delete a dentist </summary>
    /// <param name="id"> Dentist id </param>
    /// <returns> No content </returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        _dentistService.Delete(id);
        return Ok();
    }
}