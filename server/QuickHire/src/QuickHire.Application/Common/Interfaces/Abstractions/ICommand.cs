using MediatR;

namespace QuickHire.Application.Common.Interfaces.Abstractions;

public interface  ICommand 
    : IRequest<Unit>;
public interface ICommand<out TResponse> 
    : IRequest<TResponse>;


