using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using System.Windows.Input;

namespace QuickHire.Application.Gigs.Seller.DeleteGig;

public record DeleteGigCommand(int Id) : ICommand<Unit>;

