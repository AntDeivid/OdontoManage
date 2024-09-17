using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository; 
    private readonly IAddressRespository _addressRepository;
    private readonly IMapper _mapper; 
    private readonly ILogger<PatientService> _logger;

    public PatientService(IPatientRepository repository, IMapper mapper, ILogger<PatientService> logger, IAddressRespository addressRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _addressRepository = addressRepository;
    }
    public PatientDto Create(PatientCreateDto patient)
    {
        patient.Cpf ??= "";
        var exist = _repository.GetPatientByCpfWithAddress(patient.Cpf);
        if (exist != null)
        {
            throw new Exception("Patient already exists");
        }

        if (patient.IsForeign)
        {
            patient.Cpf = null;
            patient.Rg = null;
        }
        else
        {
            patient.Document = null;
        }

        var patientEntity = _mapper.Map<Patient>(patient);
        patientEntity.BirthDay = new DateOnly(patient.Birthday.Year, patient.Birthday.Month, patient.Birthday.Day);

        // 1. Salvar o paciente primeiro
        var createdPatient = _repository.Save(patientEntity);

        // 2. Depois, associar e salvar o endereço
        var addressEntity = _mapper.Map<Address>(patient.Address);
        var createdAddress = _addressRepository.Save(addressEntity);
        return _mapper.Map<PatientDto>(createdPatient);
    }


    public PatientDto GetById(Guid id)
    {
        var patient = _repository.GetPatientByIdWithAddress(id);
        if (patient == null)
        {
            throw new Exception($"Patient not found: {id}");
        }
        return _mapper.Map<PatientDto>(patient);
    }

    public PatientDto GetByCpf(string cpf)
    {
        var exits = _repository.GetPatientByCpfWithAddress(cpf);

        if (exits == null)
        {
            throw new Exception($"Patient not found: {cpf}");
        }
        
        return _mapper.Map<PatientDto>(exits);
    }

    public List<PatientDto> GetAll()
    {
        var patients = _repository.GetAll().Include(p => p.Address).ToList();
        return _mapper.Map<List<PatientDto>>(patients).ToList();
    }

    public PatientDto Update(Guid id, PatientUpdateDto patient)
    {
        var existing = _repository.GetPatientByIdWithAddress(id);
        if (existing == null)
        {
            throw new Exception($"Patient not found: {id}");
        }
        existing.Name = patient.Name;
        existing.Age = patient.Age;
        existing.Address.Street = patient.Address.Street;
        existing.Address.City = patient.Address.City;
        existing.Address.State = patient.Address.State;
        existing.Address.ZipCode = patient.Address.ZipCode;
        existing.Address.Neighborhood = patient.Address.Neighborhood;
        existing.Phone = patient.Phone;
        existing.Gender = patient.Gender;
        existing.IsForeign = patient.IsForeign;
        if (existing.IsForeign)
        {
            existing.Document = patient.Document;
            existing.Rg = null;
            existing.Cpf = null;
        }
        else
        {
            existing.Document = null;  
            existing.Rg = patient.Rg;
            existing.Cpf = patient.Cpf;
        }
        var date = new DateOnly(patient.Birthday.Year, patient.Birthday.Month, patient.Birthday.Day);
        existing.BirthDay = date;
        
        var exist = _addressRepository.GetAddressByCode(existing.Address.ZipCode);
        if (exist == null)
        {
            throw new Exception($"Address not found");
        }
                
        exist.ZipCode = patient.Address.ZipCode;
        exist.City = patient.Address.City;
        exist.Street = patient.Address.Street;
        exist.Neighborhood = patient.Address.Neighborhood;
        exist.State = patient.Address.State;
                
        var updatedAddress = _addressRepository.Update(exist);
        var updatePatient = _repository.Update(existing);
        return _mapper.Map<PatientDto>(updatePatient);
    }

    public void Delete(Guid id)
    {
        var existing = _repository.GetPatientByIdWithAddress(id);
        if (existing == null)
        {
            throw new Exception($"Patient not found: {id}");
        }
        _addressRepository.Delete(existing.Address.Id);
        _repository.Delete(id);
    }
}