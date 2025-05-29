using Microsoft.Extensions.DependencyInjection;
using Mapster;
using System.Reflection;
using QuickHire.Application.Users.Models.Authentication;
using QuickHire.Domain.Categories;
using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Admin.Models.FAQ;
using QuickHire.Domain.Gigs;
using QuickHire.Application.Gigs.Models.Tags;
using QuickHire.Domain.Users;
using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Admin.Models.Users.Notifications;

namespace QuickHire.Application.Common.Mapping;

internal static class MapsterConfiguration
{
    internal static void RegisterMapsterConfiguration(this IServiceCollection services, Assembly assembly)
    {
        TypeAdapterConfig<ApplicationUserModel, AboutUserModel>.NewConfig()
            .Map(dest => dest.UserId, src => src.Id)
            .Map(dest => dest.UserName, src => src.UserName);

        TypeAdapterConfig<MainCategory, MainCategoryRowModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.CreatedOn, src => src.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"))
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.SubCategories, src => src.SubCategories.Count())
            .Map(dest => dest.Clicks, src => src.Clicks);

        TypeAdapterConfig<MainCategory, GetMainCategoryForEditModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description);

        TypeAdapterConfig<MainCategory, MainCategoryDetailsModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Clicks, src => src.Clicks)
            .Map(dest => dest.CreatedOn, src => src.CreatedOn.ToString("yyyy-MM-dd"));

        TypeAdapterConfig<SubCategory, SubCategoriesInMainCategoryModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<MainCategory, MainCategoryForLinksModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<MainCategory, MainCategoryPageDeatilsModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description);

        TypeAdapterConfig<SubCategory, SubCategoryRowModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Clicks, src => src.Clicks)
            .Map(dest => dest.CreatedOn, src => src.CreatedOn.ToString("yyyy-MM-dd"))
            .Map(dest => dest.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.MainCategoryName, src => src.MainCategory.Name)
            .Map(dest => dest.SubSubCategories, src => src.SubSubCategories.Count());

        TypeAdapterConfig<MainCategory, int>.NewConfig()
            .Map(dest => dest, src => src.Id);

        TypeAdapterConfig<SubCategory, GetSubCategoryForEditModel>.NewConfig()
            .Map(dest => dest, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.ImageUrl, src => src.ImageUrl)
            .Map(dest => dest.MainCategoryId, src => src.MainCategoryId);

        TypeAdapterConfig<MainCategory, PopulateMainCategoriesModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<SubSubCategory, SubSubCategoryForSubCategoryModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<SubCategory, SubCategoryDetailsModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Clicks, src => src.Clicks)
            .Map(dest => dest.CreatedOn, src => src.CreatedOn.ToString("yyyy-MM-dd"))
            .Map(dest => dest.MainCategoryName, src => src.MainCategory.Name)
            .Map(dest => dest.ImageUrl, src => src.ImageUrl);

        TypeAdapterConfig<SubSubCategory, SubSubCategoryRowModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Clicks, src => src.Clicks)
            .Map(dest => dest.Filters, src => src.GigFilters.Count())
            .Map(dest => dest.Gigs, src => src.Gigs.Count())
            .Map(dest => dest.CreatedOn, src => src.CreatedOn.ToString("yyyy-MM-dd "))
            .Map(dest => dest.SubCategoryName, src => src.SubCategory.Name);

        TypeAdapterConfig<SubSubCategory, GetSubSubCategoryForEditModel>.NewConfig()
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<SubCategory, PopulateSubCategoriesModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<GigFilter, FilterForSubSubCategoryModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.FilterOptions, src => src.Options.Adapt<IEnumerable<FilterOptionsForGigFilterModel>>());

        TypeAdapterConfig<FilterOption, FilterOptionsForGigFilterModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<FAQ, FAQResponseModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Question, src => src.Question)
            .Map(dest => dest.Answer, src => src.Answer);

        TypeAdapterConfig<Tag, GetTagsResponseModel>.NewConfig()
            .Map(dest => dest.Label, src => src.Name);

        TypeAdapterConfig<SubSubCategory, PopulateSubCategoriesModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<Country, FilterItemModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<Notification, GetNotificationsResponseModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Message, src => src.Message)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt.ToString("yyyy-MM-dd"));

        TypeAdapterConfig<SubCategory, SubCategoriesHeaderResponseModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);
        TypeAdapterConfig<SubSubCategory, SubSubCategoriesInHeaderResponseModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name);

        TypeAdapterConfig<SubCategory, PopularSubCategoriesResponseModel>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.ImageUrl, src => src.ImageUrl);

        TypeAdapterConfig.GlobalSettings.Scan(assembly);
    }
}
