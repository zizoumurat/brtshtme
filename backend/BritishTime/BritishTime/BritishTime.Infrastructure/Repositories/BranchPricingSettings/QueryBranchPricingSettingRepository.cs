using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Repositories.BranchPricingSettings;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Infrastructure.Repositories.BranchPricingSettings;

public class QueryBranchPricingSettingRepository : IQueryBranchPricingSettingRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryBranchPricingSettingRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BranchPricingSettingDto> GetByBranchId(Guid branchId)
    {
        var branchPricingSettings = await _context.BranchPricingSettings
            .Where(x => x.BranchId == branchId)
            .AsNoTracking()
            .ProjectTo<BranchPricingSettingDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return branchPricingSettings;
    }
}

