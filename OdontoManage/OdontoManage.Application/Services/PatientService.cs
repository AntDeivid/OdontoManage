using AutoMapper;
using Microsoft.Extensions.Logging;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Services;

public class PatientService
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
        var exist = _repository.GetPatientByCpf(patient.Cpf);
        if (exist != null)
        {
            throw new Exception("Patient already exists");
        }

        if (patient.IsForeign)
        {
            patient.Cpf = null;
            patient.Rg = null;
        }
        
        var patientEntity = _mapper.Map<Patient>(patient);
        var createdPatient = _repository.Save(patientEntity);
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

    public PatientDto GetByCpf(PatientDto patient)
    {
        var exits = _repository.GetPatientByCpf(patient.Cpf);

        if (exits == null)
        {
            throw new Exception($"Patient not found: {patient.Cpf}");
        }
        
        return _mapper.Map<PatientDto>(exits);
    }

    public List<PatientDto> GetAll()
    {
        var patients = _repository.GetAll();
        return _mapper.Map<List<PatientDto>>(patients);
    }

    public PatientDto Update(PatientDto patient)
    {
        var existing = _repository.GetById(patient.Id);
        if (existing == null)
        {
            throw new Exception($"Patient not found: {patient.Id}");
        }
        existing.Name = patient.Name;
        existing.Age = patient.Age;
        existing.Address = patient.Address;
        existing.Phone = patient.Phone;
        existing.Cpf = patient.Cpf;
        existing.Gender = patient.Gender;
        existing.Rg = patient.Rg;
        existing.IsForeign = patient.IsForeign;
        existing.BirthDay = patient.Birthday;
        existing.Document = patient.Document;
        
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