using AutoMapper;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Services;

public class SchedulingService : ISchedulingService
{

    private readonly ISchedulingRepository _schedulingRepository;
    private readonly IDentistRepository _dentistRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public SchedulingService(ISchedulingRepository schedulingRepository, IMapper mapper, IDentistRepository dentistRepository, IPatientRepository patientRepository)
    {
        _schedulingRepository = schedulingRepository;
        _mapper = mapper;
        _dentistRepository = dentistRepository;
        _patientRepository = patientRepository;
    }

    public SchedulingDto Create(SchedulingCreateDto schedulingCreateDto)
    {
        var scheduling = _mapper.Map<Scheduling>(schedulingCreateDto);
        
        var patient = _patientRepository.GetById(schedulingCreateDto.PatientId);
        if (patient == null)
        {
            throw new Exception("Patient not found");
        }
        scheduling.Patient = patient;
        
        var dentist = _dentistRepository.GetById(schedulingCreateDto.DentistId);
        if (dentist == null)
        {
            throw new Exception("Dentist not found");
        }
        scheduling.Dentist = dentist;
        
        scheduling.Date = new DateOnly(schedulingCreateDto.Date.Year, schedulingCreateDto.Date.Month, schedulingCreateDto.Date.Day);
        
        return _mapper.Map<SchedulingDto>(_schedulingRepository.Save(scheduling));
    }

    public List<SchedulingDto> GetByInterval(DateDto start, DateDto end)
    {
        var startInterval = new DateOnly(start.Year, start.Month, start.Day);
        var endInterval = new DateOnly(end.Year, end.Month, end.Day);
        return _schedulingRepository.GetByInterval(startInterval, endInterval).Select(x => _mapper.Map<SchedulingDto>(x)).ToList();
    }

    public SchedulingDto GetById(Guid id)
    {
        var scheduling = _schedulingRepository.GetById(id);
        if (scheduling == null)
        {
            throw new Exception("Scheduling not found");
        }
        return _mapper.Map<SchedulingDto>(scheduling);
    }

    public void Delete(Guid id)
    {
        var scheduling = _schedulingRepository.GetById(id);
        if (scheduling == null)
        {
            throw new Exception("Scheduling not found");
        }
        _schedulingRepository.Delete(scheduling.Id);
    }
}