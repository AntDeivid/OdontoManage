using AutoMapper;
using OdontoManage.Application.Models.DTOs;
using OdontoManage.Core.Models;

namespace OdontoManage.Application.Util;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<User, UserDto>()
            .ReverseMap();

        CreateMap<UserCreateDto, User>();

        CreateMap<Patient, PatientDto>()
            .ReverseMap();

        CreateMap<PatientCreateDto, Patient>()
            .ForMember(destinationMember => destinationMember.BirthDay, opt => opt.Ignore());
        
        CreateMap<Dentist, DentistDto>()
            .ReverseMap();

        CreateMap<DentistCreateDto, Dentist>();

        CreateMap<DentistUpdateDto, Dentist>();

        CreateMap<Treatment, TreatmentDto>()
            .ForMember(destinationMember => destinationMember.InstallmentDueDate, opt => opt.Ignore())
            .ForMember(destinationMember => destinationMember.PatientId, opt => opt.MapFrom(src => src.Patient.Id))
            .ForMember(destinationMember => destinationMember.DentistId, opt => opt.MapFrom(src => src.Dentist.Id))
            .ForMember(destinationMember => destinationMember.ClinicalTreatmentId, opt => opt.MapFrom(src => src.ClinicalTreatment.Id))
            .ReverseMap()
            .ForMember(src => src.Patient, opt => opt.Ignore())
            .ForMember(src => src.Dentist, opt => opt.Ignore())
            .ForMember(src => src.ClinicalTreatment, opt => opt.Ignore());

        CreateMap<TreatmentCreateDto, Treatment>()
            .ForMember(destinationMember => destinationMember.InstallmentDueDate, opt => opt.Ignore());

        CreateMap<ClinicalTreatment, ClinicalTreatmentDto>()
            .ReverseMap();

        CreateMap<Address, AddressDto>().ReverseMap();
        
        CreateMap<Item, ItemDto>().ReverseMap();
        
        // Mapeamento entre PatientCreateDto e Patient
        CreateMap<PatientCreateDto, Patient>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.BirthDay, opt => opt.Ignore());
        // Mapeamento entre AddressCreateDto e Address
        CreateMap<AddressCreateDto, Address>();

        CreateMap<ItemCreateDto, Item>()
            .ForMember(destinationMember => destinationMember.Price, opt => opt.Ignore());

        CreateMap<ExpenseCreateDto, Expense>()
            .ForMember(destinationMember => destinationMember.InstallmentDueDate, opt => opt.Ignore())
            .ForMember(destinationMember => destinationMember.PaymentDate, opt => opt.Ignore());
        
        CreateMap<Expense, ExpenseDto>()
            .ReverseMap();
        
        CreateMap<ExpenseUpdateDto, Expense>()
            .ForMember(destinationMember => destinationMember.InstallmentDueDate, opt => opt.Ignore())
            .ForMember(destinationMember => destinationMember.PaymentDate, opt => opt.Ignore());

        CreateMap<ClinicalTreatment, ClinicalTreatmentDto>()
            .ReverseMap();
    }
}
