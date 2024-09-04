using OdontoManage.Application.Models.DTOs;
namespace OdontoManage.Application.Interfaces;

public interface IPatientService
{
    PatientDto Create(PatientCreateDto patient);
    PatientDto Update(Guid id, PatientUpdateDto patient);
    void Delete(Guid id);
    PatientDto GetById(Guid id);
    List<PatientDto> GetAll();
    PatientDto GetByCpf(string cpf);
}