
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.CrmRecordsFeatures.Queries.GetById;
public sealed record GetByIdQueryResponse(CrmRecordDto result);
