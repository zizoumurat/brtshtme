using BritishTime.Domain.Abstractions;

namespace BritishTime.Domain.Entities;

public class Holiday : Entity
{
    public DateTime Date { get; set; }

    public string Description { get; set; } = null!;

    // Eğer tüm şubeler içinse null
    public Guid? BranchId { get; set; }

    // Eğer tüm sınıflar içinse null, ama BranchId varsa sadece o şubenin sınıfları için
    public Guid? CourseClassId { get; set; }

    public bool IsGlobal => BranchId == null && CourseClassId == null;
}
