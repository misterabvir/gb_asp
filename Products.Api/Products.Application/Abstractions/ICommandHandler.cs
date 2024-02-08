using ProductApplication.Base;
using MediatR;

namespace ProductApplication.Abstractions;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{ }
