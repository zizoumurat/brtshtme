using AutoMapper;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Discounts;

namespace BritishTime.Application.Services.concrete;
public class DiscountService : IDiscountService
{
    private readonly IQueryDiscountRepository _queryDiscountRepository;
    private readonly ICommandDiscountRepository _commandDiscountRepository;
    private readonly IMapper _mapper;

    public DiscountService(IQueryDiscountRepository queryDiscountRepository, ICommandDiscountRepository commandDiscountRepository, IMapper mapper)
    {
        _queryDiscountRepository = queryDiscountRepository;
        _commandDiscountRepository = commandDiscountRepository;
        _mapper = mapper;
    }

    public Task<PaginatedList<DiscountDto>> GetAllAsync(DiscountFilterDto filter, PageRequest pagination)
    {
        var result = _queryDiscountRepository.GetAllAsync(filter, pagination);

        return result;
    }

    public async Task<DiscountDto> AddAsync(DiscountCreateDto DiscountDto)
    {
        var Discount = _mapper.Map<Discount>(DiscountDto);
        await _commandDiscountRepository.AddAsync(Discount);
        return _mapper.Map<DiscountDto>(Discount);
    }

    public async Task<DiscountDto> UpdateAsync(DiscountDto DiscountDto)
    {
        var Discount = await _queryDiscountRepository.GetByIdAsync(DiscountDto.Id);
        if (Discount == null) throw new KeyNotFoundException("notFoundEntity");

        var newDiscount = _mapper.Map<Discount>(DiscountDto);

        await _commandDiscountRepository.UpdateAsync(newDiscount);

        return _mapper.Map<DiscountDto>(newDiscount);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var DiscountDto = await _queryDiscountRepository.GetByIdAsync(id);
        if (DiscountDto == null) throw new KeyNotFoundException("notFoundEntity");

        var Discount = _mapper.Map<Discount>(DiscountDto);

        await _commandDiscountRepository.DeleteAsync(Discount);

        return true;
    }
}
