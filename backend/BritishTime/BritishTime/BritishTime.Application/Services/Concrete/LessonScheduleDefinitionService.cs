using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.LessonScheduleDefinitions;

namespace BritishTime.Application.Services.concrete;
public class LessonScheduleDefinitionService : ILessonScheduleDefinitionService
{
    private readonly IQueryLessonScheduleDefinitionRepository _queryLessonScheduleDefinitionRepository;
    private readonly ICommandLessonScheduleDefinitionRepository _commandLessonScheduleDefinitionRepository;
    private readonly IMapper _mapper;

    public LessonScheduleDefinitionService(IQueryLessonScheduleDefinitionRepository queryLessonScheduleDefinitionRepository, ICommandLessonScheduleDefinitionRepository commandLessonScheduleDefinitionRepository, IMapper mapper)
    {
        _queryLessonScheduleDefinitionRepository = queryLessonScheduleDefinitionRepository;
        _commandLessonScheduleDefinitionRepository = commandLessonScheduleDefinitionRepository;
        _mapper = mapper;
    }

    public Task<PaginatedList<LessonScheduleDefinitionDto>> GetAllAsync(LessonScheduleDefinitionFilterDto filter, PageRequest pagination)
    {
        var result = _queryLessonScheduleDefinitionRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<LessonScheduleDefinitionDto> AddAsync(LessonScheduleDefinitionCreateDto LessonScheduleDefinitionDto)
    {
        var LessonScheduleDefinition = _mapper.Map<LessonScheduleDefinition>(LessonScheduleDefinitionDto);
        await _commandLessonScheduleDefinitionRepository.AddAsync(LessonScheduleDefinition);
        return _mapper.Map<LessonScheduleDefinitionDto>(LessonScheduleDefinition);
    }

    public async Task<LessonScheduleDefinitionDto> UpdateAsync(LessonScheduleDefinitionDto LessonScheduleDefinitionDto)
    {
        var LessonScheduleDefinition = await _queryLessonScheduleDefinitionRepository.GetByIdAsync(LessonScheduleDefinitionDto.Id);
        if (LessonScheduleDefinition == null) throw new KeyNotFoundException("notFoundEntity");

        var newLessonScheduleDefinition = _mapper.Map<LessonScheduleDefinition>(LessonScheduleDefinitionDto);

        await _commandLessonScheduleDefinitionRepository.UpdateAsync(newLessonScheduleDefinition);

        return _mapper.Map<LessonScheduleDefinitionDto>(newLessonScheduleDefinition);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var LessonScheduleDefinitionDto = await _queryLessonScheduleDefinitionRepository.GetByIdAsync(id);
        if (LessonScheduleDefinitionDto == null) throw new KeyNotFoundException("notFoundEntity");

        var LessonScheduleDefinition = _mapper.Map<LessonScheduleDefinition>(LessonScheduleDefinitionDto);

        await _commandLessonScheduleDefinitionRepository.DeleteAsync(LessonScheduleDefinition);

        return true;
    }
}
