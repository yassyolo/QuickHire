using MediatR;

namespace QuickHire.Application.Common.Interfaces.Abstractions;

public interface IQuery<out TResponse> 
    : IRequest<TResponse>
    where TResponse: notnull;
