using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.Application.Interfaces;

public interface IExpenseService
{
    ExpenseDto Create(ExpenseDto expenseDto);
    List<ExpenseDto> GetAllPaged(int page, int pageSize);
    ExpenseDto Update(Guid id, ExpenseDto expenseDto);
    void Delete(Guid id);
    
}