using AutoMapper;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Services;

public class ExpenseService : IExpenseService
{
    
    private readonly IExpenseRepository _expenseRepository;
    private readonly IMapper _mapper;

    public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
    {
        _expenseRepository = expenseRepository;
        _mapper = mapper;
    }

    public ExpenseDto Create(ExpenseCreateDto expenseDto)
    {
        var expense = _mapper.Map<Expense>(expenseDto);
        expense.Id = Guid.NewGuid();
        var dueDate = new DateOnly(expenseDto.InstallmentDueDate.Year, expenseDto.InstallmentDueDate.Month, expenseDto.InstallmentDueDate.Day);
        var paymentDate = expenseDto.PaymentDate == null ? new DateOnly() : new DateOnly(expenseDto.PaymentDate?.Year ?? 0, expenseDto.PaymentDate?.Month ?? 0, expenseDto.PaymentDate?.Day ?? 0);
        expense.InstallmentDueDate = dueDate;
        expense.PaymentDate = paymentDate;
        var createdExpense = _expenseRepository.Save(expense);
        return _mapper.Map<ExpenseDto>(createdExpense);
    }

    public List<ExpenseDto> GetAllPaged(int page, int pageSize)
    {
        var expenses = _expenseRepository.GetAllPaged(page, pageSize).ToList();
        return _mapper.Map<List<ExpenseDto>>(expenses);
    }

    public ExpenseDto Update(Guid id, ExpenseDto expenseDto)
    {
        var expense = _expenseRepository.GetById(id);
        if (expense == null) throw new EntryPointNotFoundException();
        expense.Value = expenseDto.Value;
        expense.Description = expenseDto.Description;
        expense.Category = expenseDto.Category;
        expense.Box = expenseDto.Box;
        expense.Observation = expenseDto.Observation;
        expense.Repetitive = expenseDto.Repetitive;
        expense.RepetitionType = expenseDto.RepetitionType;
        expense.RepetitionQuantity = expenseDto.RepetitionQuantity;
        expense.Paid = expenseDto.Paid;
        expense.PaymentMethod = expenseDto.PaymentMethod;
        var dueDate = new DateOnly(expenseDto.InstallmentDueDate.Year, expenseDto.InstallmentDueDate.Month, expenseDto.InstallmentDueDate.Day);
        var paymentDate = new DateOnly(expenseDto.PaymentDate?.Year ?? 0, expenseDto.PaymentDate?.Month ?? 0, expenseDto.PaymentDate?.Day ?? 0);
        expense.InstallmentDueDate = dueDate;
        expense.PaymentDate = paymentDate;
        var updatedExpense = _expenseRepository.Update(expense);
        return _mapper.Map<ExpenseDto>(updatedExpense);
    }

    public void Delete(Guid id)
    {
        var expense = _expenseRepository.GetById(id);
        if (expense == null) throw new EntryPointNotFoundException();
        _expenseRepository.Delete(id);
    }
}