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

        CreateMap<BranchPricingSettingDto, BranchPricingSetting>().ReverseMap();

        CreateMap<CampaignDto, Campaign>().ReverseMap();
        CreateMap<CampaignCreateDto, Campaign>();

        CreateMap<CourseSaleSettingDto, CourseSaleSetting>().ReverseMap();
        CreateMap<CourseSaleSettingCreateDto, CourseSaleSetting>();

        CreateMap<DiscountDto, Discount>().ReverseMap();
        CreateMap<DiscountCreateDto, Discount>();

        CreateMap<LessonScheduleDefinitionDto, LessonScheduleDefinition>().ReverseMap();
        CreateMap<LessonScheduleDefinitionCreateDto, LessonScheduleDefinition>()
            .ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.Days.Select(d => (DayOfWeek)d).ToList()))
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.StartTime)))
            .ForMember(dest => dest.EndTime, opt => opt.Ignore());

        CreateMap<RegionDto, Region>().ReverseMap();
        CreateMap<RegionCreateDto, Region>();

        CreateMap<IncentiveSettingDto, IncentiveSetting>().ReverseMap();
        CreateMap<IncentiveSettingCreateDto, IncentiveSetting>();

        CreateMap<InstallmentSettingDto, InstallmentSetting>().ReverseMap();
        CreateMap<InstallmentSettingCreateDto, InstallmentSetting>();
    }
}
