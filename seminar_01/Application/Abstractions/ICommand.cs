﻿using Application.Base;
using MediatR;

namespace Application.Abstractions;

public interface ICommand<TResponse> : IRequest<TResponse>
{

}

