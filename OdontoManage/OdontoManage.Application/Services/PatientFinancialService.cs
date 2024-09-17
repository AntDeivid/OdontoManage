using AutoMapper;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Interfaces;

namespace OdontoManage.Application.Services;

public class PatientFinancialService : IPatientFinancialService
{
    private readonly IMapper _mapper;
    private readonly ITreatmentRepository _treatmentRepository;

    public PatientFinancialService(IMapper mapper, ITreatmentRepository treatmentRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _treatmentRepository = treatmentRepository ?? throw new ArgumentNullException(nameof(treatmentRepository));
    }

    public List<TreatmentDto> GetTreatmentsPaied(Guid id)
    { 
        var result = new List<TreatmentDto>();
        var allTreatments = _treatmentRepository.GetAll();
        if(allTreatments == null){throw new Exception("treatments are null");}
        var allTreatmentsDtos = _mapper.Map<List<TreatmentDto>>(allTreatments);

        if (allTreatmentsDtos == null)
        {
           throw new Exception("treatmentsDto are null");
        }

        foreach (var treatmentDto in allTreatmentsDtos)
        {
            if (treatmentDto.PatientId == id && treatmentDto.Paid)
            {
                result.Add(treatmentDto);
            }    
        }
        
        return result;
    }

    public List<TreatmentDto> GetTreatmentsNotPaied(Guid id)
    {
        var result = new List<TreatmentDto>();
        var allTreatments = _treatmentRepository.GetAll();
        if(allTreatments == null){throw new Exception("treatments are null");}
        var allTreatmentsDtos = _mapper.Map<List<TreatmentDto>>(allTreatments);

        if (allTreatmentsDtos == null)
        {
            throw new Exception("treatmentsDto are null");
        }

        foreach (var treatmentDto in allTreatmentsDtos)
        {
            if (treatmentDto.PatientId == id && !treatmentDto.Paid)
            {
                result.Add(treatmentDto);
            }    
        }
        
        return result;
    }
}