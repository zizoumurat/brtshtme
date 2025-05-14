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

        if (branch == null)
            throw new Exception("notFoundEntity");

        if (branch.LessonDurationInMinutes <= 0)
            throw new Exception("branchDefinitionsMissing");

        var lessonScheduleDefinition = await _queryLessonScheduleDefinitionRepository.GetByIdAsync(model.LessonScheduleId);

        if (lessonScheduleDefinition == null)
            throw new Exception("notFoundEntity");

        var branchPricingSetting = await _queryBranchPricingSettingRepository.GetByBranchId(model.BranchId);

        if (branch.LessonDurationInMinutes <= 0)
            throw new Exception("branchPricingSettingsMissing");

        decimal hourlyRate = branchPricingSetting.HourlyRate;
        int totalHours = branch.LevelDurationInHours * model.LevelCount;
        decimal baseAmount = totalHours * hourlyRate;

        if (model.CampaignId.HasValue)
        {
            var campaign = await _queryCampaignRepository.GetByIdAsync(model.CampaignId.Value);
            if (campaign != null)
                baseAmount *= campaign.DiscountRate;
        }

        if (model.DiscountId.HasValue)
        {
            var discount = await _queryDiscountRepository.GetByIdAsync(model.DiscountId.Value);
            if (discount != null)
                baseAmount *= discount.DiscountRate;
        }

        decimal finalAmount = baseAmount;

        if (model.PaymentMethod == Domain.Enums.PaymentMethod.Cash)
        {
            finalAmount *= branchPricingSetting.CashPrepaymentDiscount;
        }

        if (model.PaymentMethod == Domain.Enums.PaymentMethod.CreditCard)
        {
            finalAmount *= branchPricingSetting.CreditCardInstallmentDiscount;
        }

        if (model.InstallmentCount.HasValue && model.InstallmentCount > 1)
        {
            decimal interestRate = branchPricingSetting.InstallmentRate;
            finalAmount *= (decimal)Math.Pow((double)interestRate, model.InstallmentCount.Value);
        }

        return new(finalAmount, 0);
    }
}
