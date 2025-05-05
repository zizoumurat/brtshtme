
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetListByCrmRecord;
public sealed record GetListByCrmRecordQueryResponse(IList<CrmRecordActionDto> result);
