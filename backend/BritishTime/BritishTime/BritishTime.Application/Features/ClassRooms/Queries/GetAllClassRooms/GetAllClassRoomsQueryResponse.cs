
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.ClassRoomsFeatures.Queries.GetAllClassRooms;
public sealed record GetAllClassRoomsQueryResponse(PaginatedList<ClassRoomDto> result);
