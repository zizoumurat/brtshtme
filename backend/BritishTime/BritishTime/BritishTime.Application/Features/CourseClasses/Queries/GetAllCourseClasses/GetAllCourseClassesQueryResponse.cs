
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.CourseClasses.Queries.GetAllCourseClasses;
public sealed record GetAllCourseClassesQueryResponse(PaginatedList<CourseClassDto> result);
