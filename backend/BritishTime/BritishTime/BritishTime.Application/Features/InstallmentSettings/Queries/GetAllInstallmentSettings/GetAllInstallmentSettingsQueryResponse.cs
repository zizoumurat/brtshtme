
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.InstallmentSettingsFeatures.Queries.GetAllInstallmentSettings;
public sealed record GetAllInstallmentSettingsQueryResponse(PaginatedList<InstallmentSettingDto> result);
