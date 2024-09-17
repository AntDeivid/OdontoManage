using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.API.Controllers;

[Route("schedulings")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class SchedulingController : ControllerBase
{
    
    private readonly ISchedulingService _schedulingService;

    public SchedulingController(ISchedulingService schedulingService)
    {
        _schedulingService = schedulingService;
    }
    
    /// <summary> Saves a new scheduling </summary>
    /// <param name="schedulingDto"></param>
    /// <returns> Saved scheduling </returns>
    [HttpPost]
    [Authorize]
    public ActionResult<SchedulingDto> Create([FromBody] SchedulingCreateDto schedulingDto)
    {
        var response = _schedulingService.Create(schedulingDto);
        return Ok(response);
    }
    
    /// <summary> Get all schedulings </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns> All schedulings </returns>
    [HttpGet]
    [Authorize]
    public ActionResult<List<SchedulingDto>> GetAll([FromQuery] DateDto start, [FromQuery] DateDto end)
    {
        var response = _schedulingService.GetByInterval(start, end);
        return Ok(response);
    }
    
    /// <summary> Get a scheduling by id </summary>
    /// <param name="id"> Scheduling id </param>
    /// <returns> Scheduling </returns>
    [HttpGet("{id:guid}")]
    [Authorize]
    public ActionResult<SchedulingDto> GetById(Guid id)
    {
        var response = _schedulingService.GetById(id);
        return Ok(response);
    }
    
    /// <summary> Delete a scheduling </summary>
    /// <param name="id"> Scheduling id </param>
    /// <returns> No content </returns>
    [HttpDelete("{id:guid}")]
    [Authorize]
    public ActionResult Delete(Guid id)
    {
        _schedulingService.Delete(id);
        return NoContent();
    }
}