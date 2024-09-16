using AutoMapper;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Services;

public class TreatmentService : ITreatmentService
{
    
    private readonly ITreatmentRepository _treatmentRepository;
    private readonly IMapper _mapper;
    private readonly IPatientRepository _patientRepository;
    private readonly IDentistRepository _dentistRepository;
    private readonly IClinicalTreatmentRepository _clinicalTreatmentRepository;

    public TreatmentService(ITreatmentRepository treatmentRepository, IMapper mapper, IPatientRepository patientRepository, IDentistRepository dentistRepository, IClinicalTreatmentRepository clinicalTreatmentRepository)
    {
        _treatmentRepository = treatmentRepository;
        _mapper = mapper;
        _patientRepository = patientRepository;
        _dentistRepository = dentistRepository;
        _clinicalTreatmentRepository = clinicalTreatmentRepository;
    }

    public TreatmentDto Create(TreatmentCreateDto treatmentDto)
    {
        var treatment = _mapper.Map<Treatment>(treatmentDto);
        
        var patient = _patientRepository.GetById(treatmentDto.PatientId);
        if (patient == null) { throw new Exception("Patient not found"); }
        treatment.Patient = patient;
        
        var dentist = _dentistRepository.GetById(treatmentDto.DentistId);
        if (dentist == null) { throw new Exception("Dentist not found"); }
        treatment.Dentist = dentist;

        var clinicalTreatment = _clinicalTreatmentRepository.GetById(treatmentDto.ClinicalTreatmentId);
	    if (clinicalTreatment == null) { throw new Exception("Clinical Treatment not found"); }
		treatment.ClinicalTreatment = clinicalTreatment;
        
        var date = new DateOnly(treatmentDto.InstallmentDueDate.Year, treatmentDto.InstallmentDueDate.Month, treatmentDto.InstallmentDueDate.Day);
        treatment.InstallmentDueDate = date;
        
        var createdTreatment = _treatmentRepository.Save(treatment);
        return _mapper.Map<TreatmentDto>(createdTreatment);
    }

    public List<TreatmentDto> GetAll()
    {
        var treatments = _treatmentRepository.GetAll().ToList();
        return _mapper.Map<List<TreatmentDto>>(treatments);
    }

    public List<TreatmentDto> GetByPatientId(Guid patientId, int page, int pageSize)
    {
        var treatments = _treatmentRepository.GetTreatmentsByPatientId(patientId, page, pageSize);
        return _mapper.Map<List<TreatmentDto>>(treatments);
    }

    public TreatmentDto Update(Guid id, TreatmentDto treatmentDto)
    {
        var treatment = _treatmentRepository.GetById(id);
        if (treatment == null) { throw new Exception("Treatment not found"); }
        
        var patient = _patientRepository.GetById(treatmentDto.PatientId);
        if (patient == null) { throw new Exception("Patient not found"); }
        treatment.Patient = patient;
        
        var dentist = _dentistRepository.GetById(treatmentDto.DentistId);
        if (dentist == null) { throw new Exception("Dentist not found"); }
        treatment.Dentist = dentist;

		var clinicalTreatment = _clinicalTreatmentRepository.GetById(treatmentDto.ClinicalTreatmentId);
        if (clinicalTreatment == null) { throw new Exception("Clinical Treatment not found"); }
        treatment.ClinicalTreatment = clinicalTreatment;
        
        var date = new DateOnly(treatmentDto.InstallmentDueDate.Year, treatmentDto.InstallmentDueDate.Month, treatmentDto.InstallmentDueDate.Day);
        treatment.InstallmentDueDate = date;
        
        treatment.Plan = treatmentDto.Plan;
        treatment.Teeths = treatmentDto.Teeths;
        treatment.Region = treatmentDto.Region;
        treatment.Value = treatmentDto.Value;
        treatment.Paid = treatmentDto.Paid;
        
        var updatedTreatment = _treatmentRepository.Save(treatment);
        return _mapper.Map<TreatmentDto>(updatedTreatment);
    }

    public void Delete(Guid id)
    {
        var treatment = _treatmentRepository.GetById(id);
        if (treatment == null) { throw new Exception("Treatment not found"); }
        _treatmentRepository.Delete(treatment.Id);
    }
}