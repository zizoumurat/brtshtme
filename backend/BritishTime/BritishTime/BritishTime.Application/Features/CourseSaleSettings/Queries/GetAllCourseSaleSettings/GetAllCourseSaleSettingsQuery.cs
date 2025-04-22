using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.CourseSaleSettingsFeatures.Queries.GetAllCourseSaleSettings;

public sealed record GetAllCourseSaleSettingsQuery
    (CourseSaleSettingFilterDto filter, PageRequest pagination) : IRequest<GetAllCourseSaleSettingsQueryResponse>;
