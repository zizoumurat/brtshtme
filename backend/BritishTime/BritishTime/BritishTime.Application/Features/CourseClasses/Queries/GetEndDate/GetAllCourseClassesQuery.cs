using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.CourseClasses.Queries.GetEndDate;

public sealed record GetEndDateQuery
    (CourseEndDateRequest request) : IRequest<GetEndDateQueryResponse>;
