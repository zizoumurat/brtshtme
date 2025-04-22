using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.InstallmentSettings;

namespace BritishTime.Application.Services.concrete;
public class InstallmentSettingService : IInstallmentSettingService
{
    private readonly IQueryInstallmentSettingRepository _queryInstallmentSettingRepository;
    private readonly ICommandInstallmentSettingRepository _commandInstallmentSettingRepository;
    private readonly IMapper _mapper;

    public InstallmentSettingService(IQueryInstallmentSettingRepository queryInstallmentSettingRepository, ICommandInstallmentSettingRepository commandInstallmentSettingRepository, IMapper mapper)
    {
        _queryInstallmentSettingRepository = queryInstallmentSettingRepository;
        _commandInstallmentSettingRepository = commandInstallmentSettingRepository;
        _mapper = mapper;
    }

    public Task<PaginatedList<InstallmentSettingDto>> GetAllAsync(InstallmentSettingFilterDto filter, PageRequest pagination)
    {
        var result = _queryInstallmentSettingRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<InstallmentSettingDto> AddAsync(InstallmentSettingCreateDto InstallmentSettingDto)
    {
        var InstallmentSetting = _mapper.Map<InstallmentSetting>(InstallmentSettingDto);
        await _commandInstallmentSettingRepository.AddAsync(InstallmentSetting);
        return _mapper.Map<InstallmentSettingDto>(InstallmentSetting);
    }

    public async Task<InstallmentSettingDto> UpdateAsync(InstallmentSettingDto InstallmentSettingDto)
    {
        var InstallmentSetting = await _queryInstallmentSettingRepository.GetByIdAsync(InstallmentSettingDto.Id);
        if (InstallmentSetting == null) throw new KeyNotFoundException("notFoundEntity");

        var newInstallmentSetting = _mapper.Map<InstallmentSetting>(InstallmentSettingDto);

        await _commandInstallmentSettingRepository.UpdateAsync(newInstallmentSetting);

        return _mapper.Map<InstallmentSettingDto>(newInstallmentSetting);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var InstallmentSettingDto = await _queryInstallmentSettingRepository.GetByIdAsync(id);
        if (InstallmentSettingDto == null) throw new KeyNotFoundException("notFoundEntity");

        var InstallmentSetting = _mapper.Map<InstallmentSetting>(InstallmentSettingDto);

        await _commandInstallmentSettingRepository.DeleteAsync(InstallmentSetting);

        return true;
    }
}
