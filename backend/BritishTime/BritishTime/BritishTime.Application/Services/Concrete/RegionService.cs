using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Regions;

namespace BritishTime.Application.Services.concrete;
public class RegionService : IRegionService
{
    private readonly IQueryRegionRepository _queryRegionRepository;
    private readonly ICommandRegionRepository _commandRegionRepository;
    private readonly IMapper _mapper;

    public RegionService(IQueryRegionRepository queryRegionRepository, ICommandRegionRepository commandRegionRepository, IMapper mapper)
    {
        _queryRegionRepository = queryRegionRepository;
        _commandRegionRepository = commandRegionRepository;
        _mapper = mapper;
    }

    public Task<PaginatedList<RegionDto>> GetAllAsync(RegionFilterDto filter, PageRequest pagination)
    {
        var result = _queryRegionRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<RegionDto> AddAsync(RegionCreateDto RegionDto)
    {
        var Region = _mapper.Map<Region>(RegionDto);
        await _commandRegionRepository.AddAsync(Region);
        return _mapper.Map<RegionDto>(Region);
    }

    public async Task<RegionDto> UpdateAsync(RegionDto RegionDto)
    {
        var Region = await _queryRegionRepository.GetByIdAsync(RegionDto.Id);
        if (Region == null) throw new KeyNotFoundException("notFoundEntity");

        var newRegion = _mapper.Map<Region>(RegionDto);

        await _commandRegionRepository.UpdateAsync(newRegion);

        return _mapper.Map<RegionDto>(newRegion);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var RegionDto = await _queryRegionRepository.GetByIdAsync(id);
        if (RegionDto == null) throw new KeyNotFoundException("notFoundEntity");

        var Region = _mapper.Map<Region>(RegionDto);

        await _commandRegionRepository.DeleteAsync(Region);

        return true;
    }

    public async Task<List<SelectListDto>> GetListAsync()
    {
        var result = await _queryRegionRepository.GetListAsync();

        return result;
    }
}
