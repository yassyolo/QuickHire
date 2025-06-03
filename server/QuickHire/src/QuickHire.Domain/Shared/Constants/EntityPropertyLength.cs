namespace QuickHire.Domain.Shared.Constants;

public static class EntityPropertyLength
{
    public static class Files
    {
        public const int FileUrlMaxLength = 200;
        public const int FileUrlMinLength = 0;
    }

    public static class Portfolio {
        public const int TitleMaxLength = 100;
        public const int TitleMinLength = 10;
        public const int DescriptionMaxLength = 300;
        public const int DescriptionMinLength = 30;
    }

    public static class Notification
    {
        public const int TitleMaxLength = 50;
        public const int TitleMinLength = 2;
        public const int MessageMaxLength = 150;
        public const int MessageMinLength = 2;
    }

    public static class Certification
    {
        public const int NameMaxLength = 100;
        public const int NameMinLength = 2;
        public const int IssuerMaxLength = 100;
        public const int IssuerMinLength = 2;
    }

    public static class Education
    {
        public const int InstitutionMaxLength = 100;
        public const int InstitutionMinLength = 2;
        public const int MajorMaxLength = 50;
        public const int MajorMinLength = 2;
    }

    public static class Language
    {
        public const int NameMaxLength = 50;
        public const int NameMinLength = 2;
    }

    public static class Skill
    {
        public const int NameMaxLength = 50;
        public const int NameMinLength = 2;
    }

    public static class Address{
        public const int CountryMaxLength = 50;
        public const int CountryMinLength = 2;
        public const int CityMaxLength = 50;
        public const int CityMinLength = 2;
        public const int StreetMaxLength = 100;
        public const int StreetMinLength = 2;
        public const int ZipCodeMaxLength = 10;
        public const int ZipCodeMinLength = 4;
    }

    public static class ApplicationUser
    {
        public const int FullNameMaxLength = 100;
        public const int FullNameMinLength = 4;
        public const int DescriptionMaxLength = 300;
        public const int DescriptionMinLength = 30;
        public const int UsernameMaxLength = 30;
        public const int UsernameMinLength = 2;
    }

    public static class BillingDetails
    {
        public const int FullNameMaxLength = 100;
        public const int FullNameMinLength = 4;
        public const int CompanyNameMaxLength = 100;
        public const int CompanyNameMinLength = 2;
    }

    public static class FavouriteGigsList
    {
        public const int NameMaxLength = 30;
        public const int NameMinLength = 2;
        public const int DescriptionMaxLength = 100;
        public const int DescriptionMinLength = 2;
    }

    public static class MainCategory
    {        
        public const int DescriptionMaxLength = 100;
        public const int DescriptionMinLength = 10;
    }

    public static class Category
    {
        public const int NameMaxLength = 50;
        public const int NameMinLength = 2;
    }

    public static class GigFilter
    {
        public const int GigFilterTitleMaxLength = 50;
        public const int GigFilterTitleMinLength = 2;
    }

    public static class FAQ
    {
        public const int QuestionMaxLength = 100;
        public const int QuestionMinLength = 4;
        public const int AnswerMaxLength = 300;
        public const int AnswerMinLength = 4;
    }

    public static class ModerationItem
    {
        public const int ReasonMaxLength = 100;
        public const int ReasonMinLength = 4;
    }

    public static class Message
    {
        public const int TextMaxLength = 500;
        public const int TextMinLength = 1;
    }

    public static class OrderDeliveryDate
    {
        public const int ChangeDateReason = 100;
        public const int ChangeDateMinLength = 10;
    }

    public static class GigRequirementAnswer
    {
        public const int GigRequirementAnswerMaxLength = 300;
        public const int GigRequirementAnswerMinLength = 4;
    }

    public static class Review
    {
        public const int CommentMaxLength = 300;
        public const int CommentMinLength = 10;
        public const int RatingStarsMaxLength = 5;
        public const int RatingStarsMinLength = 1;
    }

    public static class Delivery
    {
        public const int DescriptionMaxLength = 300;
        public const int DescriptionMinLength = 10;
    }

    public static class CustomOffer
    {
        public const int DescriptionMaxLength = 150;
        public const int DescriptionMinLength = 10;
        public const int RevisionMaxCount = 5;
        public const int RevisionMinCount = 1;
    }

    public static class CustomRequest
    {
        public const int DescriptionMaxLength = 150;
        public const int DescriptionMinLength = 10;
    }

    public static class RejectedItems
    {
        public const int RejectionReasonMaxLength = 100;
        public const int RejectionReasonMinLength = 5;
    }

    public static class ProjectBrief
    {
        public const int DescriptionMaxLength = 300;
        public const int DescriptionMinLength = 10;
        public const int AboutBuyerMaxLength = 300;
        public const int AboutBuyerMinLength = 10;
    }

    public static class CustomItem
    {
        public const int CustomItemNumberMaxLength = 40;
        public const int CustomItemNumberMinLength = 2;
    }

    public static class GigRequirement
    {
        public const int QuestionMaxLength = 150;
        public const int QuestionMinLength = 2;
    }

    public static class PaymentPlan
    {
        public const int NameMaxLength = 50;
        public const int NameMinLength = 2;
        public const int DescriptionMaxLength = 100;
        public const int DescriptionMinLength = 5;
    }

    public static class PaymentPlanInclude
    {
        public const int NameMaxLength = 40;
        public const int NameMinLength = 2;
        public const int ValueMaxLength = 50;
        public const int ValueMinLength = 5;
    }

    public static class Tag
    {
        public const int NameMaxLength = 50;
        public const int NameMinLength = 2;
    }

    public static class Gig
    {
        public const int TitleMaxLength = 100;
        public const int TitleMinLength = 10;
        public const int DescriptionMaxLength = 2000;
        public const int DescriptionMinLength = 50;
    }

    public static class FilterOption
    {
        public const int NameMaxLength = 30;
        public const int NameMinLength = 2;
    }
}

