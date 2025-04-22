using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.Discounts;

public interface ICommandDiscountRepository
{
    Task AddAsync(Discount Discount);
    Task UpdateAsync(Discount Discount);
    Task DeleteAsync(Discount Discount);
}