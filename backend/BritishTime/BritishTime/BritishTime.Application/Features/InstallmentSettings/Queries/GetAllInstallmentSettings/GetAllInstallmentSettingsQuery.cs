using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.InstallmentSettingsFeatures.Queries.GetAllInstallmentSettings;

public sealed record GetAllInstallmentSettingsQuery
    (InstallmentSettingFilterDto filter, PageRequest pagination) : IRequest<GetAllInstallmentSettingsQueryResponse>;
