namespace BritishTime.Domain.Dtos;

public record BranchPricingSettingDto(
    Guid? Id,
    Guid BranchId,
    decimal HourlyRate,
    decimal DiscountForPrepayment,
    decimal CashPrepaymentDiscount,
    decimal CreditCardInstallmentDiscount,
    decimal InstallmentRate,
    decimal CollectionRateForBonus
);