using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Employees;

namespace BritishTime.Application.Services.concrete;
public class EmployeeService : IEmployeeService
{
    private readonly IQueryEmployeeRepository _queryEmployeeRepository;
    private readonly ICommandEmployeeRepository _commandEmployeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IQueryEmployeeRepository queryEmployeeRepository, ICommandEmployeeRepository commandEmployeeRepository, IMapper mapper)
    {
        _queryEmployeeRepository = queryEmployeeRepository;
        _commandEmployeeRepository = commandEmployeeRepository;
        _mapper = mapper;
    }

    public Task<PaginatedList<EmployeeDto>> GetAllAsync(EmployeeFilterDto filter, PageRequest pagination)
    {
        var result = _queryEmployeeRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<EmployeeDto> AddAsync(EmployeeCreateDto employeeDto)
    {
        var existingUser = await _queryEmployeeRepository.IsExistingAsync(x => x.IdentityNumber == employeeDto.IdentityNumber || x.Email == employeeDto.Email || x.Phone1 == employeeDto.Phone1);
        if (existingUser) throw new KeyNotFoundException("notFoundEntity");


        var employee = _mapper.Map<Employee>(employeeDto);
        await _commandEmployeeRepository.AddAsync(employee);
        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto> UpdateAsync(EmployeeDto employeeDto)
    {
        var employee = await _queryEmployeeRepository.GetByIdAsync(employeeDto.Id);
        if (employee == null) throw new KeyNotFoundException("notFoundEntity");

        var newEmployee = _mapper.Map<Employee>(employeeDto);

        await _commandEmployeeRepository.UpdateAsync(newEmployee);

        return _mapper.Map<EmployeeDto>(newEmployee);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var employeeDto = await _queryEmployeeRepository.GetByIdAsync(id);
        if (employeeDto == null) throw new KeyNotFoundException("notFoundEntity");

        var employee = _mapper.Map<Employee>(employeeDto);

        await _commandEmployeeRepository.DeleteAsync(employee);

        return true;
    }

    public async Task<List<SelectListDto>> GetListAsync(Guid BranchId)
    {
        return await _queryEmployeeRepository.GetListAsync(BranchId);
    }
}
