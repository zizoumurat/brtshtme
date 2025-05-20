using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.CourseClasses.Queries.GetAllCourseClasses;

public sealed record GetAllCourseClassesQuery
    (CourseClassFilterDto filter, PageRequest pagination) : IRequest<GetAllCourseClassesQueryResponse>;
