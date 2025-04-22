using BritishTime.Domain.Abstractions;

namespace BritishTime.Domain.Entities;

public class BranchPricingSetting : Entity
{
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; }
    public decimal HourlyRate { get; set; }
    public decimal DiscountForPrepayment { get; set; }
    public decimal CashPrepaymentDiscount { get; set; }
    public decimal CreditCardInstallmentDiscount { get; set; }
    public decimal InstallmentRate { get; set; }
    public decimal CollectionRateForBonus { get; set; }
}
