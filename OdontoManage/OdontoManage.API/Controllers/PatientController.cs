using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Application.Services;
using OdontoManage.Core.Models;

namespace OdontoManage.API.Controllers;

[Route("patients")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]

public class PatientController(IPatientService patientService) : ControllerBase
{

   [HttpPost]
   [Authorize]
   public ActionResult<PatientDto> Post([FromBody] PatientCreateDto patient)
   {
      var response = patientService.Create(patient);
      return Ok(response); // trocar para que retorne 201
   }

   [HttpGet("by-cpf")]
   [Authorize]
   public ActionResult<PatientDto> Get([FromQuery] [Required] string cpf)
   {
      var response = patientService.GetByCpf(cpf);
      return Ok(response);
   }

   [HttpGet]
   [Authorize]
   public ActionResult<List<PatientDto>> GetAll()
   {
      var response = patientService.GetAll();
      return Ok(response);
   }

   [HttpPut("{id}")]
   [Authorize]
   public ActionResult<PatientDto> Put(Guid id, [FromBody] PatientUpdateDto patient)
   {
      var exist = patientService.GetById(id);

      if (exist == null)
      {
         return BadRequest();
      }
      
      var response = patientService.Update(id, patient);
      return Ok(response);
   }

   [HttpDelete("{id}")]
   [Authorize]
   public ActionResult<Guid> Delete(Guid id)
   {
      patientService.Delete(id);
      return NoContent();
   }
}