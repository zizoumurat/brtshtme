using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Branches;
using BritishTime.Domain.Repositories.CourseClasses;
using BritishTime.Domain.Repositories.LessonScheduleDefinitions;

namespace BritishTime.Application.Services.concrete;
public class CourseClassService : ICourseClassService
{
    private readonly IQueryCourseClassRepository _queryCourseClassRepository;
    private readonly ICommandCourseClassRepository _commandCourseClassRepository;
    private readonly IQueryLessonScheduleDefinitionRepository _queryLessonScheduleDefinitionRepository;
    private readonly IQueryBranchRepository _queryBranchRepository;
    private readonly IMapper _mapper;

    public CourseClassService(IQueryCourseClassRepository queryCourseClassRepository, ICommandCourseClassRepository commandCourseClassRepository, IMapper mapper, IQueryLessonScheduleDefinitionRepository queryLessonScheduleDefinitionRepository, IQueryBranchRepository queryBranchRepository)
    {
        _queryCourseClassRepository = queryCourseClassRepository;
        _commandCourseClassRepository = commandCourseClassRepository;
        _mapper = mapper;
        _queryLessonScheduleDefinitionRepository = queryLessonScheduleDefinitionRepository;
        _queryBranchRepository = queryBranchRepository;
    }

    public Task<PaginatedList<CourseClassDto>> GetAllAsync(CourseClassFilterDto filter, PageRequest pagination)
    {
        if(filter == null)
        {
            filter = new CourseClassFilterDto();
        }

        var result = _queryCourseClassRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<CourseClassDto> AddAsync(CourseClassCreateDto CourseClassDto)
    {
        var isExisting = await _queryCourseClassRepository.IsExistingAsync(x => x.Name == CourseClassDto.Name);

        if (isExisting)
            if (isExisting) throw new Exception("duplicateName");

        var CourseClass = _mapper.Map<CourseClass>(CourseClassDto);
        await _commandCourseClassRepository.AddAsync(CourseClass);
        return _mapper.Map<CourseClassDto>(CourseClass);
    }

    public async Task<CourseClassDto> UpdateAsync(CourseClassDto CourseClassDto)
    {
        var CourseClass = await _queryCourseClassRepository.GetByIdAsync(CourseClassDto.Id);
        if (CourseClass == null) throw new KeyNotFoundException("notFoundEntity");

        var isExisting = await _queryCourseClassRepository.IsExistingAsync(x => x.Id != CourseClassDto.Id && x.Name == CourseClassDto.Name);

        if (isExisting)
            if (isExisting) throw new Exception("duplicateName");

        var newCourseClass = _mapper.Map<CourseClass>(CourseClassDto);

        await _commandCourseClassRepository.UpdateAsync(newCourseClass);

        return _mapper.Map<CourseClassDto>(newCourseClass);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var CourseClassDto = await _queryCourseClassRepository.GetByIdAsync(id);
        if (CourseClassDto == null) throw new KeyNotFoundException("notFoundEntity");

        var CourseClass = _mapper.Map<CourseClass>(CourseClassDto);

        await _commandCourseClassRepository.DeleteAsync(CourseClass);

        return true;
    }

    public async Task<DateTime> CalculateEndDate(DateTime startDate, Guid lessonScheduleId)
    {
        var lessonSchedule = await _queryLessonScheduleDefinitionRepository.GetByIdAsync(lessonScheduleId);
        if (lessonSchedule == null) throw new KeyNotFoundException("notFoundEntity");

        var branch = await _queryBranchRepository.GetByIdAsync(lessonSchedule.BranchId);
        if (branch == null) throw new KeyNotFoundException("branchNotFound");

        int totalHours = branch.LevelDurationInHours;
        int hoursPerDay = lessonSchedule.DayHour;
        var lessonDays = lessonSchedule.Days.Distinct().ToList(); // örn: [Saturday, Sunday]

        if (!lessonDays.Contains(startDate.DayOfWeek))
            throw new InvalidOperationException("startDateNotInProgramDays");

        int hoursLeft = totalHours;
        DateTime currentDate = startDate;

        while (hoursLeft > 0)
        {
            // Eğer bugünkü gün ders günü ise saat düş
            if (lessonDays.Contains(currentDate.DayOfWeek))
            {
                hoursLeft -= hoursPerDay;
            }

            // Eğer bu gün dersle birlikte saatler tamamlandıysa, bu gün son ders günü
            if (hoursLeft <= 0)
                break;

            currentDate = currentDate.AddDays(1);
        }

        return currentDate;
    }

}
