using ProductApplication.Base;
using MediatR;

namespace ProductApplication.Abstractions;

public interface ICommand<TResponse> : IRequest<TResponse>
{

}

