using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.CrmRecordActionsFeatures.Queries.GetAllCrmRecordActions;

public sealed record GetAllCrmRecordActionsQuery
    (CrmRecordActionFilterDto filter, PageRequest pagination) : IRequest<GetAllCrmRecordActionsQueryResponse>;
