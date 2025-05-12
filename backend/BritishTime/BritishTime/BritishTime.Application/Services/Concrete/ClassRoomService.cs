using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.ClassRooms;

namespace BritishTime.Application.Services.concrete;
public class ClassRoomService : IClassRoomService
{
    private readonly IQueryClassRoomRepository _queryClassRoomRepository;
    private readonly ICommandClassRoomRepository _commandClassRoomRepository;
    private readonly IMapper _mapper;

    public ClassRoomService(IQueryClassRoomRepository queryClassRoomRepository, ICommandClassRoomRepository commandClassRoomRepository, IMapper mapper)
    {
        _queryClassRoomRepository = queryClassRoomRepository;
        _commandClassRoomRepository = commandClassRoomRepository;
        _mapper = mapper;
    }

    public Task<PaginatedList<ClassRoomDto>> GetAllAsync(ClassRoomFilterDto filter, PageRequest pagination)
    {
        var result = _queryClassRoomRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<ClassRoomDto> AddAsync(ClassRoomCreateDto classRoomDto)
    {
        var isExisting = await _queryClassRoomRepository.IsExistingAsync(x => x.Name == classRoomDto.Name && x.BranchId == classRoomDto.BranchId);

        if (isExisting)
            if (isExisting) throw new Exception("duplicateName");

        var classRoom = _mapper.Map<ClassRoom>(classRoomDto);
        await _commandClassRoomRepository.AddAsync(classRoom);
        return _mapper.Map<ClassRoomDto>(classRoom);
    }

    public async Task<ClassRoomDto> UpdateAsync(ClassRoomDto classRoomDto)
    {
        var classRoom = await _queryClassRoomRepository.GetByIdAsync(classRoomDto.Id);
        if (classRoom == null) throw new KeyNotFoundException("notFoundEntity");

        var isExisting = await _queryClassRoomRepository.IsExistingAsync(x => x.Id != classRoomDto.Id && x.Name == classRoomDto.Name && x.BranchId == classRoomDto.BranchId);

        if (isExisting)
            if (isExisting) throw new Exception("duplicateName");

        var newClassRoom = _mapper.Map<ClassRoom>(classRoomDto);

        await _commandClassRoomRepository.UpdateAsync(newClassRoom);

        return _mapper.Map<ClassRoomDto>(newClassRoom);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var ClassRoomDto = await _queryClassRoomRepository.GetByIdAsync(id);
        if (ClassRoomDto == null) throw new KeyNotFoundException("notFoundEntity");

        var ClassRoom = _mapper.Map<ClassRoom>(ClassRoomDto);

        await _commandClassRoomRepository.DeleteAsync(ClassRoom);

        return true;
    }
}
