using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Domain.Repositories.Campaigns;

public interface IQueryCampaignRepository
{
    Task<PaginatedList<CampaignDto>> GetAllAsync(CampaignFilterDto filter, PageRequest pagination);
    Task<CampaignDto> GetByIdAsync(Guid id);
}
