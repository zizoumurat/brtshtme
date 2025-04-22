using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.CourseSaleSettings;

namespace BritishTime.Application.Services.concrete;
public class CourseSaleSettingService : ICourseSaleSettingService
{
    private readonly IQueryCourseSaleSettingRepository _queryCourseSaleSettingRepository;
    private readonly ICommandCourseSaleSettingRepository _commandCourseSaleSettingRepository;
    private readonly IMapper _mapper;

    public CourseSaleSettingService(IQueryCourseSaleSettingRepository queryCourseSaleSettingRepository, ICommandCourseSaleSettingRepository commandCourseSaleSettingRepository, IMapper mapper)
    {
        _queryCourseSaleSettingRepository = queryCourseSaleSettingRepository;
        _commandCourseSaleSettingRepository = commandCourseSaleSettingRepository;
        _mapper = mapper;
    }

    public Task<PaginatedList<CourseSaleSettingDto>> GetAllAsync(CourseSaleSettingFilterDto filter, PageRequest pagination)
    {
        var result = _queryCourseSaleSettingRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<CourseSaleSettingDto> AddAsync(CourseSaleSettingCreateDto CourseSaleSettingDto)
    {
        var CourseSaleSetting = _mapper.Map<CourseSaleSetting>(CourseSaleSettingDto);
        await _commandCourseSaleSettingRepository.AddAsync(CourseSaleSetting);
        return _mapper.Map<CourseSaleSettingDto>(CourseSaleSetting);
    }

    public async Task<CourseSaleSettingDto> UpdateAsync(CourseSaleSettingDto CourseSaleSettingDto)
    {
        var CourseSaleSetting = await _queryCourseSaleSettingRepository.GetByIdAsync(CourseSaleSettingDto.Id);
        if (CourseSaleSetting == null) throw new KeyNotFoundException("notFoundEntity");

        var newCourseSaleSetting = _mapper.Map<CourseSaleSetting>(CourseSaleSettingDto);

        await _commandCourseSaleSettingRepository.UpdateAsync(newCourseSaleSetting);

        return _mapper.Map<CourseSaleSettingDto>(newCourseSaleSetting);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var CourseSaleSettingDto = await _queryCourseSaleSettingRepository.GetByIdAsync(id);
        if (CourseSaleSettingDto == null) throw new KeyNotFoundException("notFoundEntity");

        var CourseSaleSetting = _mapper.Map<CourseSaleSetting>(CourseSaleSettingDto);

        await _commandCourseSaleSettingRepository.DeleteAsync(CourseSaleSetting);

        return true;
    }
}
