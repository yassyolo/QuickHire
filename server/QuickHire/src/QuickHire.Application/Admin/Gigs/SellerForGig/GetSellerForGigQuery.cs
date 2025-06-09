using QuickHire.Application.Admin.Models.Users;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.Gigs.SellerForGig;

public record GetSellerForGigQuery(int Id) : IQuery<UserForAdminModel>;