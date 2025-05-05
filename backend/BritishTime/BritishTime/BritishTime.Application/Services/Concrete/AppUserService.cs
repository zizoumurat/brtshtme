using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Employees;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Application.Services.concrete;
public class AppUserService : IAppUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IQueryEmployeeRepository _queryEmployeeRepository;
    private readonly ICommandEmployeeRepository _commandEmployeeRepository;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public AppUserService(IMapper mapper, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IQueryEmployeeRepository queryEmployeeRepository, ICommandEmployeeRepository commandEmployeeRepository, IUserContextService userContextService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
        _queryEmployeeRepository = queryEmployeeRepository;
        _commandEmployeeRepository = commandEmployeeRepository;
        _userContextService = userContextService;
    }

    public async Task<PaginatedList<AppUserDto>> GetAllAsync(AppUserFilterDto filter, PageRequest pagination)
    {
        var currentUser = await _userContextService.GetCurrentUserAsync();
        pagination ??= new PageRequest();

        var query = _userManager.Users.Where(x=>x.Id != currentUser.Id).AsQueryable();

        var count = await query.CountAsync();

        var items = await query
            .Include(x => x.Branch)
            .Include(x => x.AppUserRoles)
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .Select(x => new AppUserDto(
                x.Id,
                x.FirstName,
                x.LastName,
                x.Email,
                string.Join(", ", x.AppUserRoles.Select(ar => ar.Role.Name)),
                x.Branch.Name
            )).ToListAsync();

        return new PaginatedList<AppUserDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var appUserDto = await _userManager.FindByIdAsync(id.ToString());
        if (appUserDto == null) throw new KeyNotFoundException("notFoundEntity");

        var appUser = _mapper.Map<AppUser>(appUserDto);

        await _userManager.DeleteAsync(appUser);

        var employeeDto = await _queryEmployeeRepository.GetAllAsync(x => x.AppUserId == id).FirstOrDefaultAsync();

        if (employeeDto != null)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            employee.AppUserId = null;
            await _commandEmployeeRepository.UpdateAsync(employee);
        }

        return true;
    }

    public async Task<bool> AddAsync(AppUserCreateDto appUserModel)
    {
        var employee = await _queryEmployeeRepository.GetByIdAsync(appUserModel.EmployeeId);
        if (employee == null)
            throw new KeyNotFoundException("notFoundEntity");

        if (employee.AppUserId != null)
            throw new InvalidOperationException("userAlreadyExist");

        foreach (var item in appUserModel.Roles)
        {
            var role = await _roleManager.FindByNameAsync(item);
            if (role == null)
                throw new InvalidOperationException("roleNotFound");
        }
       

        var addUser = new AppUser
        {
            Id = Guid.NewGuid(),
            UserName = employee.Email,
            Email = employee.Email,
            BranchId = employee.BranchId,
            PhoneNumber = employee.Phone1,
            FirstName = employee.FirstName,
            LastName = employee.LastName
        };

        var createResult = await _userManager.CreateAsync(addUser, appUserModel.Password);
        if (!createResult.Succeeded)
            throw new Exception(string.Join(" | ", createResult.Errors.Select(e => e.Description)));

        var roleResult = await _userManager.AddToRolesAsync(addUser,  appUserModel.Roles);
        if (!roleResult.Succeeded)
            throw new Exception(string.Join(" | ", roleResult.Errors.Select(e => e.Description)));

        var editEmployee = _mapper.Map<Employee>(employee);
        editEmployee.AppUserId = addUser.Id;

        await _commandEmployeeRepository.UpdateAsync(editEmployee);

        return true;
    }

    public async Task<List<SelectListDto>> GetUnassignedEmployees(Guid BranchId)
    {
        var list = await _queryEmployeeRepository
           .GetAllAsync(x => x.AppUserId == null && x.BranchId == BranchId)
           .Select(x => new SelectListDto(x.Id, $"{x.FirstName} {x.LastName}"))
           .ToListAsync();

        return list;
    }
}
