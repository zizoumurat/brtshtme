using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Enums;
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

    public async Task<CalculatePaymentResultDto> CalculatePayment(CalculatePaymentDto request)
    {
        var branch = await _queryBranchRepository.GetByIdAsync(request.BranchId)
                     ?? throw new Exception("notFoundEntity");

        if (branch.LessonDurationInMinutes <= 0)
            throw new Exception("branchDefinitionsMissing");

        var lessonScheduleDefinition = await _queryLessonScheduleDefinitionRepository.GetByIdAsync(request.LessonScheduleId)
                                       ?? throw new Exception("notFoundEntity");

        var pricing = await _queryBranchPricingSettingRepository.GetByBranchId(request.BranchId)
                     ?? throw new Exception("branchPricingSettingsMissing");

        decimal hourlyRate = pricing.HourlyRate;
        int totalHours = branch.LevelDurationInHours * request.LevelCount;

        // 🔄 baseAmount'ta floor KULLANILMIYOR
        decimal baseAmount = totalHours * hourlyRate;

        // 1. Kampanya ve gerekçeli indirimleri uygula
        decimal discountedAmount = baseAmount;

        if (request.CampaignId.HasValue)
        {
            var campaign = await _queryCampaignRepository.GetByIdAsync(request.CampaignId.Value);
            if (campaign != null)
                discountedAmount *= campaign.DiscountRate;
        }

        if (request.DiscountId.HasValue)
        {
            var discount = await _queryDiscountRepository.GetByIdAsync(request.DiscountId.Value);
            if (discount != null)
                discountedAmount *= discount.DiscountRate;
        }

        // 2. Peşinatı düş
        decimal deposit = request.Deposit ?? 0m;
        decimal remainingAmount = discountedAmount - deposit;

        // 3. Taksit varsa vade farkı uygula
        decimal financedAmount = remainingAmount;

        if (request.InstallmentCount.HasValue && request.InstallmentCount > 1)
        {
            decimal interest = pricing.InstallmentRate;
            financedAmount *= (decimal)Math.Pow((double)interest, request.InstallmentCount.Value);
        }

        // 4. Ödeme yöntemine göre indirim uygula
        if (request.PaymentMethod == PaymentMethod.Cash)
        {
            financedAmount *= pricing.CashPrepaymentDiscount;
        }
        else if (request.PaymentMethod == PaymentMethod.CreditCard && request.InstallmentCount > 1)
        {
            financedAmount *= pricing.CreditCardInstallmentDiscount;
        }

        // 🔄 Nihai floor sadece burada
        financedAmount = Math.Floor(financedAmount);

        // 5. Toplam tutar: finans edilen + peşinat
        decimal totalAmount = financedAmount + deposit;

        // 6. Taksitleri oluştur
        var installments = new List<InstallmentListDto>();
        DateTime baseDate = request.FirstInstallmentDate ?? DateTime.Today;

        if(request.PaymentMethod == PaymentMethod.Cash)
        {
            installments.Add(new InstallmentListDto(DateTime.Today, financedAmount, true));
        }
        if (deposit > 0)
        {
            installments.Add(new InstallmentListDto(DateTime.Today, deposit, true));
        }

        if (request.InstallmentCount.HasValue && request.InstallmentCount > 0 && financedAmount > 0)
        {
            decimal installmentAmount = Math.Round(financedAmount / request.InstallmentCount.Value, 2);

            for (int i = 0; i < request .InstallmentCount.Value; i++)
            {
                var dueDate = baseDate.AddDays(30 * i);
                installments.Add(new InstallmentListDto(dueDate, installmentAmount));
            }
        }

        return new CalculatePaymentResultDto(
            TotalAmount: totalAmount,
            FinancedAmount: financedAmount,
            Installments: installments
        );
    }


}
