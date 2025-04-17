using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Branches;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BritishTime.Application.Services.concrete;
public class BranchService : IBranchService
{
    private readonly IQueryBranchRepository _queryBranchRepository;
    private readonly ICommandBranchRepository _commandBranchRepository;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public BranchService(IQueryBranchRepository queryBranchRepository, ICommandBranchRepository commandBranchRepository, IMapper mapper, IUserContextService userContextService)
    {
        _queryBranchRepository = queryBranchRepository;
        _commandBranchRepository = commandBranchRepository;
        _mapper = mapper;
        _userContextService = userContextService;
    }

    public Task<PaginatedList<BranchDto>> GetAllAsync(BranchFilterDto filter, PageRequest pagination)
    {
        var result = _queryBranchRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<BranchDto> AddAsync(BranchCreateDto branchDto)
    {
        var branch = _mapper.Map<Branch>(branchDto);
        await _commandBranchRepository.AddAsync(branch);
        return _mapper.Map<BranchDto>(branch);
    }

    public async Task<BranchDto> UpdateAsync(BranchDto branchDto)
    {
        var branch = await _queryBranchRepository.GetByIdAsync(branchDto.Id);
        if (branch == null) throw new KeyNotFoundException("notFoundEntity");

        var newBranch = _mapper.Map<Branch>(branchDto);

        await _commandBranchRepository.UpdateAsync(newBranch);

        return _mapper.Map<BranchDto>(newBranch);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var branchDto = await _queryBranchRepository.GetByIdAsync(id);
        if (branchDto == null) throw new KeyNotFoundException("notFoundEntity");

        var branch = _mapper.Map<Branch>(branchDto);

        await _commandBranchRepository.DeleteAsync(branch);

        return true;
    }

    public async Task<List<SelectListDto>> GetUserBranchListAsync()
    {
        var user = await _userContextService.GetCurrentUserAsync();

        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found.");
        }

        var roles = await _userContextService.GetUserRolesAsync(user);
        bool isAdmin = roles.Contains("Admin");

        var branches = await _queryBranchRepository
        .GetList(x => isAdmin || x.Id == user.BranchId)
        .Select(x => new SelectListDto(x.Id, x.Name)).ToListAsync();

        return branches;
    }
}
