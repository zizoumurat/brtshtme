using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.CrmRecords;
using BritishTime.Domain.Repositories.Employees;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Application.Services.concrete;
public class CrmRecordService : ICrmRecordService
{
    private readonly IQueryEmployeeRepository _queryEmployeeRepository;
    private readonly IQueryCrmRecordRepository _queryCrmRecordRepository;
    private readonly ICommandCrmRecordRepository _commandCrmRecordRepository;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public CrmRecordService(IQueryCrmRecordRepository queryCrmRecordRepository, ICommandCrmRecordRepository commandCrmRecordRepository, IMapper mapper, IUserContextService userContextService, IQueryEmployeeRepository queryEmployeeRepository)
    {
        _queryCrmRecordRepository = queryCrmRecordRepository;
        _commandCrmRecordRepository = commandCrmRecordRepository;
        _mapper = mapper;
        _userContextService = userContextService;
        _queryEmployeeRepository = queryEmployeeRepository;
    }

    public Task<PaginatedList<CrmRecordDto>> GetAllAsync(CrmRecordFilterDto filter, PageRequest pagination)
    {
        var result = _queryCrmRecordRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<CrmRecordDto> AddAsync(CrmRecordCreateDto crmRecord)
    {
        try
        {
            var crmRecordExist = await _queryCrmRecordRepository.IsExistingAsync(x => x.Phone == crmRecord.Phone);

            if (crmRecordExist) throw new Exception("duplicatePhone");

            crmRecordExist = await _queryCrmRecordRepository.IsExistingAsync(x => x.Email == crmRecord.Email);

            if (crmRecordExist) throw new Exception("duplicateMail");

            var crm = _mapper.Map<CrmRecord>(crmRecord);

            var user = await _userContextService.GetCurrentUserAsync();
            var employee = await _queryEmployeeRepository.GetAllAsync(x => x.AppUserId == user.Id).FirstOrDefaultAsync();

            if (employee == null) throw new KeyNotFoundException("notFoundEntity");

            crm.Date = DateTime.UtcNow;
            crm.SalesRepresentativeId = employee.Id;

            await _commandCrmRecordRepository.AddAsync(crm);

            return _mapper.Map<CrmRecordDto>(crm);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<CrmRecordDto> UpdateAsync(CrmRecordDto crmRecord)
    {
        var CrmRecord = await _queryCrmRecordRepository.GetByIdAsync(crmRecord.Id);
        if (CrmRecord == null) throw new KeyNotFoundException("notFoundEntity");

        var newCrmRecord = _mapper.Map<CrmRecord>(crmRecord);

        await _commandCrmRecordRepository.UpdateAsync(newCrmRecord);

        return _mapper.Map<CrmRecordDto>(newCrmRecord);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var crmRecord = await _queryCrmRecordRepository.GetByIdAsync(id);
        if (crmRecord == null) throw new KeyNotFoundException("notFoundEntity");

        var CrmRecord = _mapper.Map<CrmRecord>(crmRecord);

        await _commandCrmRecordRepository.DeleteAsync(CrmRecord);

        return true;
    }

    public async Task<CrmRecordDto> CheckPhone(string phone)
    {
        return await _queryCrmRecordRepository.GetByPhone(phone);
    }

    public async Task<CrmRecordDto> GetByIdAsync(Guid id)
    {
        return await _queryCrmRecordRepository.GetByIdAsync(id);
    }
}
