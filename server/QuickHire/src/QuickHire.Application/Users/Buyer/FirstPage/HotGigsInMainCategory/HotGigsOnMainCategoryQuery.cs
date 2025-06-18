using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Shared;
using QuickHire.Application.Users.Models.Gigs;

namespace QuickHire.Application.Users.Buyer.FirstPage.HotGigsInMainCategory;

public record HotGigsOnMainCategoryQuery() : IQuery<HotGigsModel>;
