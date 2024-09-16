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
            .ReverseMap();

        CreateMap<TreatmentCreateDto, Treatment>()
            .ForMember(destinationMember => destinationMember.InstallmentDueDate, opt => opt.Ignore());

        CreateMap<ClinicalTreatment, ClinicalTreatmentDto>()
            .ReverseMap();
        
        CreateMap<Item, ItemDto>().ReverseMap();

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
    }
}