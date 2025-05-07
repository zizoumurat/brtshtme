
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.CrmRecordActionsFeatures.Queries.GetAllCrmRecordActions;
public sealed record GetAllCrmRecordActionsQueryResponse(PaginatedList<CrmRecordActionDto> result);
