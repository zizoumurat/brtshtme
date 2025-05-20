using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.Installments;

public interface ICommandInstallmentRepository
{
    Task AddAsync(Installment Installment);
    Task UpdateAsync(Installment Installment);
    Task DeleteAsync(Installment Installment);
}