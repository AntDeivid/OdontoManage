using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdontoManage.Application.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.API.Controllers;

[Route("patient-financial")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]

public class PatientFinancialController : ControllerBase
{
    private readonly IPatientFinancialService _patientFinancialService;

    public PatientFinancialController(IPatientFinancialService patientFinancialService)
    {
        _patientFinancialService = patientFinancialService;
    }

    [HttpGet("paied/{id}")]
    [Authorize]
    public ActionResult<List<Treatment>> GetTreatmentsPaied([FromRoute] Guid id)
    {
        var response = _patientFinancialService.GetTreatmentsPaied(id);
        return Ok(response);
    }
    
    [HttpGet("not-paied/{id}")]
    [Authorize]
    public ActionResult<List<Treatment>> GetTreatmentsNotPaied([FromRoute] Guid id)
    {
        var response = _patientFinancialService.GetTreatmentsNotPaied(id);
        return Ok(response);
    }
}