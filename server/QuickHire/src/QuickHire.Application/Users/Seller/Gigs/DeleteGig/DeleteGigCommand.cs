using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using System.Windows.Input;

namespace QuickHire.Application.Users.Seller.Gigs.DeleteGig;

public record DeleteGigCommand(int Id) : ICommand<Unit>;

