using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.API.Controllers;

[Route("clinical-treatments")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class ClinicalTreatmentController : ControllerBase
{
    
    private readonly IClinicalTreatmentService _clinicalTreatmentService;

    public ClinicalTreatmentController(IClinicalTreatmentService clinicalTreatmentService)
    {
        _clinicalTreatmentService = clinicalTreatmentService;
    }
    
    /// <summary> Saves a new clinical treatment </summary>
    /// <param name="clinicalTreatmentDto"></param>
    /// <returns> Saved clinical treatment </returns>
    [HttpPost]
    public ActionResult<ClinicalTreatmentDto> Create([FromBody] ClinicalTreatmentDto clinicalTreatmentDto)
    {
        var response = _clinicalTreatmentService.Create(clinicalTreatmentDto);
        return Ok(response);
    }
    
    /// <summary> Get all clinical treatments </summary>
    /// <returns> All clinical treatments </returns>
    [HttpGet]
    public ActionResult<List<ClinicalTreatmentDto>> GetAll()
    {
        var response = _clinicalTreatmentService.GetAll();
        return Ok(response);
    }
    
    /// <summary> Get a clinical treatment by id </summary>
    /// <param name="id"> Clinical treatment id </param>
    /// <returns> Clinical treatment </returns>
    [HttpGet("{id}")]
    public ActionResult<ClinicalTreatmentDto> GetById(Guid id)
    {
        var response = _clinicalTreatmentService.GetById(id);
        return Ok(response);
    }
    
    /// <summary> Update a clinical treatment </summary>
    /// <param name="id"> Clinical treatment id </param>
    /// <param name="clinicalTreatmentDto"> Clinical treatment data </param>
    /// <returns> Updated clinical treatment </returns>
    [HttpPut("{id}")]
    public ActionResult<ClinicalTreatmentDto> Update(Guid id, [FromBody] ClinicalTreatmentDto clinicalTreatmentDto)
    {
        var response = _clinicalTreatmentService.Update(id, clinicalTreatmentDto);
        return Ok(response);
    }
    
    /// <summary> Delete a clinical treatment </summary>
    /// <param name="id"> Clinical treatment id </param>
    /// <returns> No content </returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        _clinicalTreatmentService.Delete(id);
        return Ok();
    }
}