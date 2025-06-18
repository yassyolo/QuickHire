using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Gigs;

namespace QuickHire.Application.Gigs.Seller.SellerGigsTable;

public record GetSellerGigsTableQuery(int ModerationStatusId) : IQuery<List<SellerGigTableRowModel>>;

