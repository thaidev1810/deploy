using BloodDonation.Domain.Common;
using MediatR;

namespace BloodDonation.Application.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;