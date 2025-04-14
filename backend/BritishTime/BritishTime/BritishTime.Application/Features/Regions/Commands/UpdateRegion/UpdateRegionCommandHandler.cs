using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Regions.Commands.UpdateRegion;

public class UpdateRegionCommandHandler : IRequestHandler<UpdateRegionCommand, UpdateRegionCommandResponse>
{
    private readonly IRegionService _RegionService;

    public UpdateRegionCommandHandler(IRegionService RegionService)
    {
        _RegionService = RegionService;
    }

    public async Task<UpdateRegionCommandResponse> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
    {
        await _RegionService.UpdateAsync(request.Region);
        return new("updated");
    }
}
