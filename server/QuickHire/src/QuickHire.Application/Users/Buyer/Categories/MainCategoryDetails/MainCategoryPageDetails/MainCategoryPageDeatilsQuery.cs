﻿using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Buyer.Categories.MainCategoryDetails.MainCategoryPageDetails;

public record MainCategoryPageDeatilsQuery(int Id) : IQuery<MainCategoryPageDeatilsModel>;
