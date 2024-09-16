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

        CreateMap<Treatment, TreatmentDto>()
            .ReverseMap();

        CreateMap<TreatmentCreateDto, Treatment>();

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
    }
}
