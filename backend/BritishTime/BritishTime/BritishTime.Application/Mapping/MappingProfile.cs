using AutoMapper;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;

namespace BritishTime.Application.Mapping;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BranchDto, Branch>().ReverseMap();
        CreateMap<BranchCreateDto, Branch>();

        CreateMap<LessonScheduleDefinitionDto, LessonScheduleDefinition>().ReverseMap();
        CreateMap<LessonScheduleDefinitionCreateDto, LessonScheduleDefinition>();

        CreateMap<RegionDto, Region>().ReverseMap();
        CreateMap<RegionCreateDto, Region>();

        CreateMap<IncentiveSettingDto, IncentiveSetting>().ReverseMap();
        CreateMap<IncentiveSettingCreateDto, IncentiveSetting>();
    }
}
