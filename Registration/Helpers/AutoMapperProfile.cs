using System;
using AutoMapper;
using Registration.Domain;
using Registration.DTO;

namespace Registration.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

            CreateMap<AddEmployeeDTO, Employee>()
                .ForMember(dest => dest.SkillId, opt => opt.MapFrom(src => Convert.ToInt32(src.SkillId))).ReverseMap();

            CreateMap<SkillDTO, EmployeeDTO>();
        }
    }
}
