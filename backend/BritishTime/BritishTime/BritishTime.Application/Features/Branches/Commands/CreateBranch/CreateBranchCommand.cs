using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Branches.Commands.CreateBranch;

public sealed record CreateBranchCommand(BranchCreateDto Branch) : IRequest<CreateBranchCommandResponse>;