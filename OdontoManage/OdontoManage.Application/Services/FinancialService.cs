using AutoMapper;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;

namespace OdontoManage.Application.Services;

public class FinancialService : IFinancialService
{   
    private readonly IExpenseRepository _expenseRepository;
    private readonly ITreatmentRepository _treatmentRepository;
    private readonly IMapper _mapper;

    public FinancialService(IExpenseRepository expenseRepository, ITreatmentRepository treatmentRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
        _treatmentRepository = treatmentRepository ?? throw new ArgumentNullException(nameof(treatmentRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public double GetExpenseValue()
    {
        return _expenseRepository.GetTotalNotPaied();
    }

    public List<ExpenseDto> GetExpenses()
    {
        var result = new List<ExpenseDto>();
        var expenses = _expenseRepository.GetAll();
        if (expenses == null)
        {
            throw new Exception($"expenses is null");
        }
        var expensesMapped = _mapper.Map<List<ExpenseDto>>(expenses);
        if (expensesMapped == null)
        {
            throw new Exception($"expenses dto is null");
        }
        foreach (var expense in expensesMapped)
        {
            if (!expense.Paid)
            {
                result.Add(expense);
            }
        }
        
        return result;
    }

    public double GetRevenueValue()
    {
        var result = 0.0;
        
        var total = _treatmentRepository.GetAll();
        if (total == null)
        {
            throw new Exception($"treatment is null");
        }
        foreach (var treatment in total)
        {
            if (treatment.Paid)
            {
                result += treatment.Value;
            }
        }

        return result;
    }

    public List<TreatmentDto> GetRevenues()
    {
        
        var result = new List<TreatmentDto>();
        var treatments = _treatmentRepository.GetAll();
        if (treatments == null)
        {
            throw new Exception("treatments is null");
            
        }
        var treatmentsMapped = _mapper.Map<List<TreatmentDto>>(treatments);
        if (treatmentsMapped == null)
        {
            throw new Exception($"treatments dto is null");
        }
        foreach (var treatment in treatmentsMapped)
        {
            if (treatment.Paid)
            {
                result.Add(treatment);
            }
        }
        
        return result;
    }
}