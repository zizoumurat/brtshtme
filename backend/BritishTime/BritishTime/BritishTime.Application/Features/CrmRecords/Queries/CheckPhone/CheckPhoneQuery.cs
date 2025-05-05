using MediatR;


namespace BritishTime.Application.Features.CrmRecordsFeatures.Queries.CheckPhone;

public sealed record CheckPhoneQuery(string Phone) : IRequest<CheckPhoneQueryResponse>;
