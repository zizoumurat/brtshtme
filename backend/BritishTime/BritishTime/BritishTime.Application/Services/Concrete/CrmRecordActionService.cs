using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Consts;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Enums;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.CrmRecordActions;
using BritishTime.Domain.Repositories.CrmRecords;
using BritishTime.Domain.Repositories.Employees;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Application.Services.concrete;
public class CrmRecordActionService : ICrmRecordActionService
{
    private readonly IQueryCrmRecordRepository _queryCrmRecordRepository;
    private readonly ICommandCrmRecordRepository _crmRecordRepository;
    private readonly IQueryEmployeeRepository _queryEmployeeRepository;
    private readonly IQueryCrmRecordActionRepository _queryCrmRecordActionRepository;
    private readonly ICommandCrmRecordActionRepository _commandCrmRecordActionRepository;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public CrmRecordActionService(IQueryCrmRecordActionRepository queryCrmRecordActionRepository, ICommandCrmRecordActionRepository commandCrmRecordActionRepository, IMapper mapper, IUserContextService userContextService, IQueryEmployeeRepository queryEmployeeRepository, IQueryCrmRecordRepository queryCrmRecordRepository, ICommandCrmRecordRepository crmRecordRepository)
    {
        _queryCrmRecordActionRepository = queryCrmRecordActionRepository;
        _commandCrmRecordActionRepository = commandCrmRecordActionRepository;
        _mapper = mapper;
        _userContextService = userContextService;
        _queryEmployeeRepository = queryEmployeeRepository;
        _queryCrmRecordRepository = queryCrmRecordRepository;
        _crmRecordRepository = crmRecordRepository;
    }

    public async Task<CrmRecordActionDto> AddAsync(CrmRecordActionCreateDto crmRecordActionDto)
    {
        try
        {
            var currentUser = await _userContextService.GetCurrentUserAsync();
            var employee = await _queryEmployeeRepository.GetAllAsync(x => x.AppUserId == currentUser.Id).FirstAsync();
            var crmRecordAction = _mapper.Map<CrmRecordAction>(crmRecordActionDto);
            crmRecordAction.Date = DateTime.Now;
            crmRecordAction.EmployeeId = employee.Id;

            await _commandCrmRecordActionRepository.AddAsync(crmRecordAction);

            var actionList = (await _queryCrmRecordActionRepository.GetListAsync(x => x.CrmRecordId == crmRecordActionDto.CrmRecordId))
                .OrderByDescending(x => x.Date)
                .ToList();

            var crmDto = await _queryCrmRecordRepository.GetByIdAsync(crmRecordActionDto.CrmRecordId);
            var crm = _mapper.Map<CrmRecord>(crmDto);

            if (crmRecordActionDto.ActionType != CrmActionType.Other && crmRecordActionDto.ActionType != CrmActionType.Sms)
            {

                // data üzerinde işlem yapılmamışsa ve üzerinden 24 saat geçmişse, data sahibi el değiştirir
                var ts = DateTime.Now - crm.Date;
                if (actionList.Count == 1 && ts.TotalHours > CrmProcessConsts.NoActionHours)
                {
                    crm.SalesRepresentativeId = employee.Id;
                    crm.Date = DateTime.Now;
                }

                if (actionList.Count > 1)
                {
                    var first = actionList.Skip(1).FirstOrDefault();
                    ts = DateTime.Now - (first != null ? first.Date : DateTime.Now);

                    // Ne sebeple olursa olsun daha önce kontak kurulup 3 ayrı tarihte 'Ulaşılamıyor' işlemi yapılıp 'Ulaşılamadı' kategorisine düşen datalar, 24 saat sonra yeni data sayılır
                    // Burası hatalı
                    if (crm.Status == CrmStatus.Unreachable && ts.TotalHours > CrmProcessConsts.NoReplyHours)
                    {
                        crm.SalesRepresentativeId = employee.Id;
                        crm.Date = DateTime.Now;
                        crm.Status = CrmStatus.NewData;
                    }

                    // Negatif sonuçlanmış yada kirli data kategorisine düşmüş bir data 24 saat sonra ilgili data sağlayıcıdan çıkıp yeni data vasfı kazanır
                    else if ((crm.Status == CrmStatus.Dirty || crm.Status == CrmStatus.Negative) && ts.TotalHours > CrmProcessConsts.NegativeHours)
                    {
                        crm.SalesRepresentativeId = employee.Id;
                        crm.Date = DateTime.Now;
                        crm.Status = CrmStatus.NewData;
                    }

                    // Değerlendirmede olan bir datanın son aktif işlem tarihinden itibaren 45 gün geçmiş ve herhangi bir şekilde kuruma tekrar girmiş ise yeni data sayılır. Son işlem tekrar ara veya Randevu olan datalar için dahi hedef tarih (Randevu veya Tekrar Ara tarihi) henüz gelmemiş bile olsa durum değişmez.
                    else if (crm.Status == CrmStatus.InEvaluation && ts.TotalHours > CrmProcessConsts.InProgressDay)
                    {
                        crm.SalesRepresentativeId = employee.Id;
                        crm.Date = DateTime.Now;
                        crm.Status = CrmStatus.NewData;
                    }

                    // Ne sebeple olursa olsun daha önce kontak kurulup 3 ayrı tarihte 'Ulaşılamıyor' işlemi yapıldıysa data 'Ulaşılamadı' kategorisine düşer
                    if (crmRecordActionDto.ActionType == CrmActionType.Unreachable)
                    {
                        var unreachableCount = actionList
                            .Where(x => x.ActionType == CrmActionType.Unreachable)
                            .OrderByDescending(x => x.Date)
                            .Select(x => x.Date.Date)
                            .Distinct()
                            .Take(3)
                            .Count();

                        if (unreachableCount == 3)
                        {
                            crm.Status = CrmStatus.Unreachable;
                        }
                    }
                }

                if (new[] { CrmActionType.Callback, CrmActionType.Appointment }.Contains(crmRecordActionDto.ActionType))
                {
                    crm.Status = CrmStatus.InEvaluation;
                }
                else if (crmRecordActionDto.ActionType == CrmActionType.WrongNumber)
                {
                    crm.Status = CrmStatus.Dirty;
                }
                else if (crmRecordActionDto.ActionType == CrmActionType.NotInterested || crmRecordActionDto.ActionType == CrmActionType.Canceled)
                {
                    crm.Status = CrmStatus.Negative;
                }
                else if (crmRecordAction.ActionType == CrmActionType.Sale)
                {
                    crm.Status = CrmStatus.Sold;
                }

                await _crmRecordRepository.UpdateAsync(crm);
            }


            return _mapper.Map<CrmRecordActionDto>(crmRecordAction);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<PaginatedList<CrmRecordActionDto>> GetAllAsync(CrmRecordActionFilterDto filter, PageRequest pagination)
    {
        if (filter.EmployeeId == null)
            filter.EmployeeId = await _userContextService.GetCurrentUserEmployeeId();

        var result = await _queryCrmRecordActionRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public Task<IList<CrmRecordActionDto>> GetListByCrmRecord(Guid CrmRecordId)
    {
        var result = _queryCrmRecordActionRepository.GetListByCrmRecord(CrmRecordId);

        return result;
    }

    public async Task<IList<AppointmentListDto>> GetOpenAppointmentsAsync()
    {
        var employeeId = await _userContextService.GetCurrentUserEmployeeId();

        return await _queryCrmRecordActionRepository.GetOpenAppointmentsAsync(employeeId);
    }

    public async Task<IList<AppointmentListDto>> GetOpenCallsAsync()
    {
        var employeeId = await _userContextService.GetCurrentUserEmployeeId();

        return await _queryCrmRecordActionRepository.GetOpenCallsAsync(employeeId);
    }

    public async Task<IList<AppointmentListDto>> GetValidAppointmentsByDateAsync(DateTime date)
    {
        var employeeId = await _userContextService.GetCurrentUserEmployeeId();

        return await _queryCrmRecordActionRepository.GetValidAppointmentsByDateAsync(date, employeeId);
    }

    public async Task<IList<AppointmentListDto>> GetValidCallsByDateAsync(DateTime date)
    {
        var employeeId = await _userContextService.GetCurrentUserEmployeeId();

        return await _queryCrmRecordActionRepository.GetValidCallsByDateAsync(date, employeeId);
    }
}
