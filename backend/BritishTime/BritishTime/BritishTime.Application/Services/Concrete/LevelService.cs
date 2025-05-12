using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Levels;

namespace BritishTime.Application.Services.concrete;
public class LevelService : ILevelService
{
    private readonly IQueryLevelRepository _queryLevelRepository;
    private readonly ICommandLevelRepository _commandLevelRepository;
    private readonly IMapper _mapper;

    public LevelService(IQueryLevelRepository queryLevelRepository, ICommandLevelRepository commandLevelRepository, IMapper mapper)
    {
        _queryLevelRepository = queryLevelRepository;
        _commandLevelRepository = commandLevelRepository;
        _mapper = mapper;
    }

    public Task<PaginatedList<LevelDto>> GetAllAsync(LevelFilterDto filter, PageRequest pagination)
    {
        var result = _queryLevelRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<LevelDto> AddAsync(LevelCreateDto LevelDto)
    {
        var isExisting = await _queryLevelRepository.IsExistingAsync(x => x.Name == LevelDto.Name);

        if (isExisting)
            if (isExisting) throw new Exception("duplicateName");

        var Level = _mapper.Map<Level>(LevelDto);
        await _commandLevelRepository.AddAsync(Level);
        return _mapper.Map<LevelDto>(Level);
    }

    public async Task<LevelDto> UpdateAsync(LevelDto LevelDto)
    {
        var Level = await _queryLevelRepository.GetByIdAsync(LevelDto.Id);
        if (Level == null) throw new KeyNotFoundException("notFoundEntity");

        var isExisting = await _queryLevelRepository.IsExistingAsync(x => x.Id != LevelDto.Id && x.Name == LevelDto.Name);

        if (isExisting)
            if (isExisting) throw new Exception("duplicateName");

        var newLevel = _mapper.Map<Level>(LevelDto);

        await _commandLevelRepository.UpdateAsync(newLevel);

        return _mapper.Map<LevelDto>(newLevel);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var LevelDto = await _queryLevelRepository.GetByIdAsync(id);
        if (LevelDto == null) throw new KeyNotFoundException("notFoundEntity");

        var Level = _mapper.Map<Level>(LevelDto);

        await _commandLevelRepository.DeleteAsync(Level);

        return true;
    }
}
