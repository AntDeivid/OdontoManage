using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.Application.Interfaces;

public interface IExpenseService
{
    ExpenseDto Create(ExpenseCreateDto expenseDto);
    List<ExpenseDto> GetAllPaged(int page, int pageSize);
    ExpenseDto Update(Guid id, ExpenseUpdateDto expenseDto);
    void Delete(Guid id);
    
}