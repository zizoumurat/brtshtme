using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.BranchPricingSettings;

namespace BritishTime.Application.Services.concrete;
public class BranchPricingSettingService : IBranchPricingSettingService
{
    private readonly IQueryBranchPricingSettingRepository _queryBranchPricingSettingRepository;
    private readonly ICommandBranchPricingSettingRepository _commandBranchPricingSettingRepository;
    private readonly IMapper _mapper;

    public BranchPricingSettingService(IQueryBranchPricingSettingRepository queryBranchPricingSettingRepository, ICommandBranchPricingSettingRepository commandBranchPricingSettingRepository, IMapper mapper)
    {
        _queryBranchPricingSettingRepository = queryBranchPricingSettingRepository;
        _commandBranchPricingSettingRepository = commandBranchPricingSettingRepository;
        _mapper = mapper;
    }

    public async Task<BranchPricingSettingDto> AddOrUpdateAsync(BranchPricingSettingDto dto)
    {
        var entity = _mapper.Map<BranchPricingSetting>(dto);

        if (dto.Id is null)
        {
            await _commandBranchPricingSettingRepository.AddAsync(entity);
            return _mapper.Map<BranchPricingSettingDto>(entity);
        }

        await _commandBranchPricingSettingRepository.UpdateAsync(entity);
        return _mapper.Map<BranchPricingSettingDto>(entity);
    }

    public async Task<BranchPricingSettingDto> GetBranchPricingSettingByBranchId(Guid BranchId)
    {
        var setting = await _queryBranchPricingSettingRepository.GetByBranchId(BranchId);

        return setting;
    }
}
