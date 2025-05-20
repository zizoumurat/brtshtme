using BritishTime.Domain.Abstractions;
using BritishTime.Domain.Enums;

namespace BritishTime.Domain.Entities;

public class Contract : Entity
{
    public Guid StudentId { get; set; }
    public Student Student { get; set; }

    public DateTime StartDate { get; set; }
    public int LevelCount { get; set; }
    public int UsedLevelCount { get; set; } = 0;
    public decimal TotalAmount { get; set; }

    public PaymentMethod PaymentMethod { get; set; }
    public int? InstallmentCount { get; set; }
    public decimal? Deposit { get; set; }
    public ContractType ContractType { get; set; }
    public EducationType EducationType { get; set; }
    public Signatory Signatory { get; set; }

    public Guid LessonScheduleId { get; set; }
    public LessonScheduleDefinition LessonScheduleDefinition { get; set; }

    public Guid? CampaignId { get; set; }
    public Campaign Campaign { get; set; }

    public Guid? DiscountId { get; set; }
    public Discount Discount { get; set; }

    public ICollection<Installment> Installments { get; set; } = [];
}

