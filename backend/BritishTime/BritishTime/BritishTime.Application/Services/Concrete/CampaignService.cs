using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Campaigns;

namespace BritishTime.Application.Services.concrete;
public class CampaignService : ICampaignService
{
    private readonly IQueryCampaignRepository _queryCampaignRepository;
    private readonly ICommandCampaignRepository _commandCampaignRepository;
    private readonly IMapper _mapper;

    public CampaignService(IQueryCampaignRepository queryCampaignRepository, ICommandCampaignRepository commandCampaignRepository, IMapper mapper)
    {
        _queryCampaignRepository = queryCampaignRepository;
        _commandCampaignRepository = commandCampaignRepository;
        _mapper = mapper;
    }

    public Task<PaginatedList<CampaignDto>> GetAllAsync(CampaignFilterDto filter, PageRequest pagination)
    {
        var result = _queryCampaignRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<CampaignDto> AddAsync(CampaignCreateDto CampaignDto)
    {
        var Campaign = _mapper.Map<Campaign>(CampaignDto);
        await _commandCampaignRepository.AddAsync(Campaign);
        return _mapper.Map<CampaignDto>(Campaign);
    }

    public async Task<CampaignDto> UpdateAsync(CampaignDto CampaignDto)
    {
        var Campaign = await _queryCampaignRepository.GetByIdAsync(CampaignDto.Id);
        if (Campaign == null) throw new KeyNotFoundException("notFoundEntity");

        var newCampaign = _mapper.Map<Campaign>(CampaignDto);

        await _commandCampaignRepository.UpdateAsync(newCampaign);

        return _mapper.Map<CampaignDto>(newCampaign);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var CampaignDto = await _queryCampaignRepository.GetByIdAsync(id);
        if (CampaignDto == null) throw new KeyNotFoundException("notFoundEntity");

        var Campaign = _mapper.Map<Campaign>(CampaignDto);

        await _commandCampaignRepository.DeleteAsync(Campaign);

        return true;
    }
}
