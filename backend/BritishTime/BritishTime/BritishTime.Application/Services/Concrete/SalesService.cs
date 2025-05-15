using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Repositories.Branches;
using BritishTime.Domain.Repositories.BranchPricingSettings;
using BritishTime.Domain.Repositories.Campaigns;
using BritishTime.Domain.Repositories.Discounts;
using BritishTime.Domain.Repositories.LessonScheduleDefinitions;

namespace BritishTime.Application.Services.Concrete;

public class SalesService : ISalesService
{
    private readonly IQueryBranchRepository _queryBranchRepository;
    private readonly IQueryBranchPricingSettingRepository _queryBranchPricingSettingRepository;
    private readonly IQueryLessonScheduleDefinitionRepository _queryLessonScheduleDefinitionRepository;
    private readonly IQueryCampaignRepository _queryCampaignRepository;
    private readonly IQueryDiscountRepository _queryDiscountRepository;

    public SalesService(IQueryLessonScheduleDefinitionRepository queryLessonScheduleDefinitionRepository,
        IQueryBranchRepository queryBranchRepository,
        IQueryBranchPricingSettingRepository queryBranchPricingSettingRepository,
        IQueryCampaignRepository queryCampaignRepository,
        IQueryDiscountRepository queryDiscountRepository)
    {
        _queryLessonScheduleDefinitionRepository = queryLessonScheduleDefinitionRepository;
        _queryBranchRepository = queryBranchRepository;
        _queryBranchPricingSettingRepository = queryBranchPricingSettingRepository;
        _queryCampaignRepository = queryCampaignRepository;
        _queryDiscountRepository = queryDiscountRepository;
    }

    public async Task<CalculatePaymentResultDto> CalculatePayment(CalculatePaymentDto model)
    {
        var branch = await _queryBranchRepository.GetByIdAsync(model.BranchId);
        if (branch is null)
            throw new Exception("notFoundEntity");

        if (branch.LessonDurationInMinutes <= 0)
            throw new Exception("branchDefinitionsMissing");

        var lessonScheduleDefinition = await _queryLessonScheduleDefinitionRepository.GetByIdAsync(model.LessonScheduleId);
        if (lessonScheduleDefinition == null)
            throw new Exception("notFoundEntity");

        var branchPricingSetting = await _queryBranchPricingSettingRepository.GetByBranchId(model.BranchId);
        if (branchPricingSetting is null)
            throw new Exception("branchPricingSettingsMissing");

        decimal hourlyRate = branchPricingSetting.HourlyRate;
        int totalHours = branch.LevelDurationInHours * model.LevelCount;
        decimal baseAmount = totalHours * hourlyRate;

        // Kampanya indirimi
        if (model.CampaignId.HasValue)
        {
            var campaign = await _queryCampaignRepository.GetByIdAsync(model.CampaignId.Value);
            if (campaign != null)
                baseAmount *= campaign.DiscountRate;
        }

        // Gerekçeli indirim
        if (model.DiscountId.HasValue)
        {
            var discount = await _queryDiscountRepository.GetByIdAsync(model.DiscountId.Value);
            if (discount != null)
                baseAmount *= discount.DiscountRate;
        }

        decimal finalAmount = baseAmount;

        // Peşin ödeme indirimi
        if (model.PaymentMethod == Domain.Enums.PaymentMethod.Cash)
        {
            finalAmount *= branchPricingSetting.CashPrepaymentDiscount;
        }

        // Kredi kartı taksitlendirme indirimi
        if (model.PaymentMethod == Domain.Enums.PaymentMethod.CreditCard && model.InstallmentCount > 1)
        {
            finalAmount *= branchPricingSetting.CreditCardInstallmentDiscount;
        }

        decimal downPayment = model.DownPayment ?? 0m;
        decimal financedAmount = finalAmount - downPayment;

        // Taksit varsa: vade farkı ekle
        if (model.InstallmentCount.HasValue && model.InstallmentCount > 1)
        {
            decimal interestRate = branchPricingSetting.InstallmentRate;
            financedAmount *= (decimal)Math.Pow((double)interestRate, model.InstallmentCount.Value);
        }

        var installments = new List<InstallmentDto>();

        if (model.InstallmentCount.HasValue && model.InstallmentCount > 0 && financedAmount > 0)
        {
            decimal installmentAmount = Math.Round(financedAmount / model.InstallmentCount.Value, 2);
            DateTime baseDate = model.FirstInstallmentDate ?? DateTime.Today;

            for (int i = 0; i < model.InstallmentCount.Value; i++)
            {
                var dueDate = (i == 0 && downPayment > 0) ? DateTime.Today : baseDate.AddDays(30 * i);
                installments.Add(new InstallmentDto(dueDate, installmentAmount));
            }
        }

        return new CalculatePaymentResultDto(
            TotalAmount: finalAmount,
            FinancedAmount: financedAmount,
            Installments: installments
        );
    }

}
