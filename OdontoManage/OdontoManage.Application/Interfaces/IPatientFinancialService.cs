using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Interfaces;

public interface IPatientFinancialService
{
    public List<TreatmentDto> GetTreatmentsPaied(Guid id);
    public List<TreatmentDto> GetTreatmentsNotPaied(Guid id);
}