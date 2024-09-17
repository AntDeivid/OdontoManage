using AutoMapper;
using Moq;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Application.Services;
using OdontoManage.Core.Enums;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.UnitTests;

[TestFixture]
public class ExpenseServiceTests
{
    private Mock<IExpenseRepository> _expenseRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private ExpenseService _expenseService;

    [SetUp]
    public void SetUp()
    {
        _expenseRepositoryMock = new Mock<IExpenseRepository>();
        _mapperMock = new Mock<IMapper>();

        _expenseService = new ExpenseService(
            _expenseRepositoryMock.Object,
            _mapperMock.Object
        );
    }

    [Test]
    public void GetAllPaged_ShouldReturnPagedExpenseDtos()
    {
        // Arrange
        const int page = 1;
        const int pageSize = 10;
        var expenses = new List<Expense>
        {
            new Expense
            {
                Id = Guid.NewGuid(),
                Value = 100.0,
                Description = "Expense 1",
                Category = ExpenseCategory.Other,
                Box = Box.Bank,
                Observation = "Observation 1",
                Repetitive = false,
                RepetitionType = RepetitionType.None,
                RepetitionQuantity = 0,
                InstallmentDueDate = new DateOnly(2024, 9, 15),
                Paid = true,
                PaymentDate = new DateOnly(2024, 9, 14),
                PaymentMethod = PaymentMethod.CreditCard
            },
            new Expense
            {
                Id = Guid.NewGuid(),
                Value = 200.0,
                Description = "Expense 2",
                Category = ExpenseCategory.Utilities,
                Box = Box.Bank,
                Observation = "Observation 2",
                Repetitive = true,
                RepetitionType = RepetitionType.Monthly,
                RepetitionQuantity = 3,
                InstallmentDueDate = new DateOnly(2024, 10, 15),
                Paid = false,
                PaymentDate = null,
                PaymentMethod = PaymentMethod.BankTransfer
            }
        };

        var expenseDtos = expenses.Select(e => new ExpenseDto
        {
            Id = e.Id,
            Value = e.Value,
            Description = e.Description,
            Category = e.Category,
            Box = e.Box,
            Observation = e.Observation,
            Repetitive = e.Repetitive,
            RepetitionType = e.RepetitionType,
            RepetitionQuantity = e.RepetitionQuantity,
            InstallmentDueDate = e.InstallmentDueDate,
            Paid = e.Paid,
            PaymentDate = e.PaymentDate,
            PaymentMethod = e.PaymentMethod
        }).ToList();

        _expenseRepositoryMock.Setup(repo => repo.GetAllPaged(page, pageSize)).Returns(expenses.AsQueryable());
        _mapperMock.Setup(mapper => mapper.Map<List<ExpenseDto>>(It.IsAny<List<Expense>>())).Returns(expenseDtos);

        // Act
        var result = _expenseService.GetAllPaged(page, pageSize);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(expenses.Count));
        for (var i = 0; i < expenses.Count; i++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(result[i].Id, Is.EqualTo(expenses[i].Id));
                Assert.That(result[i].Value, Is.EqualTo(expenses[i].Value));
                Assert.That(result[i].Description, Is.EqualTo(expenses[i].Description));
                Assert.That(result[i].Category, Is.EqualTo(expenses[i].Category));
                Assert.That(result[i].Box, Is.EqualTo(expenses[i].Box));
                Assert.That(result[i].Observation, Is.EqualTo(expenses[i].Observation));
                Assert.That(result[i].Repetitive, Is.EqualTo(expenses[i].Repetitive));
                Assert.That(result[i].RepetitionType, Is.EqualTo(expenses[i].RepetitionType));
                Assert.That(result[i].RepetitionQuantity, Is.EqualTo(expenses[i].RepetitionQuantity));
                Assert.That(result[i].InstallmentDueDate, Is.EqualTo(expenses[i].InstallmentDueDate));
                Assert.That(result[i].Paid, Is.EqualTo(expenses[i].Paid));
                Assert.That(result[i].PaymentDate, Is.EqualTo(expenses[i].PaymentDate));
                Assert.That(result[i].PaymentMethod, Is.EqualTo(expenses[i].PaymentMethod));
            });
        }
    }
}