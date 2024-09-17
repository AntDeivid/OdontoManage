using OdontoManage.Core.Models;

namespace OdontoManage.Core.Interfaces;

public interface IExpenseRepository : IGenericRepository<Expense>
{
    public double GetTotalNotPaied();
}