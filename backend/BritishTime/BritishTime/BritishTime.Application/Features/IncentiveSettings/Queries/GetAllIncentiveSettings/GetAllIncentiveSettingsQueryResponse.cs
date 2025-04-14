
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.IncentiveSettingsFeatures.Queries.GetAllIncentiveSettings;
public sealed record GetAllIncentiveSettingsQueryResponse(PaginatedList<IncentiveSettingDto> result);
