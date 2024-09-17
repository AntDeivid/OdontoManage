using System.Collections.ObjectModel;
using AutoMapper;
using Microsoft.VisualBasic;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Services;

public class RevenueService : IRevenueService
{
    private readonly IRevenueRepository _repository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDentistRepository _dentistRepository;
    private readonly IClinicalTreatmentRepository _clinicalTreatmentRepository;
    private readonly IMapper _mapper;


    public RevenueService
        (IRevenueRepository repository, IPatientRepository patientRepository,IDentistRepository dentistRepository, 
            IMapper mapper, IClinicalTreatmentRepository clinicalTreatmentRepository)
    {
        _repository = repository;
        _patientRepository = patientRepository;
        _dentistRepository = dentistRepository;
        _clinicalTreatmentRepository = clinicalTreatmentRepository;
        _mapper = mapper;
    }
    
    public void CreateRevenue(RevenueCreateDto revenue)
    {
        var patient = _patientRepository.GetById(revenue.PatientID);

        if (patient == null)
        {
            throw new ApplicationException("Patient not found");
        }
        
        var revenueNew = new Revenue();
        
        revenueNew.Patient = patient;

        var revenueCollecion = new Collection<Treatment>();
        
        foreach (var treatment in revenue.Treatment)
        {
            var patientTreatment = _patientRepository.GetById(treatment.PatientId);
            if (patientTreatment == null)
            {
                throw new ApplicationException("Patient not found");
            }
            
            var dentistTreatment = _dentistRepository.GetById(treatment.DentistId);
            if (dentistTreatment == null)
            {
                throw new ApplicationException("Dentist not found");
            }
            
            var clinicalTreatment = _clinicalTreatmentRepository.GetById(treatment.ClinicalTreatmentId);
            if (clinicalTreatment == null)
            {
                throw new ApplicationException("Clinical treatment not found");
            }
            
            var treatmentEntity = _mapper.Map<Treatment>(treatment);
            revenueCollecion.Add(treatmentEntity);
        }
        
        revenueNew.Treatments = revenueCollecion;

        var revenueDate = new DateOnly(revenue.InstallmentDueDate.Year, revenue.InstallmentDueDate.Month, revenue.InstallmentDueDate.Day);
        
        revenueNew.Observation = revenue.Observation;
        
        _repository.Save(revenueNew);
    }
}