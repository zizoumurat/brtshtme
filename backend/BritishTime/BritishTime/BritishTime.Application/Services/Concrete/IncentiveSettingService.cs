using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.IncentiveSettings;

namespace BritishTime.Application.Services.concrete;
public class IncentiveSettingService : IIncentiveSettingService
{
    private readonly IQueryIncentiveSettingRepository _queryIncentiveSettingRepository;
    private readonly ICommandIncentiveSettingRepository _commandIncentiveSettingRepository;
    private readonly IMapper _mapper;

    public IncentiveSettingService(IQueryIncentiveSettingRepository queryIncentiveSettingRepository, ICommandIncentiveSettingRepository commandIncentiveSettingRepository, IMapper mapper)
    {
        _queryIncentiveSettingRepository = queryIncentiveSettingRepository;
        _commandIncentiveSettingRepository = commandIncentiveSettingRepository;
        _mapper = mapper;
    }

    public Task<PaginatedList<IncentiveSettingDto>> GetAllAsync(IncentiveSettingFilterDto filter, PageRequest pagination)
    {
        var result = _queryIncentiveSettingRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<IncentiveSettingDto> AddAsync(IncentiveSettingCreateDto IncentiveSettingDto)
    {
        var IncentiveSetting = _mapper.Map<IncentiveSetting>(IncentiveSettingDto);
        await _commandIncentiveSettingRepository.AddAsync(IncentiveSetting);
        return _mapper.Map<IncentiveSettingDto>(IncentiveSetting);
    }

    public async Task<IncentiveSettingDto> UpdateAsync(IncentiveSettingDto IncentiveSettingDto)
    {
        var IncentiveSetting = await _queryIncentiveSettingRepository.GetByIdAsync(IncentiveSettingDto.Id);
        if (IncentiveSetting == null) throw new KeyNotFoundException("notFoundEntity");

        var newIncentiveSetting = _mapper.Map<IncentiveSetting>(IncentiveSettingDto);

        await _commandIncentiveSettingRepository.UpdateAsync(newIncentiveSetting);

        return _mapper.Map<IncentiveSettingDto>(newIncentiveSetting);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var IncentiveSettingDto = await _queryIncentiveSettingRepository.GetByIdAsync(id);
        if (IncentiveSettingDto == null) throw new KeyNotFoundException("notFoundEntity");

        var IncentiveSetting = _mapper.Map<IncentiveSetting>(IncentiveSettingDto);

        await _commandIncentiveSettingRepository.DeleteAsync(IncentiveSetting);

        return true;
    }
}
