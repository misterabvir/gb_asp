using ProductApplication.Base;
using MediatR;

namespace ProductApplication.Abstractions;

public interface IQuery<TResponse> : IRequest<TResponse>
{

}
