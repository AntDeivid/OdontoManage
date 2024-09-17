using Microsoft.VisualBasic;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Interfaces;

public interface IFinancialService
{
    public double GetRevenueValue();
    public  double GetExpenseValue();
    public List<ExpenseDto> GetExpenses();
    public List<TreatmentDto> GetRevenues();
    
}