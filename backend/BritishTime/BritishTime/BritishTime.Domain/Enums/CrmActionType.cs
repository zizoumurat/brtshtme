namespace BritishTime.Domain.Enums;

public enum CrmActionType
{
    WrongNumber = 0,      // No Yanlış
    Unreachable = 1,      // Ulaşılamıyor
    NotInterested = 2,    // Düşünmüyor
    Callback = 3,         // Tekrar Ara
    Appointment = 4,      // Randevu
    Other = 5,            // Diğer
    Canceled = 6,         // Görüşme İptal
    Sms = 7,              // SMS (otomatik)
    Sale = 8              // Satış (otomatik)
}
