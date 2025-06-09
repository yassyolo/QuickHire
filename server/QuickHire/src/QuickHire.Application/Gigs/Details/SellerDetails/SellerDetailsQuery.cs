using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Details;

namespace QuickHire.Application.Gigs.Details.SellerDetails;

public record SellerDetailsQuery(int Id) : IQuery<GigSellerDetailsModel>;
