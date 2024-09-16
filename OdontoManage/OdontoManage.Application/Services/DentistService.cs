using AutoMapper;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Services;

public class DentistService : IDentistService
{
    
    private readonly IDentistRepository _dentistRepository;
    private readonly IMapper _mapper;

    public DentistService(IDentistRepository dentistRepository, IMapper mapper)
    {
        _dentistRepository = dentistRepository;
        _mapper = mapper;
    }

    public DentistDto Create(DentistCreateDto dentistDto)
    {
        var existingDentist = _dentistRepository.GetByCpf(dentistDto.CPF);
        if (existingDentist != null)
        {
            throw new Exception("Dentist already exists");
        }
        
        var dentist = _mapper.Map<Dentist>(dentistDto);
        var saved = _dentistRepository.Save(dentist);
        return _mapper.Map<DentistDto>(saved);
    }

    public List<DentistDto> GetAll()
    {
        var dentists = _dentistRepository.GetAll();
        return _mapper.Map<List<DentistDto>>(dentists);
    }

    public List<DentistDto> GetAllPaged(int page, int pageSize)
    {
        var dentists = _dentistRepository.GetAllPaged(page, pageSize);
        return _mapper.Map<List<DentistDto>>(dentists);
    }

    public DentistDto Update(Guid id, DentistUpdateDto dentistDto)
    {
        var dentist = _dentistRepository.GetById(id);
        if (dentist == null) throw new EntryPointNotFoundException();
        dentist.Name = dentistDto.Name;
        dentist.CPF = dentistDto.CPF;
        dentist.CRO = dentistDto.CRO;
        dentist.Specialty = dentistDto.Specialty;
        dentist.Phone = dentistDto.Phone;
        dentist.Email = dentistDto.Email;
        dentist.Address = dentistDto.Address;
        var updatedDentist = _dentistRepository.Update(dentist);
        return _mapper.Map<DentistDto>(updatedDentist);
    }

    public void Delete(Guid id)
    {
        var dentist = _dentistRepository.GetById(id);
        if (dentist == null) throw new EntryPointNotFoundException();
        _dentistRepository.Delete(id);
    }
}