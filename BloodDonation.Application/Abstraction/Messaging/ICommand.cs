using BloodDonation.Domain.Common;
using MediatR;

namespace BloodDonation.Application.Abstraction.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;