using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Details;

namespace QuickHire.Application.Gigs.Details.GigDetails;

public record GetGigDetailsQuery(int Id, bool Preview) : IQuery<GigDetailsModel>;