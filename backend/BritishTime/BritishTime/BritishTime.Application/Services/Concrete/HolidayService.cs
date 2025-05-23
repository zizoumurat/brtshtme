using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Holidays;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace BritishTime.Application.Services.Concrete;

public class HolidayService : IHolidayService
{
    private readonly HttpClient _httpClient;
    private const string CalendarId = "tr.turkish%23holiday%40group.v.calendar.google.com";
    private readonly string _apiKey;
    //"ApiKey": "AIzaSyBWqjIYXNsPZN8VsRj7_gg7mv2QWJHj-ps"
    private readonly IQueryHolidayRepository _queryHolidayRepository;

    public HolidayService(HttpClient httpClient, IConfiguration configuration, IQueryHolidayRepository queryHolidayRepository)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

        _apiKey = configuration["GoogleCalendar:ApiKey"]
                  ?? throw new InvalidOperationException("Google Calendar API key is not configured.");
        _queryHolidayRepository = queryHolidayRepository;
    }

    public async Task<List<DateTime>> GetHolidaysAsync(int year)
    {
        return await Task.FromResult(new List<DateTime>());
        /*
        var timeMin = $"{year}-01-01T00:00:00Z";
        var timeMax = $"{year}-12-31T23:59:59Z";

        var url = $"https://www.googleapis.com/calendar/v3/calendars/{CalendarId}/events" +
                  $"?key={_apiKey}&timeMin={timeMin}&timeMax={timeMax}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<GoogleCalendarResponse>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (data == null)
            return new List<DateTime>();

        return data.Items
            .Where(e => e.Start?.Date != null)
            .Select(e => DateTime.Parse(e.Start.Date))
            .ToList();
        */
    }

    public async Task<List<DateTime>> GetCourseHolidaysAsync(Guid BranchId, Guid CourseClassId, DateTime StartDate)
    {
        return await _queryHolidayRepository.GetList(h => h.Date > StartDate && (
                 h.BranchId == null && h.CourseClassId == null ||
                 h.BranchId == BranchId && h.CourseClassId == null ||
                 h.CourseClassId == CourseClassId)).Select(x => x.Date).ToListAsync();
    }
}
