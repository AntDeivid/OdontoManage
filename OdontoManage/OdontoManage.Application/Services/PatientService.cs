using AutoMapper;
using Microsoft.Extensions.Logging;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository; 
    private readonly IMapper _mapper; 
    private readonly ILogger<PatientService> _logger;

    public PatientService(IPatientRepository repository, IMapper mapper, ILogger<PatientService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public PatientDto Create(PatientCreateDto patient)
    {
        Console.WriteLine("entrou no service");
        var exist = _repository.GetPatientByCpf(patient.Cpf);
        Console.WriteLine("passou do getPatientByCpf");
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
        Console.WriteLine("passou do map");
        var createdPatient = _repository.Save(patientEntity);
        Console.WriteLine("passou do save");
        return _mapper.Map<PatientDto>(createdPatient);
    }

    public PatientDto GetById(Guid id)
    {
        var patient = _repository.GetById(id);
        if (patient == null)
        {
            throw new Exception($"Patient not found: {id}");
        }
        return _mapper.Map<PatientDto>(patient);
    }

    public PatientDto GetByCpf(string cpf)
    {
        var exits = _repository.GetPatientByCpf(cpf);

        if (exits == null)
        {
            throw new Exception($"Patient not found: {cpf}");
        }
        
        return _mapper.Map<PatientDto>(exits);
    }

    public List<PatientDto> GetAll()
    {
        var patients = _repository.GetAll();
        return _mapper.Map<List<PatientDto>>(patients);
    }

    public PatientDto Update(Guid id, PatientUpdateDto patient)
    {
        var existing = _repository.GetById(id);
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
        
        var updatePatient = _repository.Update(existing);
        return _mapper.Map<PatientDto>(updatePatient);
    }

    public void Delete(Guid id)
    {
        var existing = _repository.GetById(id);
        if (existing == null)
        {
            throw new Exception($"Patient not found: {id}");
        }
        _repository.Delete(id);
    }
} 