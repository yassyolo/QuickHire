using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Seller.Gigs.GetGigForDelete;

public record GetGigForDeleteQuery(int Id) : IQuery<string[]>;

