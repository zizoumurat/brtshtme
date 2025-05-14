using BritishTime.Application.Features.BrancheFeatures.Queries.GetAllBranches;
using BritishTime.Application.Features.BrancheFeatures.Queries.GetUserBranches;
using BritishTime.Application.Features.Branches.Commands.CreateBranch;
using BritishTime.Application.Features.Branches.Commands.DeleteBranch;
using BritishTime.Application.Features.Branches.Commands.UpdateBranch;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class BranchesController : ApiController
{
    public BranchesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BranchFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllBranchesQuery query = new(filter, pagination);
        GetAllBranchesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("user-branch-list")]
    public async Task<IActionResult> GetAll()
    {
        GetUserBranchesQuery query = new();
        GetUserBranchesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BranchCreateDto branch)
    {
        CreateBranchCommand request = new(branch);
        CreateBranchCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(BranchDto branch)
    {
        UpdateBranchCommand request = new(branch);
        UpdateBranchCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteBranchCommand request = new(id);
        DeleteBranchCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPost("calculate-payment")]
    public async Task<IActionResult> CalculatePayment([FromBody] PaymentCalculationRequest request)
    {
        // 1. Seviye bazlı saat hesabı
        var level = await _context.Levels.FindAsync(request.LevelId);
        if (level == null)
            return BadRequest("Level not found.");

        int totalHours = level.HourPerLevel * request.LevelCount;

        // 2. Saatlik ücret
        decimal hourlyRate = 106.3m;
        decimal baseAmount = totalHours * hourlyRate;

        // 3. Kampanya indirimi
        if (request.CampaignId.HasValue)
        {
            var campaign = await _context.Campaigns.FindAsync(request.CampaignId.Value);
            if (campaign != null)
                baseAmount *= campaign.DiscountRate;
        }

        // 4. Gerekçeli indirim
        if (request.DiscountId.HasValue)
        {
            var discount = await _context.Discounts.FindAsync(request.DiscountId.Value);
            if (discount != null)
                baseAmount *= discount.DiscountRate;
        }

        decimal finalAmount = baseAmount;

        // 5. Kredi kartı indirimi (sadece kredi kartı ödemelerinde)
        if (request.PaymentMethod == "creditCard")
        {
            finalAmount *= 0.97m;
        }

        // 6. Taksit varsa → her ödeme yönteminde vade farkı eklenir
        if (request.InstallmentCount > 1)
        {
            decimal interestRate = 1.01686m;
            finalAmount *= (decimal)Math.Pow((double)interestRate, request.InstallmentCount);
        }

        // 7. Nakit + peşin ödeme (tek çekim) ise ekstra indirim
        if (request.IsCash && request.InstallmentCount == 1)
        {
            finalAmount *= 0.95m;
        }

        return Ok(new
        {
            TotalHours = totalHours,
            BaseAmount = Math.Round(baseAmount, 2),
            FinalAmount = Math.Round(finalAmount, 2),
            InstallmentCount = request.InstallmentCount,
            InstallmentAmount = request.InstallmentCount > 1
                ? Math.Round(finalAmount / request.InstallmentCount, 2)
                : Math.Round(finalAmount, 2)
        });
    }

}

public class PaymentCalculationRequest
{
    public int LevelId { get; set; }
    public int LevelCount { get; set; }
    public int? CampaignId { get; set; }
    public int? DiscountId { get; set; }
    public bool IsCash { get; set; }
    public int InstallmentCount { get; set; }
    public required string PaymentMethod { get; set; } // "creditCard" veya "promissoryNote"
}