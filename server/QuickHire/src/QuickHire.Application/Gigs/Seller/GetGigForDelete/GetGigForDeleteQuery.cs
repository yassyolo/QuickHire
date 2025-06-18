using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Gigs.Seller.GetGigForDelete;

public record GetGigForDeleteQuery(int Id) : IQuery<string[]>;

