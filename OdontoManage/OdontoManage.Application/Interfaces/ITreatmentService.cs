using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.Application.Interfaces;

public interface ITreatmentService
{
    TreatmentDto Create(TreatmentCreateDto treatmentDto);
    List<TreatmentDto> GetAll();
    List<TreatmentDto> GetByPatientId(Guid patientId, int page, int pageSize);
    TreatmentDto Update(Guid id, TreatmentDto treatmentDto);
    void Delete(Guid id);
}