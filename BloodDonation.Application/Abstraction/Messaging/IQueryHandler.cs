using BloodDonation.Domain.Common;
using MediatR;

namespace BloodDonation.Application.Abstraction.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;