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
    }
}