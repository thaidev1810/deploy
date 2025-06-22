using System.Windows.Input;
using BloodDonation.Domain.Common;
using MediatR;

namespace BloodDonation.Application.Abstraction.Messaging;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;