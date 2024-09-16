using AutoMapper;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Services;

public class ClinicalTreatmentService : IClinicalTreatmentService
{
    
    private readonly IClinicalTreatmentRepository _clinicalTreatmentRepository;
    private readonly IMapper _mapper;

    public ClinicalTreatmentService(IClinicalTreatmentRepository clinicalTreatmentRepository, IMapper mapper)
    {
        _clinicalTreatmentRepository = clinicalTreatmentRepository;
        _mapper = mapper;
    }

    public ClinicalTreatmentDto Create(ClinicalTreatmentDto clinicalTreatmentDto)
    {
        var clinicalTreatment = _mapper.Map<ClinicalTreatment>(clinicalTreatmentDto);
        var createdClinicalTreatment = _clinicalTreatmentRepository.Save(clinicalTreatment);
        return _mapper.Map<ClinicalTreatmentDto>(createdClinicalTreatment);
    }

    public ClinicalTreatmentDto Update(Guid id, ClinicalTreatmentDto clinicalTreatmentDto)
    {
        var clinicalTreatment = _clinicalTreatmentRepository.GetById(id);
        if (clinicalTreatment == null)
        {
            throw new Exception("Clinical Treatment not found");
        }
        
        clinicalTreatment.Name = clinicalTreatmentDto.Name;
        clinicalTreatment.Description = clinicalTreatmentDto.Description;
        clinicalTreatment.DefaultValue = clinicalTreatmentDto.DefaultValue;
        
        var updatedClinicalTreatment = _clinicalTreatmentRepository.Update(clinicalTreatment);
        return _mapper.Map<ClinicalTreatmentDto>(updatedClinicalTreatment);
    }

    public ClinicalTreatmentDto GetById(Guid id)
    {
        var clinicalTreatment = _clinicalTreatmentRepository.GetById(id);
        if (clinicalTreatment == null)
        {
            throw new Exception("Clinical Treatment not found");
        }
        
        return _mapper.Map<ClinicalTreatmentDto>(clinicalTreatment);
    }

    public List<ClinicalTreatmentDto> GetAll()
    {
        var clinicalTreatments = _clinicalTreatmentRepository.GetAll().ToList();
        return _mapper.Map<List<ClinicalTreatmentDto>>(clinicalTreatments);
    }

    public void Delete(Guid id)
    {
        var clinicalTreatment = _clinicalTreatmentRepository.GetById(id);
        if (clinicalTreatment == null)
        {
            throw new Exception("Clinical Treatment not found");
        }
        
        _clinicalTreatmentRepository.Delete(clinicalTreatment.Id);
    }
}