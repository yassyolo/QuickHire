using MediatR;

namespace QuickHire.Application.Common.Interfaces.Abstractions;

internal interface IQuery<out TResponse> 
    : IRequest<TResponse>
    where TResponse: notnull;
