
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.CrmRecordsFeatures.Queries.GetAllCrmRecords;
public sealed record GetAllCrmRecordsQueryResponse(PaginatedList<CrmRecordDto> result);
