using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Gigs;

namespace QuickHire.Application.Users.Seller.Gigs.SellerGigsTable;

public record GetSellerGigsTableQuery(int ModerationStatusId) : IQuery<List<SellerGigTableRowModel>>;

