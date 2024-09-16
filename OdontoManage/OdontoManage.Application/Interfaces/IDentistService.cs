using OdontoManage.Application.Models.DTOs;

namespace OdontoManage.Application.Interfaces;

public interface IDentistService
{
    DentistDto Create(DentistDto dentistDto);
    List<DentistDto> GetAll();
    List<DentistDto> GetAllPaged(int page, int pageSize);
    DentistDto Update(Guid id, DentistDto dentistDto);
    void Delete(Guid id);
}