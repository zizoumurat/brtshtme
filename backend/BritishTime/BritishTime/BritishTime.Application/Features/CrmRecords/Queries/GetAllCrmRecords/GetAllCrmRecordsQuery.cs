using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.CrmRecordsFeatures.Queries.GetAllCrmRecords;

public sealed record GetAllCrmRecordsQuery
    (CrmRecordFilterDto filter, PageRequest pagination) : IRequest<GetAllCrmRecordsQueryResponse>;
