using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Branches.Commands.UpdateBranch;

public sealed record UpdateBranchCommand(BranchDto Branch) : IRequest<UpdateBranchCommandResponse>;