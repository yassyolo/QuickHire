using MediatR;

namespace QuickHire.Application.Common.Interfaces.Abstractions;

internal interface  ICommand 
    : IRequest<Unit>;
internal interface ICommand<out TResponse> 
    : IRequest<TResponse>;


