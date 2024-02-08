using StoreApplication.Base;
using MediatR;

namespace StoreApplication.Abstractions;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{ }
