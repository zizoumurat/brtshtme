using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.IncentiveSettingsFeatures.Queries.GetAllIncentiveSettings;

public sealed record GetAllIncentiveSettingsQuery
    (IncentiveSettingFilterDto filter, PageRequest pagination) : IRequest<GetAllIncentiveSettingsQueryResponse>;
