using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.Payments;

public interface ICommandPaymentRepository
{
    Task AddAsync(Payment Payment);
    Task UpdateAsync(Payment Payment);
    Task DeleteAsync(Payment Payment);
}