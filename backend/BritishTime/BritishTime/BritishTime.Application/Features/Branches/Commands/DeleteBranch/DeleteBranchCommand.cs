using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Branches.Commands.DeleteBranch;

public sealed record DeleteBranchCommand(Guid Id) : IRequest<DeleteBranchCommandResponse>;