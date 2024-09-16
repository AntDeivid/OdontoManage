using OdontoManage.Core.Enums;

namespace OdontoManage.Application.Models.DTOs;

public class ExpenseCreateDto
{
    public double Value { get; set; }
    public string? Description { get; set; }
    public ExpenseCategory Category { get; set; }
    public Box Box { get; set; }
    public string? Observation { get; set; }
    public bool Repetitive { get; set; }
    public RepetitionType RepetitionType { get; set; }
    public int RepetitionQuantity { get; set; }
    public DateDto InstallmentDueDate { get; set; }
    public bool Paid { get; set; }
    public DateDto? PaymentDate { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}