using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Branches;
using BritishTime.Domain.Repositories.LessonScheduleDefinitions;

namespace BritishTime.Application.Services.concrete;
public class LessonScheduleDefinitionService : ILessonScheduleDefinitionService
{
    private readonly IQueryLessonScheduleDefinitionRepository _queryLessonScheduleDefinitionRepository;
    private readonly ICommandLessonScheduleDefinitionRepository _commandLessonScheduleDefinitionRepository;
    private readonly IQueryBranchRepository _branchRepository;
    private readonly IMapper _mapper;

    public LessonScheduleDefinitionService(IQueryLessonScheduleDefinitionRepository queryLessonScheduleDefinitionRepository, ICommandLessonScheduleDefinitionRepository commandLessonScheduleDefinitionRepository, IMapper mapper, IQueryBranchRepository branchRepository)
    {
        _queryLessonScheduleDefinitionRepository = queryLessonScheduleDefinitionRepository;
        _commandLessonScheduleDefinitionRepository = commandLessonScheduleDefinitionRepository;
        _mapper = mapper;
        _branchRepository = branchRepository;
    }

    public Task<PaginatedList<LessonScheduleDefinitionDto>> GetAllAsync(LessonScheduleDefinitionFilterDto filter, PageRequest pagination)
    {
        var result = _queryLessonScheduleDefinitionRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<LessonScheduleDefinitionDto> AddAsync(LessonScheduleDefinitionCreateDto lessonScheduleDefinitionDto)
    {
        try
        {
            var branch = await _branchRepository.GetByIdAsync(lessonScheduleDefinitionDto.BranchId);
            var lessonScheduleDefinition = _mapper.Map<LessonScheduleDefinition>(lessonScheduleDefinitionDto);
            lessonScheduleDefinition.StartTime = TimeOnly.Parse(lessonScheduleDefinitionDto.StartTime);

            lessonScheduleDefinition.DayCount = lessonScheduleDefinitionDto.Days.Count;

            var totalMinutes = (lessonScheduleDefinition.DayHour * branch.LessonDurationInMinutes)
                     + ((lessonScheduleDefinition.DayHour - 1) * branch.BreakDurationInMinutes);

            lessonScheduleDefinition.EndTime = lessonScheduleDefinition.StartTime.AddMinutes(totalMinutes);
            lessonScheduleDefinition.ScheduleCode = GenerateScheduleCode(lessonScheduleDefinitionDto);


            await _commandLessonScheduleDefinitionRepository.AddAsync(lessonScheduleDefinition);
            return _mapper.Map<LessonScheduleDefinitionDto>(lessonScheduleDefinition);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<LessonScheduleDefinitionDto> UpdateAsync(LessonScheduleDefinitionCreateDto LessonScheduleDefinitionDto)
    {
        try
        {
            if (LessonScheduleDefinitionDto.Id == null) throw new ArgumentNullException("notFoundEntity");

            var LessonScheduleDefinition = await _queryLessonScheduleDefinitionRepository.GetByIdAsync(LessonScheduleDefinitionDto.Id.Value);
            if (LessonScheduleDefinition == null) throw new KeyNotFoundException("notFoundEntity");

            var branch = await _branchRepository.GetByIdAsync(LessonScheduleDefinitionDto.BranchId);

            var newLessonScheduleDefinition = _mapper.Map<LessonScheduleDefinition>(LessonScheduleDefinitionDto);

            newLessonScheduleDefinition.StartTime = TimeOnly.Parse(LessonScheduleDefinitionDto.StartTime);

            newLessonScheduleDefinition.DayCount = newLessonScheduleDefinition.Days.Count;

            var totalMinutes = (newLessonScheduleDefinition.DayHour * branch.LessonDurationInMinutes)
                     + ((newLessonScheduleDefinition.DayHour - 1) * branch.BreakDurationInMinutes);

            newLessonScheduleDefinition.EndTime = newLessonScheduleDefinition.StartTime.AddMinutes(totalMinutes);
            newLessonScheduleDefinition.ScheduleCode = GenerateScheduleCode(LessonScheduleDefinitionDto);

            await _commandLessonScheduleDefinitionRepository.UpdateAsync(newLessonScheduleDefinition);

            return _mapper.Map<LessonScheduleDefinitionDto>(newLessonScheduleDefinition);
        }
        catch (Exception ex)
        {
            var t = ex.Message;
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var LessonScheduleDefinitionDto = await _queryLessonScheduleDefinitionRepository.GetByIdAsync(id);
        if (LessonScheduleDefinitionDto == null) throw new KeyNotFoundException("notFoundEntity");

        var LessonScheduleDefinition = _mapper.Map<LessonScheduleDefinition>(LessonScheduleDefinitionDto);

        await _commandLessonScheduleDefinitionRepository.DeleteAsync(LessonScheduleDefinition);

        return true;
    }

    public async Task<bool> ExistAsync(LessonScheduleDefinitionCreateDto lessonScheduleDefinition)
    {
        var exist = await _queryLessonScheduleDefinitionRepository
            .ExistsAsync(x => x.ScheduleCode == lessonScheduleDefinition.ScheduleCode && x.BranchId == lessonScheduleDefinition.BranchId
                    && (lessonScheduleDefinition.Id == null || x.Id != lessonScheduleDefinition.Id.Value));
        return exist;
    }

    public async Task<bool> ExistSchedule(string Schedule, Guid BranchId, Guid? Id = null)
    {
        var exist = await _queryLessonScheduleDefinitionRepository
             .ExistsAsync(x => x.Schedule == Schedule && x.BranchId == BranchId && (Id == null || x.Id != Id));
        return exist;
    }
    public string GenerateScheduleCode(LessonScheduleDefinitionCreateDto dto)
    {
        var days = string.Join(",", dto.Days.OrderBy(d => d));
        return $"{dto.ScheduleCategory}-{dto.EducationType}-{dto.StudentType}-{dto.StartTime}-{days}";
    }
}
