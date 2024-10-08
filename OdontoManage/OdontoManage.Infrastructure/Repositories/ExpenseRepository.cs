using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;

namespace OdontoManage.Infrastructure.Repositories;

public class ExpenseRepository(OdontoManageDbContext dbContext)
    : GenericRepository<Expense>(dbContext), IExpenseRepository
{
    public double GetTotalNotPaied()
    {
        return dbContext.Expenses.Where(e => e.Paid == false).Sum(e => e.Value);
    }
}