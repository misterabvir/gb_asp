using StoreApplication.Base;
using MediatR;

namespace StoreApplication.Abstractions;

public interface IQuery<TResponse> : IRequest<TResponse>
{

}
