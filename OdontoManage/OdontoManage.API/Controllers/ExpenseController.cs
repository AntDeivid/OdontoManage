using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.API.Controllers;

[Route("expenses")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    /// <summary> Saves a new expense </summary>
    /// <param name="expenseDto"></param>
    /// <returns> Saved expense </returns>
    [HttpPost]
    public ActionResult<ExpenseDto> Create([FromBody] ExpenseCreateDto expenseDto)
    {
        var response = _expenseService.Create(expenseDto);
        return Ok(response);
    }
    
    /// <summary> Get all expenses, paged </summary>
    /// <param name="page"> Page number </param>
    /// <param name="pageSize"> Page size </param>
    /// <returns> All expenses </returns>
    [HttpGet]
    public ActionResult<List<ExpenseDto>> GetAllPaged([FromQuery] int page, [FromQuery] int pageSize)
    {
        var response = _expenseService.GetAllPaged(page, pageSize);
        return Ok(response);
    }
    
    /// <summary> Update an expense </summary>
    /// <param name="id"> Expense id </param>
    /// <param name="expenseDto"> Expense data </param>
    /// <returns> Updated expense </returns>
    [HttpPut("{id}")]
    public ActionResult<ExpenseDto> Update(Guid id, [FromBody] ExpenseDto expenseDto)
    {
        var response = _expenseService.Update(id, expenseDto);
        return Ok(response);
    }
    
    /// <summary> Delete an expense </summary>
    /// <param name="id"> Expense id </param>
    /// <returns> No content </returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        _expenseService.Delete(id);
        return Ok();
    }
}