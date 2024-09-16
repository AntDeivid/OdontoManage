using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.Application.Interfaces;

public interface IClinicalTreatmentService
{
    ClinicalTreatmentDto Create(ClinicalTreatmentDto clinicalTreatmentDto);
    ClinicalTreatmentDto Update(Guid id, ClinicalTreatmentDto clinicalTreatmentDto);
    ClinicalTreatmentDto GetById(Guid id);
    List<ClinicalTreatmentDto> GetAll();
    void Delete(Guid id);
    
}