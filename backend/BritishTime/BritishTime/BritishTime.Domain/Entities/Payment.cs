using BritishTime.Domain.Abstractions;
using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Entities;

public class Payment : Entity
{
    public Guid StudentId { get; set; }
    public Student Student { get; set; }

    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public Guid? InstallmentId { get; set; }
    public Installment Installment { get; set; } 

    public string Description { get; set; }
}

