using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetListByCrmRecord;

public sealed record GetListByCrmRecordQuery(Guid CrmRecordId) : IRequest<GetListByCrmRecordQueryResponse>;
