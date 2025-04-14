using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Regions.Commands.DeleteRegion;

public class DeleteRegionCommandHandler : IRequestHandler<DeleteRegionCommand, DeleteRegionCommandResponse>
{
    private readonly IRegionService _RegionService;

    public DeleteRegionCommandHandler(IRegionService RegionService)
    {
        _RegionService = RegionService;
    }

    public async Task<DeleteRegionCommandResponse> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
    {
        await _RegionService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
