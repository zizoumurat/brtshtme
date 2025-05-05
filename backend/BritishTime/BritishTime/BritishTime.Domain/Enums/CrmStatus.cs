namespace BritishTime.Domain.Enums;

public enum CrmStatus
{
    NewData = 0,           // Yeni Data
    InEvaluation = 1,      // Değerlendirmede
    Negative = 2,          // Negatif Sonuçlandı
    Sold = 3,              // Satış Yapıldı
    BranchTransfer = 4,    // Şube Transferi
    Dirty = 5,             // Kirli Data
    Unreachable = 6        // Ulaşılamadı
}