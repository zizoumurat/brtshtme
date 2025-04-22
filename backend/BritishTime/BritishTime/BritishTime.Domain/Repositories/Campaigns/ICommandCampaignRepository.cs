using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.Campaigns;

public interface ICommandCampaignRepository
{
    Task AddAsync(Campaign Campaign);
    Task UpdateAsync(Campaign Campaign);
    Task DeleteAsync(Campaign Campaign);
}