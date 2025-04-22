using BritishTime.Domain.Abstractions;

namespace BritishTime.Domain.Entities;

public class InstallmentSetting : Entity
{
    public int Level { get; set; }
    public int MaxBond { get; set; }
    public int MaxCardInstallment { get; set; }

    public Guid BranchId { get; set; }
    public Branch Branch { get; set; } = default!;
}