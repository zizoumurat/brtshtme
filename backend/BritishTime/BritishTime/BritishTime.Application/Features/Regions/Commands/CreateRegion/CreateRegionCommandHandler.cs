using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Regions.Commands.CreateRegion;

public class CreateRegionCommandHandler : IRequestHandler<CreateRegionCommand, CreateRegionCommandResponse>
{
    private readonly IRegionService _RegionService;

    public CreateRegionCommandHandler(IRegionService RegionService)
    {
        _RegionService = RegionService;
    }

    public async Task<CreateRegionCommandResponse> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
    {
        await _RegionService.AddAsync(request.Region);
        return new("added");
    }
}
