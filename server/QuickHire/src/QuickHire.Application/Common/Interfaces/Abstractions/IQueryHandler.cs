using MediatR;

namespace QuickHire.Application.Common.Interfaces.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> 
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull;
