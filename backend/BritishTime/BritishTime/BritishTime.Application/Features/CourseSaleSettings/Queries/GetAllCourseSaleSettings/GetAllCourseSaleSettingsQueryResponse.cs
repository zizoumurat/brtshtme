
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.CourseSaleSettingsFeatures.Queries.GetAllCourseSaleSettings;
public sealed record GetAllCourseSaleSettingsQueryResponse(PaginatedList<CourseSaleSettingDto> result);
