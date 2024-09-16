using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.API.Controllers;

[Route("treatments")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class TreatmentController : ControllerBase
{
    
    private readonly ITreatmentService _treatmentService;

    public TreatmentController(ITreatmentService treatmentService)
    {
        _treatmentService = treatmentService;
    }

    /// <summary> Saves a new treatment </summary>
    /// <param name="treatmentDto"></param>
    /// <returns> Saved treatment </returns>
    [HttpPost]
    [Authorize]
    public ActionResult<TreatmentDto> Create([FromBody] TreatmentCreateDto treatmentDto)
    {
        var response = _treatmentService.Create(treatmentDto);
        return Ok(response);
    }
    
    /// <summary> Get all treatments </summary>
    /// <returns> All treatments </returns>
    [HttpGet]
    [Authorize]
    public ActionResult<List<TreatmentDto>> GetAll()
    {
        var response = _treatmentService.GetAll();
        return Ok(response);
    }
    
    /// <summary> Get all treatments from a patient </summary>
    /// <param name="patientId"> Patient id </param>
    /// <param name="page"> Page number </param>
    /// <param name="pageSize"> Page size </param>
    /// <returns> All treatments from a patient </returns>
    [HttpGet("by-patient")]
    [Authorize]
    public ActionResult<List<TreatmentDto>> GetByPatientId([FromQuery] Guid patientId, [FromQuery] int page, [FromQuery] int pageSize)
    {
        var response = _treatmentService.GetByPatientId(patientId, page, pageSize);
        return Ok(response);
    }
    
    /// <summary> Update a treatment </summary>
    /// <param name="id"> Treatment id </param>
    /// <param name="treatmentDto"> Treatment data </param>
    /// <returns> Updated treatment </returns>
    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<TreatmentDto> Update(Guid id, [FromBody] TreatmentDto treatmentDto)
    {
        var response = _treatmentService.Update(id, treatmentDto);
        return Ok(response);
    }
    
    /// <summary> Delete a treatment </summary>
    /// <param name="id"> Treatment id </param>
    /// <returns> No content </returns>
    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult Delete(Guid id)
    {
        _treatmentService.Delete(id);
        return Ok();
    }
}