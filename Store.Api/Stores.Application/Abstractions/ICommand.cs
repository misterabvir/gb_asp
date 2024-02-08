using StoreApplication.Base;
using MediatR;

namespace StoreApplication.Abstractions;

public interface ICommand<TResponse> : IRequest<TResponse>
{

}

