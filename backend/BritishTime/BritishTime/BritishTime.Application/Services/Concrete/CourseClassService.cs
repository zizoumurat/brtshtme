using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Branches;
using BritishTime.Domain.Repositories.CourseClasses;
using BritishTime.Domain.Repositories.LessonScheduleDefinitions;
using BritishTime.Domain.Repositories.LessonSessions;
using BritishTime.Domain.Repositories.LessonSessionTemplates;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Application.Services.concrete;

public class CourseClassService : ICourseClassService
{
    private readonly IHolidayService _holidayService;
    private readonly IQueryCourseClassRepository _queryCourseClassRepository;
    private readonly ICommandCourseClassRepository _commandCourseClassRepository;
    private readonly IQueryLessonScheduleDefinitionRepository _queryLessonScheduleDefinitionRepository;
    private readonly IQueryBranchRepository _queryBranchRepository;
    private readonly ICommandLessonSessionRepository _commandLessonSessionRepository;
    private readonly ICommandLessonSessionTemplateRepository _commandLessonSessionTemplateRepository;
    private readonly IQueryLessonSessionRepository _queryLessonSessionRepository;
    private readonly IMapper _mapper;

    public CourseClassService(IQueryCourseClassRepository queryCourseClassRepository, ICommandCourseClassRepository commandCourseClassRepository, IMapper mapper, IQueryLessonScheduleDefinitionRepository queryLessonScheduleDefinitionRepository, IQueryBranchRepository queryBranchRepository, IHolidayService holidayService, ICommandLessonSessionRepository commandLessonSessionRepository, ICommandLessonSessionTemplateRepository commandLessonSessionTemplateRepository, IQueryLessonSessionRepository queryLessonSessionRepository)
    {
        _queryCourseClassRepository = queryCourseClassRepository;
        _commandCourseClassRepository = commandCourseClassRepository;
        _mapper = mapper;
        _queryLessonScheduleDefinitionRepository = queryLessonScheduleDefinitionRepository;
        _queryBranchRepository = queryBranchRepository;
        _holidayService = holidayService;
        _commandLessonSessionRepository = commandLessonSessionRepository;
        _commandLessonSessionTemplateRepository = commandLessonSessionTemplateRepository;
        _queryLessonSessionRepository = queryLessonSessionRepository;
    }

    public Task<PaginatedList<CourseClassDto>> GetAllAsync(CourseClassFilterDto filter, PageRequest pagination)
    {
        if (filter == null)
        {
            filter = new CourseClassFilterDto();
        }

        var result = _queryCourseClassRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<CourseClassDto> AddAsync(CourseClassCreateDto courseClassDto)
    {
        var isExisting = await _queryCourseClassRepository.IsExistingAsync(x => x.Name == courseClassDto.Name);

        if (isExisting) throw new Exception("duplicateName");

        var lessonSchedule = await _queryLessonScheduleDefinitionRepository.GetByIdAsync(courseClassDto.LessonScheduleDefinitionId);

        if (lessonSchedule == null)
            throw new KeyNotFoundException("notFoundEntity");

        var isConflict = await this.IsClassroomScheduleConflictAsync(
            courseClassDto.ClassroomId ?? Guid.Empty,
            courseClassDto.StartDate,
            courseClassDto.EndDate,
            lessonSchedule.Days,
            lessonSchedule.StartTime,
            lessonSchedule.DayHour
        );

        if (isConflict) throw new Exception("duplicateRoom");

        var courseClass = _mapper.Map<CourseClass>(courseClassDto);
        await _commandCourseClassRepository.AddAsync(courseClass);
        return _mapper.Map<CourseClassDto>(courseClass);
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
        if (lessonSchedule == null)
            throw new KeyNotFoundException("notFoundEntity");

        var branch = await _queryBranchRepository.GetByIdAsync(lessonSchedule.BranchId);
        if (branch == null)
            throw new KeyNotFoundException("branchNotFound");

        int totalHours = branch.LevelDurationInHours;
        int hoursPerDay = lessonSchedule.DayHour;
        var lessonDays = lessonSchedule.Days.Distinct().ToList();

        if (!lessonDays.Contains(startDate.DayOfWeek))
            throw new InvalidOperationException("startDateNotInProgramDays");

        int year = startDate.Year;

        var holidays = await _holidayService.GetHolidaysAsync(year);

        var courseHolidays = await _holidayService.GetCourseHolidaysAsync(branch.Id, Guid.Empty, startDate);

        holidays = holidays.Concat(courseHolidays).Distinct().ToList();

        // Eğer ders yılı iki takvim yılına yayılıyorsa, sonraki yılın tatillerini de al
        if (startDate.Month == 12 || totalHours / hoursPerDay > 20)
        {
            holidays.AddRange(await _holidayService.GetHolidaysAsync(year + 1));
        }

        int hoursLeft = totalHours;
        DateTime currentDate = startDate;

        while (hoursLeft > 0)
        {
            bool isLessonDay = lessonDays.Contains(currentDate.DayOfWeek);
            bool isHoliday = holidays.Contains(currentDate.Date);

            if (isLessonDay && !isHoliday)
            {
                hoursLeft -= hoursPerDay;
            }

            if (hoursLeft <= 0)
                break;

            currentDate = currentDate.AddDays(1);
        }

        return currentDate;
    }

    public async Task<bool> IsClassroomScheduleConflictAsync(Guid classroomId, DateTime newStartDate, DateTime newEndDate, List<DayOfWeek> newLessonDays, TimeOnly newStartTime, int dayHour)
    {
        var existingClasses = await _queryCourseClassRepository.GetList(x => x.ClassroomId == classroomId && x.StartDate <= newEndDate && x.EndDate >= newStartDate).ToListAsync();

        TimeOnly newEndTime = newStartTime.Add(TimeSpan.FromHours(dayHour));

        foreach (var existingClass in existingClasses)
        {
            var existingSchedule = await _queryLessonScheduleDefinitionRepository
                .GetByIdAsync(existingClass.LessonScheduleDefinitionId);

            var existingLessonDays = existingSchedule.Days;
            var existingStartTime = existingSchedule.StartTime; // TimeOnly
            var existingEndTime = existingStartTime.Add(TimeSpan.FromHours(existingSchedule.DayHour));

            // 1. Gün çakışması var mı?
            var commonDays = existingLessonDays.Intersect(newLessonDays);
            if (!commonDays.Any()) continue;

            // 2. Saat çakışması var mı?
            bool timeConflict = newStartTime < existingEndTime && newEndTime > existingStartTime;

            // 3. Tarih aralığı çakışıyor mu?
            bool dateConflict = newStartDate <= existingClass.EndDate && newEndDate >= existingClass.StartDate;

            if (timeConflict && dateConflict)
                return true;
        }

        return false;
    }

    public async Task<List<LessonSession>> GenerateLessonSessionAsync(Guid classId, Dictionary<DayOfWeek, Guid> programDaysWithTeachers)
    {
        try
        {
            var classroom = await _queryCourseClassRepository.GetByIdAsync(classId);
            var branch = await _queryBranchRepository.GetByIdAsync(classroom.BranchId);
            var holidays = await _holidayService.GetHolidaysAsync(classroom.StartDate.Year);
            var lessonSchedule = await _queryLessonScheduleDefinitionRepository.GetByIdAsync(classroom.LessonScheduleDefinitionId);

            var result = new List<LessonSession>();
            var currentDate = classroom.StartDate;

            var templateList = programDaysWithTeachers
                .Select(item => new LessonSessionTemplate
                {
                    Id = Guid.NewGuid(),
                    CourseClassId = classId,
                    TeacherId = item.Value,
                    Day = item.Key
                }).ToList();

            while (currentDate <= classroom.EndDate)
            {
                if (programDaysWithTeachers.TryGetValue(currentDate.DayOfWeek, out Guid teacherId) &&
                    !holidays.Contains(currentDate.Date))
                {
                    TimeOnly currentLessonStartTime = lessonSchedule.StartTime;

                    for (int i = 0; i < lessonSchedule.DayHour; i++)
                    {
                        TimeOnly lessonEndTime = currentLessonStartTime.AddMinutes(branch.LessonDurationInMinutes);

                        result.Add(new LessonSession
                        {
                            Id = Guid.NewGuid(),
                            CourseClasId = classId,
                            Date = currentDate,
                            StartTime = currentLessonStartTime,
                            EndTime = lessonEndTime,
                            TeacherId = teacherId,
                        });

                        // Son ders değilse, bir de ara ekle
                        if (i < lessonSchedule.DayHour - 1)
                        {
                            currentLessonStartTime = lessonEndTime.AddMinutes(branch.BreakDurationInMinutes);
                        }
                    }
                }

                currentDate = currentDate.AddDays(1);
            }

            await _commandLessonSessionTemplateRepository.AddRangeAsync(templateList);
            await _commandLessonSessionRepository.AddRangeAsync(result);

            return result;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<List<LessonSessionListDto>> GetLessonSessionListByCourseClass(Guid courseClassId)
    {
        var lessonSessionList = await _queryLessonSessionRepository
            .GetList(x => x.CourseClasId == courseClassId)
            .Include(x => x.Teacher)
            .ToListAsync();

        var result = lessonSessionList
            .OrderBy(x => x.Date)
            .Select(x =>
            {
                var startDateTime = x.Date + x.StartTime.ToTimeSpan();
                var endDateTime = x.Date + x.EndTime.ToTimeSpan();

                return new LessonSessionListDto(
                    Title: x.Teacher.FirstName + " " + x.Teacher.LastName,
                    Start: startDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    End: endDateTime.ToString("yyyy-MM-ddTHH:mm:ss")
                );
            })
            .ToList();

        return result;
    }

}
