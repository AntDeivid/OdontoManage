using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.API.Controllers;

[Route("financial")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class FinancialController : ControllerBase
{
    
    private readonly IFinancialService _financialService;

    public FinancialController(IFinancialService financialService)
    {
        _financialService = financialService;
    }

    [HttpGet("expense-value")]
    [Authorize]
    public ActionResult<double> GetExpenseValue()
    {
        var response = _financialService.GetExpenseValue();
        return Ok(response);
    }
    
    [HttpGet("expenses")]
    [Authorize]
    public ActionResult<List<ExpenseDto>> GetExpense()
    {
        var response = _financialService.GetExpenses();
        return Ok(response);
    }
    
    [HttpGet("revenues-value")]
    [Authorize]
    public ActionResult<double> GetRevenueValue()
    {
        var response = _financialService.GetRevenueValue();
        return Ok(response);
    }
    
    [HttpGet("revenues")]
    [Authorize]
    public ActionResult<List<TreatmentDto>> GetRevenues()
    {
        var response = _financialService.GetRevenues();
        return Ok(response);
    }
}