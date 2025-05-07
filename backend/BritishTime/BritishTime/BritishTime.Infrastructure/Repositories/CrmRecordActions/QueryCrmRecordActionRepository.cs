using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Enums;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.CrmRecordActions;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.CrmRecordActions;

public class QueryCrmRecordActionRepository : IQueryCrmRecordActionRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryCrmRecordActionRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CrmRecordActionDto>> GetAllAsync(CrmRecordActionFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<CrmRecordAction>().AsNoTracking();

        var count = await query.CountAsync();

        if (filter != null)
        {
            query = query.Where(x =>
                    (filter.StartDate == null || x.Date.Date >= filter.StartDate.Value.Date) &&
                    (filter.EndDate == null || x.Date.Date <= filter.EndDate.Value.Date) &&
                    (filter.Status == null || x.CrmRecord.Status == filter.Status) &&
                    (filter.ActionType == null || x.ActionType == filter.ActionType) &&
                    (filter.DataProviderId == null || x.CrmRecord.DataProviderId == filter.DataProviderId) &&
                    (filter.RegionId == null || x.CrmRecord.RegionId == filter.RegionId) &&
                    (string.IsNullOrEmpty(filter.FirstName) || x.CrmRecord.FirstName.ToLower().Contains(filter.FirstName.ToLower())) &&
                    (string.IsNullOrEmpty(filter.LastName) || x.CrmRecord.LastName.ToLower().Contains(filter.LastName.ToLower()))
                );
        }

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<CrmRecordActionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<CrmRecordActionDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<IList<CrmRecordActionDto>> GetListByCrmRecord(Guid CrmRecordId)
    {
        var list = await _context.CrmRecordActions
            .Include(x => x.Employee)
            .Where(x => x.CrmRecordId == CrmRecordId)
            .AsNoTracking()
            .ProjectTo<CrmRecordActionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return list;
    }

    public async Task<IList<CrmRecordActionDto>> GetListAsync(Expression<Func<CrmRecordAction, bool>> predicate)
    {
        var entities = await _context.CrmRecordActions
            .Where(predicate)
            .ToListAsync();

        return _mapper.Map<List<CrmRecordActionDto>>(entities);
    }

    public async Task<CrmRecordActionDto> GetAsync(Expression<Func<CrmRecordAction, bool>> predicate)
    {
        var entity = await _context.CrmRecordActions
            .FirstOrDefaultAsync(predicate);

        return _mapper.Map<CrmRecordActionDto>(entity);
    }

    public async Task<CrmRecordActionDto> GetByIdAsync(Guid id)
    {
        var crmRecordAction = await _context.CrmRecordActions
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<CrmRecordActionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return crmRecordAction;
    }

    public async Task<IList<AppointmentListDto>> GetValidAppointmentsByDateAsync(DateTime date, Guid employeeId)
    {
        var start = date.Date;
        var end = date.Date.AddDays(1);

        var list = await _context.CrmRecordActions
            .Include(x => x.Employee)
            .Include(x => x.CrmRecord)
            .Where(r =>
                r.EmployeeId == employeeId &&
                r.ActionType == CrmActionType.Appointment &&
                r.TargetDate >= start && r.TargetDate < end &&
                !_context.CrmRecordActions.Any(x =>
                    x.CrmRecordId == r.CrmRecordId &&
                    x.Date > r.Date &&
                    x.ActionType != CrmActionType.Other &&
                    x.ActionType != CrmActionType.Appointment
                ))
            .Select(x => new AppointmentListDto(x.CrmRecordId, $"{x.CrmRecord.FirstName} {x.CrmRecord.LastName}", x.CrmRecord.Phone))
            .ToListAsync();

        return list;
    }

    public async Task<IList<AppointmentListDto>> GetValidCallsByDateAsync(DateTime date, Guid employeeId)
    {
        var start = date.Date;
        var end = date.Date.AddDays(1);

        var list = await _context.CrmRecordActions
            .Include(x => x.Employee)
            .Include(x => x.CrmRecord)
            .Where(r =>
                r.EmployeeId == employeeId &&
                r.ActionType == CrmActionType.Callback &&
                r.TargetDate >= start && r.TargetDate < end &&
                !_context.CrmRecordActions.Any(x =>
                    x.CrmRecordId == r.CrmRecordId &&
                    x.Date > r.Date &&
                    x.ActionType != CrmActionType.Other &&
                    x.ActionType != CrmActionType.Callback
                ))
            .Select(x => new AppointmentListDto(x.CrmRecordId, $"{x.CrmRecord.FirstName} {x.CrmRecord.LastName}", x.CrmRecord.Phone))
            .ToListAsync();

        return list;
    }

    public async Task<IList<AppointmentListDto>> GetOpenAppointmentsAsync(Guid employeeId)
    {
        var today = DateTime.Now.Date;

        var list = await _context.CrmRecordActions
            .Include(x => x.Employee)
            .Include(x => x.CrmRecord)
            .Where(r =>
                r.EmployeeId == employeeId &&
                r.ActionType == CrmActionType.Appointment &&
                r.TargetDate < today &&
                !_context.CrmRecordActions.Any(x =>
                    x.CrmRecordId == r.CrmRecordId &&
                    x.Date > r.Date &&
                    x.ActionType != CrmActionType.Other &&
                    x.ActionType != CrmActionType.Appointment
                ))
            .Select(x => new AppointmentListDto(x.CrmRecordId, $"{x.CrmRecord.FirstName} {x.CrmRecord.LastName}", x.CrmRecord.Phone))
            .ToListAsync();

        return list;
    }
    public async Task<IList<AppointmentListDto>> GetOpenCallsAsync(Guid employeeId)
    {
        var today = DateTime.Now.Date;

        var list = await _context.CrmRecordActions
            .Include(x => x.Employee)
            .Include(x => x.CrmRecord)
            .Where(r =>
                r.EmployeeId == employeeId &&
                r.ActionType == CrmActionType.Callback &&
                r.TargetDate < today &&
                !_context.CrmRecordActions.Any(x =>
                    x.CrmRecordId == r.CrmRecordId &&
                    x.Date > r.Date &&
                    x.ActionType != CrmActionType.Other &&
                    x.ActionType != CrmActionType.Callback
                ))
            .Select(x => new AppointmentListDto(x.CrmRecordId, $"{x.CrmRecord.FirstName} {x.CrmRecord.LastName}", x.CrmRecord.Phone))
            .ToListAsync();

        return list;
    }
}

