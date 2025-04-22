using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface ICampaignService
{
    Task<PaginatedList<CampaignDto>> GetAllAsync(CampaignFilterDto filter, PageRequest pagination);
    Task<CampaignDto> AddAsync(CampaignCreateDto CampaignDto);
    Task<CampaignDto> UpdateAsync(CampaignDto CampaignDto);
    Task<bool> DeleteAsync(Guid id);
}
