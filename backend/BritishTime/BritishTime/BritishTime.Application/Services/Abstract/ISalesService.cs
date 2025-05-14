using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface ISalesService
{
    Task<CalculatePaymentResultDto> CalculatePayment(CalculatePaymentDto id);
}
