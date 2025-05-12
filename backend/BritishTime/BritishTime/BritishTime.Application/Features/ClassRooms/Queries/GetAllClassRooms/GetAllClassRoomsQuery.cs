using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.ClassRoomsFeatures.Queries.GetAllClassRooms;

public sealed record GetAllClassRoomsQuery
    (ClassRoomFilterDto filter, PageRequest pagination) : IRequest<GetAllClassRoomsQueryResponse>;
