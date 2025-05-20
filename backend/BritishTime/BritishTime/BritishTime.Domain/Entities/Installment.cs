using BritishTime.Domain.Abstractions;

namespace BritishTime.Domain.Entities;

public class Installment : Entity
{
    public Guid ContractId { get; set; }
    public Contract Contract { get; set; }

    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }

    public ICollection<Payment> Payments { get; set; } = [];
}

