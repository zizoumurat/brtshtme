using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.CrmRecordsFeatures.Queries.GetById;

public sealed record GetByIdQuery(Guid Id) : IRequest<GetByIdQueryResponse>;
