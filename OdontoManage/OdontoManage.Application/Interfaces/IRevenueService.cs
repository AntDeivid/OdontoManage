using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Interfaces;

public interface IRevenueService
{
    public void CreateRevenue(RevenueCreateDto revenue);
}