using Microsoft.AspNetCore.Identity;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Moderation.Enums;
using QuickHire.Domain.Users;
using ApplicationUser = QuickHire.Infrastructure.Persistence.Identity.ApplicationUser;
using Tag = QuickHire.Domain.Gigs.Tag;
using FAQ = QuickHire.Domain.Categories.FAQ;
using MainCategory = QuickHire.Domain.Categories.MainCategory;
using Language = QuickHire.Domain.Users.Language;
using Gig = QuickHire.Domain.Gigs.Gig;
using UserLanguage = QuickHire.Domain.Users.UserLanguage;
using Certification = QuickHire.Domain.Users.Certification;
using Education = QuickHire.Domain.Users.Education;
using Skill = QuickHire.Domain.Users.Skill;
using Address = QuickHire.Domain.Users.Address;
using Portfolio = QuickHire.Domain.Users.Portfolio;
using BillingDetails = QuickHire.Domain.Users.BillingDetails;
using GigFilter = QuickHire.Domain.Categories.GigFilter;
using FilterOption = QuickHire.Domain.Categories.FilterOption;
using PaymentPlanInclude = QuickHire.Domain.Gigs.PaymentPlanInclude;
using PaymentPlan = QuickHire.Domain.Gigs.PaymentPlan;
using GigRequirement = QuickHire.Domain.Gigs.GigRequirement;
using QuickHire.Domain.Categories.Enums;
using QuickHire.Domain.Gigs;
using static QuickHire.Domain.Shared.Constants.EntityPropertyLength;

namespace QuickHire.Infrastructure.Persistence.Seed;

public class SeedData
{

    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public SeedData(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    public Language Language1 { get; set; } = null!;
    public Language Language2 { get; set; } = null!;
    public Language Language3 { get; set; } = null!;
    public Language Language4 { get; set; } = null!;
    public Language Language5 { get; set; } = null!;
    public Language Language6 { get; set; } = null!;
    public Language Language7 { get; set; } = null!;
    public Language Language8 { get; set; } = null!;
    public Language Language9 { get; set; } = null!;
    public Language Language10 { get; set; } = null!;
    public Language Language11 { get; set; } = null!;
    public Language Language12 { get; set; } = null!;
    public Language Language13 { get; set; } = null!;
    public Language Language14 { get; set; } = null!;
    public Language Language15 { get; set; } = null!;
    public UserLanguage UserLanguage1 { get; set; } = null!;
    public UserLanguage UserLanguage2 { get; set; } = null!;
    public UserLanguage UserLanguage3 { get; set; } = null!;
    public UserLanguage UserLanguage4 { get; set; } = null!;
    public UserLanguage UserLanguage5 { get; set; } = null!;
    public UserLanguage UserLanguage6 { get; set; } = null!;
    public UserLanguage UserLanguage7 { get; set; } = null!;
    public UserLanguage UserLanguage8 { get; set; } = null!;
    public UserLanguage UserLanguage9 { get; set; } = null!;
    public UserLanguage UserLanguage10 { get; set; } = null!;
    public UserLanguage UserLanguage11{ get; set; } = null!;
    public UserLanguage UserLanguage12 { get; set; } = null!;
    public UserLanguage UserLanguage13 { get; set; } = null!;
    public UserLanguage UserLanguage14 { get; set; } = null!;
    public UserLanguage UserLanguage15 { get; set; } = null!;
    public UserLanguage UserLanguage16 { get; set; } = null!;
    public UserLanguage UserLanguage17 { get; set; } = null!;
    public UserLanguage UserLanguage18 { get; set; } = null!;
    public UserLanguage UserLanguage19 { get; set; } = null!;
    public UserLanguage UserLanguage20 { get; set; } = null!;



    public ApplicationUser User1 { get; set; } = null!;
    public ApplicationUser User2 { get; set; } = null!;
    public ApplicationUser User3 { get; set; } = null!;
    public ApplicationUser User4 { get; set; } = null!;
    public ApplicationUser User5 { get; set; } = null!;
    public ApplicationUser User6 { get; set; } = null!;
    public ApplicationUser User7 { get; set; } = null!;
    public ApplicationUser User8 { get; set; } = null!;
    public ApplicationUser User9 { get; set; } = null!;
    public ApplicationUser User10 { get; set; } = null!;
    public ApplicationUser User11 { get; set; } = null!;
    public ApplicationUser User12 { get; set; } = null!;
    public ApplicationUser User13 { get; set; } = null!;
    public ApplicationUser User14 { get; set; } = null!;
    public ApplicationUser User15 { get; set; } = null!;
    public ApplicationUser User16{ get; set; } = null!;
    public ApplicationUser User17 { get; set; } = null!;
    public ApplicationUser User18 { get; set; } = null!;
    public ApplicationUser User19 { get; set; } = null!;
    public ApplicationUser User20 { get; set; } = null!;



    public Buyer Buyer1 { get; set; } = null!;
    public Buyer Buyer2 { get; set; } = null!;
    public Buyer Buyer3 { get; set; } = null!;
    public Buyer Buyer4 { get; set; } = null!;
    public Buyer Buyer5 { get; set; } = null!;
    public Buyer Buyer6 { get; set; } = null!;
    public Buyer Buyer7 { get; set; } = null!;
    public Buyer Buyer8 { get; set; } = null!;
    public Buyer Buyer9 { get; set; } = null!;
    public Buyer Buyer10 { get; set; } = null!;


    public Seller Seller1 { get; set; } = null!;
    public Seller Seller2 { get; set; } = null!;
    public Seller Seller3 { get; set; } = null!;
    public Seller Seller4 { get; set; } = null!;
    public Seller Seller5 { get; set; } = null!;
    public Seller Seller6 { get; set; } = null!;
    public Seller Seller7 { get; set; } = null!;
    public Seller Seller8 { get; set; } = null!;
    public Seller Seller9 { get; set; } = null!;
    public Seller Seller10 { get; set; } = null!;


    public Certification Certification1 { get; set; } = null!;
    public Certification Certification2 { get; set; } = null!;
    public Certification Certification3 { get; set; } = null!;
    public Certification Certification4 { get; set; } = null!;
    public Certification Certification5 { get; set; } = null!;
    public Certification Certification6 { get; set; } = null!;
    public Education Education1 { get; set; } = null!;
    public Education Education2 { get; set; } = null!;
    public Education Education3 { get; set; } = null!;
    public Address Address1 { get; set; } = null!;
    public Address Address2 { get; set; } = null!;
    public Address Address3 { get; set; } = null!;
    public Address Address4 { get; set; } = null!;
    public Address Address5 { get; set; } = null!;
    public Address Address6 { get; set; } = null!;
    public Address Address7 { get; set; } = null!;
    public Address Address8 { get; set; } = null!;
    public Address Address9 { get; set; } = null!;
    public Address Address10 { get; set; } = null!;

    public Skill Skill1 { get; set; } = null!;
    public Skill Skill2 { get; set; } = null!;
    public Skill Skill3 { get; set; } = null!;
    public Skill Skill4 { get; set; } = null!;
    public Skill Skill5 { get; set; } = null!;
    public Skill Skill6 { get; set; } = null!;
    public Skill Skill7 { get; set; } = null!;
    public Skill Skill8 { get; set; } = null!;
    public Skill Skill9 { get; set; } = null!;
    public Skill Skill10 { get; set; } = null!;
    public Skill Skill11 { get; set; } = null!;
    public Skill Skill12 { get; set; } = null!;
    public Skill Skill13 { get; set; } = null!;
    public Skill Skill14 { get; set; } = null!;
    public Skill Skill15 { get; set; } = null!;

    public Portfolio Portfolio1 { get; set; } = null!;
    public Portfolio Portfolio2 { get; set; } = null!;
    public Portfolio Portfolio3 { get; set; } = null!;
    public Portfolio Portfolio4 { get; set; } = null!;
    public Portfolio Portfolio5 { get; set; } = null!;
    public Portfolio Portfolio6 { get; set; } = null!;
    public MainCategory MainCategory1 { get; set; } = null!;
    public MainCategory MainCategory2 { get; set; } = null!;
    public MainCategory MainCategory3 { get; set; } = null!;
    public MainCategory MainCategory4 { get; set; } = null!;
    public MainCategory MainCategory5 { get; set; } = null!;
    public MainCategory MainCategory6 { get; set; } = null!;
    public MainCategory MainCategory7 { get; set; } = null!;       
    public MainCategory MainCategory8 { get; set; } = null!;
    public MainCategory MainCategory9 { get; set; } = null!;
    public MainCategory MainCategory10 { get; set; } = null!;
    public FAQ FAQ1ForMainCategory1 { get; set; } = null!;
    public FAQ FAQ2ForMainCategory1 { get; set; } = null!;
    public FAQ FAQ3ForMainCategory1 { get; set; } = null!;
    public FAQ FAQ4ForMainCategory1 { get; set; } = null!;
    public FAQ FAQ5ForMainCategory1 { get; set; } = null!;
    public FAQ FAQ1ForMainCategory2 { get; set; } = null!;
    public FAQ FAQ2ForMainCategory2 { get; set; } = null!;
    public FAQ FAQ3ForMainCategory2 { get; set; } = null!;
    public FAQ FAQ1ForMainCategory3 { get; set; } = null!;
    public FAQ FAQ2ForMainCategory3 { get; set; } = null!;
    public FAQ FAQ3ForMainCategory3 { get; set; } = null!;
    public FAQ FAQ1ForMainCategory4 { get; set; } = null!;
    public FAQ FAQ2ForMainCategory4 { get; set; } = null!;
    public FAQ FAQ3ForMainCategory4 { get; set; } = null!;
    public FAQ FAQ1ForMainCategory5 { get; set; } = null!;
    public FAQ FAQ2ForMainCategory5 { get; set; } = null!;
    public FAQ FAQ3ForMainCategory5 { get; set; } = null!;
    public FAQ FAQ1ForMainCategory6 { get; set; } = null!;
    public FAQ FAQ2ForMainCategory6 { get; set; } = null!;
    public FAQ FAQ3ForMainCategory6 { get; set; } = null!;
    public FAQ FAQ1ForMainCategory7 { get; set; } = null!;
    public FAQ FAQ2ForMainCategory7 { get; set; } = null!;
    public FAQ FAQ3ForMainCategory7 { get; set; } = null!;
    public FAQ FAQ1ForMainCategory8 { get; set; } = null!;
    public FAQ FAQ2ForMainCategory8 { get; set; } = null!;
    public FAQ FAQ3ForMainCategory8 { get; set; } = null!;
    public FAQ FAQ1ForMainCategory9 { get; set; } = null!;
    public FAQ FAQ2ForMainCategory9 { get; set; } = null!;
    public FAQ FAQ3ForMainCategory9 { get; set; } = null!;
    public FAQ FAQ1ForMainCategory10 { get; set; } = null!;
    public FAQ FAQ2ForMainCategory10 { get; set; } = null!;
    public FAQ FAQ3ForMainCategory10 { get; set; } = null!;
    public SubCategory SubCategory1ForMainCategory1 { get; set; } = null!;
    public SubCategory SubCategory2ForMainCategory1 { get; set; } = null!;
    public SubCategory SubCategory3ForMainCategory1 { get; set; } = null!;
    public SubCategory SubCategory4ForMainCategory1 { get; set; } = null!;
    public SubCategory SubCategory5ForMainCategory1 { get; set; } = null!;
    public SubCategory SubCategory6ForMainCategory1 { get; set; } = null!;
    public SubCategory SubCategory7ForMainCategory1 { get; set; } = null!;
    public SubCategory SubCategory8ForMainCategory1 { get; set; } = null!;
    public SubCategory SubCategory1ForMainCategory2 { get; set; } = null!;
    public SubCategory SubCategory2ForMainCategory2 { get; set; } = null!;
    public SubCategory SubCategory3ForMainCategory2 { get; set; } = null!;
    public SubCategory SubCategory4ForMainCategory2 { get; set; } = null!;
    public SubCategory SubCategory5ForMainCategory2 { get; set; } = null!;
    public SubCategory SubCategory1ForMainCategory3 { get; set; } = null!;
    public SubCategory SubCategory2ForMainCategory3 { get; set; } = null!;
    public SubCategory SubCategory3ForMainCategory3 { get; set; } = null!;
    public SubCategory SubCategory4ForMainCategory3 { get; set; } = null!;
    public SubCategory SubCategory5ForMainCategory3 { get; set; } = null!;
    public SubCategory SubCategory1ForMainCategory4 { get; set; } = null!;
    public SubCategory SubCategory2ForMainCategory4 { get; set; } = null!;
    public SubCategory SubCategory3ForMainCategory4 { get; set; } = null!;
    public SubCategory SubCategory4ForMainCategory4 { get; set; } = null!;
    public SubCategory SubCategory5ForMainCategory4 { get; set; } = null!;
    public SubCategory SubCategory1ForMainCategory5 { get; set; } = null!;
    public SubCategory SubCategory2ForMainCategory5 { get; set; } = null!;
    public SubCategory SubCategory3ForMainCategory5 { get; set; } = null!;
    public SubCategory SubCategory4ForMainCategory5 { get; set; } = null!;
    public SubCategory SubCategory5ForMainCategory5 { get; set; } = null!;
    public SubCategory SubCategory1ForMainCategory6 { get; set; } = null!;
    public SubCategory SubCategory2ForMainCategory6 { get; set; } = null!;
    public SubCategory SubCategory3ForMainCategory6 { get; set; } = null!;
    public SubCategory SubCategory4ForMainCategory6 { get; set; } = null!;
    public SubCategory SubCategory5ForMainCategory6 { get; set; } = null!;
    public SubCategory SubCategory1ForMainCategory7 { get; set; } = null!;
    public SubCategory SubCategory2ForMainCategory7 { get; set; } = null!;
        
    public SubCategory SubCategory3ForMainCategory7 { get; set; } = null!;
    public SubCategory SubCategory4ForMainCategory7 { get; set; } = null!;
    public SubCategory SubCategory5ForMainCategory7 { get; set; } = null!;
    public SubCategory SubCategory1ForMainCategory8 { get; set; } = null!;
    public SubCategory SubCategory2ForMainCategory8 { get; set; } = null!;
    public SubCategory SubCategory3ForMainCategory8 { get; set; } = null!;
    public SubCategory SubCategory4ForMainCategory8 { get; set; } = null!;
    public SubCategory SubCategory5ForMainCategory8 { get; set; } = null!;
    public SubCategory SubCategory1ForMainCategory9 { get; set; } = null!;
    public SubCategory SubCategory2ForMainCategory9 { get; set; } = null!;
    public SubCategory SubCategory3ForMainCategory9 { get; set; } = null!;
    public SubCategory SubCategory4ForMainCategory9 { get; set; } = null!;
    public SubCategory SubCategory5ForMainCategory9 { get; set; } = null!;       
    public SubCategory SubCategory1ForMainCategory10 { get; set; } = null!;
    public SubCategory SubCategory2ForMainCategory10 { get; set; } = null!;
    public SubCategory SubCategory3ForMainCategory10 { get; set; } = null!;
    public SubCategory SubCategory4ForMainCategory10 { get; set; } = null!;
    public SubCategory SubCategory5ForMainCategory10 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory1ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory1ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory3ForSubCategory1ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory4ForSubCategory1ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory4ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory5ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory3ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory4ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory5ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory6ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory7ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory8ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory9ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory10ForSubCategory3ForMainCategory1 { get; set; } = null!;

    public SubSubCategory SubSubCategory1ForSubCategory4ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory4ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory3ForSubCategory4ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory4ForSubCategory4ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory5ForSubCategory4ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory5ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory5ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory3ForSubCategory5ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory4ForSubCategory5ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory5ForSubCategory5ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory6ForSubCategory5ForMainCategory1 { get; set; } = null!;

    public SubSubCategory SubSubCategory1ForSubCategory6ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory6ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory3ForSubCategory6ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory4ForSubCategory6ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory5ForSubCategory6ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory6ForSubCategory6ForMainCategory1 { get; set; } = null!;

    public SubSubCategory SubSubCategory1ForSubCategory7ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory7ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory3ForSubCategory7ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory4ForSubCategory7ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory5ForSubCategory7ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory6ForSubCategory7ForMainCategory1 { get; set; } = null!;

    public SubSubCategory SubSubCategory1ForSubCategory8ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory8ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory3ForSubCategory8ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory4ForSubCategory8ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory5ForSubCategory8ForMainCategory1 { get; set; } = null!;
    public SubSubCategory SubSubCategory6ForSubCategory8ForMainCategory1 { get; set; } = null!;




    public SubSubCategory SubSubCategory1ForSubCategory1ForMainCategory2 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory1ForMainCategory2 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory2ForMainCategory2 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory2ForMainCategory2 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory3ForMainCategory2 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory3ForMainCategory2 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory4ForMainCategory2 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory4ForMainCategory2 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory1ForMainCategory3 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory1ForMainCategory3 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory2ForMainCategory3 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory2ForMainCategory3 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory3ForMainCategory3 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory3ForMainCategory3 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory4ForMainCategory3 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory4ForMainCategory3 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory1ForMainCategory4 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory1ForMainCategory4 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory2ForMainCategory4 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory2ForMainCategory4 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory3ForMainCategory4 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory3ForMainCategory4 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory4ForMainCategory4 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory4ForMainCategory4 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory1ForMainCategory5 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory1ForMainCategory5 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory2ForMainCategory5 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory2ForMainCategory5 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory3ForMainCategory5 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory3ForMainCategory5 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory4ForMainCategory5 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory4ForMainCategory5 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory1ForMainCategory6 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory1ForMainCategory6 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory2ForMainCategory6 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory2ForMainCategory6 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory3ForMainCategory6 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory3ForMainCategory6 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory4ForMainCategory6 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory4ForMainCategory6 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory1ForMainCategory7 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory1ForMainCategory7 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory2ForMainCategory7 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory3ForMainCategory7 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory3ForMainCategory7 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory4ForMainCategory7 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory1ForMainCategory8 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory1ForMainCategory8 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory2ForMainCategory8 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory3ForMainCategory8 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory3ForMainCategory8 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory4ForMainCategory8 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory1ForMainCategory9 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory1ForMainCategory9 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory2ForMainCategory9 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory2ForMainCategory9 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory3ForMainCategory9 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory3ForMainCategory9 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory4ForMainCategory9 { get; set; } = null!;
    public SubSubCategory SubSubCategory2ForSubCategory4ForMainCategory9 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory1ForMainCategory10 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory2ForMainCategory10 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory3ForMainCategory10 { get; set; } = null!;
    public SubSubCategory SubSubCategory1ForSubCategory4ForMainCategory10 { get; set; } = null!;




    public BillingDetails BillingDetails1 { get; set; } = null!;
    public BillingDetails BillingDetails2 { get; set; } = null!;
    public BillingDetails BillingDetails3 { get; set; } = null!;
    public BillingDetails BillingDetails4 { get; set; } = null!;
    public BillingDetails BillingDetails5 { get; set; } = null!;
    public BillingDetails BillingDetails6 { get; set; } = null!;
    public BillingDetails BillingDetails7 { get; set; } = null!;
    public BillingDetails BillingDetails8 { get; set; } = null!;
    public BillingDetails BillingDetails9 { get; set; } = null!;
    public BillingDetails BillingDetails10 { get; set; } = null!;

    

    public Country Country1 { get; set; } = null!;
    public Country Country2 { get; set; } = null!;
    public Country Country3 { get; set; } = null!;
    public Country Country4 { get; set; } = null!;
    public Country Country5 { get; set; } = null!;
    public Country Country6 { get; set; }
    = null!;
    public Country Country7 { get; set; } = null!;
    public Country Country8 { get; set; } = null!;
    public Country Country9 { get; set; } = null!;
    public Country Country10 { get; set; } = null!;
    public Country Country11 { get; set; } = null!;
    public GigFilter GigFilter1 { get; set; } = null!;
    public GigFilter GigFilter2 { get; set; } = null!;
    public GigFilter GigFilter3 { get; set; } = null!;
    public GigFilter GigFilter4 { get; set; } = null!;
    public GigFilter GigFilter5 { get; set; } = null!;
    public FilterOption FilterOption1 { get; set; } = null!;
    public FilterOption FilterOption2 { get; set; } = null!;
    public FilterOption FilterOption3 { get; set; } = null!;
    public FilterOption FilterOption4 { get; set; } = null!;
    public FilterOption FilterOption5 { get; set; } = null!;
    public FilterOption FilterOption6 { get; set; } = null!;
    public FilterOption FilterOption7 { get; set; } = null!;
    public FilterOption FilterOption8 { get; set; } = null!;
    public FilterOption FilterOption9 { get; set; } = null!;
    
    public GigFilter GigFilter1ForSubSubCategory1ForSubCategory1ForMainCategory10 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory1ForSubCategory1ForMainCategory10 { get; set; } = null!;
    public GigFilter GigFilter3ForSubSubCategory1ForSubCategory1ForMainCategory10 { get; set; } = null!;

    public GigFilter GigFilter1ForSubSubCategory2ForSubCategory1ForMainCategory10 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory2ForSubCategory1ForMainCategory10 { get; set; } = null!;

    public GigFilter GigFilter1ForSubSubCategory3ForSubCategory1ForMainCategory10 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory3ForSubCategory1ForMainCategory10 { get; set; } = null!;


    public GigFilter GigFilter1ForSubSubCategory1ForSubCategory2ForMainCategory10 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory1ForSubCategory2ForMainCategory10 { get; set; } = null!;
    public GigFilter GigFilter3ForSubSubCategory1ForSubCategory2ForMainCategory10 { get; set; } = null!;


    public GigFilter GigFilterForSubSubCategory1ForSubCategory2ForMainCategory10 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory3ForMainCategory10 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory4ForMainCategory10 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory1ForMainCategory7 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory1ForMainCategory7 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory2ForMainCategory7 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory3ForMainCategory7 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory3ForMainCategory7 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory4ForMainCategory7 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory1ForMainCategory8 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory1ForMainCategory8 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory2ForMainCategory8 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory3ForMainCategory8 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory3ForMainCategory8 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory4ForMainCategory8 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory1ForMainCategory9 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory1ForMainCategory9 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory2ForMainCategory9 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory3ForMainCategory9 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory3ForMainCategory9 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory4ForMainCategory9 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory4ForMainCategory9 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory2ForMainCategory9 { get; set; } = null!;

    public GigFilter GigFilterForSubSubCategory1ForSubCategory1ForMainCategory5 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory1ForMainCategory5 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory2ForMainCategory5 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory2ForMainCategory5 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory3ForMainCategory5 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory3ForMainCategory5 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory4ForMainCategory5 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory4ForMainCategory5 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory1ForMainCategory6 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory1ForMainCategory6 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory2ForMainCategory6 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory2ForMainCategory6 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory4ForMainCategory6 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory4ForMainCategory6 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory3ForMainCategory6 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory3ForMainCategory6 { get; set; } = null!;

    public GigFilter GigFilterForSubSubCategory1ForSubCategory3ForMainCategory4 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory1ForMainCategory4 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory2ForMainCategory4 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory2ForMainCategory4 { get; set; } = null!;
            public GigFilter GigFilterForSubSubCategory2ForSubCategory3ForMainCategory4 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory4ForMainCategory4 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory4ForMainCategory4 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory1ForMainCategory4 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory4ForMainCategory3 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory4ForMainCategory3 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory3ForMainCategory3 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory3ForMainCategory3 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory2ForMainCategory3 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory2ForMainCategory3 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory1ForMainCategory3 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory1ForMainCategory3 { get; set; } = null!;
            public GigFilter GigFilterForSubSubCategory2ForSubCategory4ForMainCategory2 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory4ForMainCategory2 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory3ForMainCategory2 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory3ForMainCategory2 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory2ForMainCategory2 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory2ForMainCategory2 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory2ForSubCategory1ForMainCategory2 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory1ForMainCategory2 { get; set; } = null!;


    public GigFilter GigFilter1ForSubSubCategory1ForSubCategory1ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory1ForSubCategory1ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter3ForSubSubCategory1ForSubCategory1ForMainCategory1 { get; set; } = null!;

    public GigFilter GigFilter1ForSubSubCategory2ForSubCategory1ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory2ForSubCategory1ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilterForSubSubCategory1ForSubCategory1ForMainCategory10 { get; set; } = null!;

    public GigFilter GigFilter1ForSubSubCategory3ForSubCategory1ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory3ForSubCategory1ForMainCategory1 { get; set; } = null!;

    public GigFilter GigFilter1ForSubSubCategory1ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory1ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter3ForSubSubCategory1ForSubCategory2ForMainCategory1 { get; set; } = null!;



    public GigFilter GigFilter1ForSubSubCategory2ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory2ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter3ForSubSubCategory2ForSubCategory2ForMainCategory1 { get; set; } = null!;

    public GigFilter GigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;

    public GigFilter GigFilter1ForSubSubCategory4ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory4ForSubCategory2ForMainCategory1 { get; set; } = null!;

    public GigFilter GigFilter1ForSubSubCategory5ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory5ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter3ForSubSubCategory5ForSubCategory2ForMainCategory1 { get; set; } = null!;

    public GigFilter GigFilter1ForSubSubCategory1ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory1ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter3ForSubSubCategory1ForSubCategory3ForMainCategory1 { get; set; } = null!;

    public GigFilter GigFilter1ForSubSubCategory2ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter2ForSubSubCategory2ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter3ForSubSubCategory2ForSubCategory3ForMainCategory1 { get; set; } = null!;

    public GigFilter GigFilter1ForSubSubCategory3ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public GigFilter GigFilter3ForSubSubCategory3ForSubCategory3ForMainCategory1 { get; set; } = null!;
    public Buyer Buyer11 { get; set; } = null!;
    public Buyer Buyer12 { get; set; } = null!;
    public Buyer Buyer13 { get; set; } = null!;
    public Buyer Buyer14 { get; set; } = null!;
    public Buyer Buyer15 { get; set; } = null!;
    public Buyer Buyer16 { get; set; } = null!;
    public Buyer Buyer17 { get; set; } = null!;
    public Buyer Buyer18 { get; set; } = null!;
    public Buyer Buyer19 { get; set; } = null!;

    //App type
    public FilterOption FilterOption1ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption2ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption3ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption4ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption5ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption6ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption7ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption8ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;

    //Design tool
    public FilterOption FilterOption1ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption2ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption3ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption4ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption5ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption6ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;

    //"Service includes"
    public FilterOption FilterOption1ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption2ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption3ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;
    public FilterOption FilterOption4ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1 { get; set; } = null!;

    public Gig Gig1 { get; set; } = null!;
    public Gig Gig2 { get; set; } = null!;
    public Gig Gig3 { get; set; } = null!;
    public Gig Gig4 { get; set; } = null!;
    public Gig Gig5 { get; set; } = null!;
    public Gig Gig6 { get; set; } = null!;
    public Gig Gig7 { get; set; } = null!;
    public Gig Gig8 { get; set; } = null!;


    public async Task SeedAsync()
    {
        await SeedCountries();     
        await SeedMainCategories();
        await SeedMainCategoriesFAQs();
        await SeedSubCategories();
        await SeedSubSubCategories();
        await SeedGigFiltersForSubSubCategories();
        await SeedFilterOptions();
        await SeedApplicationUsers();
        await SeedAddresses();
        await SeedBillingDetails();
        await SeedLanguages();
        await SeedUserLanguages();
        await SeedBuyers();
        await SeedSellers();
        await SeedEducations();
        await SeedCertifications();
        await SeedSkills();
        await SeedPortfolios();
       // await SeedGigs();
    }

    private async Task SeedApplicationUsers()
    {
        if (!_context.Users.Any())
        {
            User11 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "emily.clark@example.com",
                Email = "emily.clark@example.com",
                EmailConfirmed = true,
                FullName = "Emily Clark",
                Description = "Freelance UX writer and content strategist.",
                JoinedAt = DateTime.UtcNow.AddDays(-75),
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/45.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User12 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "marco.bianchi@example.com",
                Email = "marco.bianchi@example.com",
                EmailConfirmed = true,
                FullName = "Marco Bianchi",
                Description = "UI designer focused on responsive design principles.",
                JoinedAt = DateTime.UtcNow.AddDays(-180),
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/34.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User13 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "sofia.garcia@example.com",
                Email = "sofia.garcia@example.com",
                EmailConfirmed = true,
                FullName = "Sofia Garcia",
                Description = "Graphic designer with a passion for typography.",
                JoinedAt = DateTime.UtcNow.AddDays(-210),
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/52.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User14 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "jack.wilson@example.com",
                Email = "jack.wilson@example.com",
                EmailConfirmed = true,
                FullName = "Jack Wilson",
                Description = "Motion graphics designer and animation specialist.",
                JoinedAt = DateTime.UtcNow.AddDays(-320),
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/56.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User15 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "hana.kawasaki@example.com",
                Email = "hana.kawasaki@example.com",
                EmailConfirmed = true,
                FullName = "Hana Kawasaki",
                Description = "Brand designer with experience in product packaging.",
                JoinedAt = DateTime.UtcNow.AddDays(-45),
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/19.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User16 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "alexandre.moreau@example.com",
                Email = "alexandre.moreau@example.com",
                EmailConfirmed = true,
                FullName = "Alexandre Moreau",
                Description = "UI/UX designer for mobile-first applications.",
                JoinedAt = DateTime.UtcNow.AddDays(-150),
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/39.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User17 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "olivia.nelson@example.com",
                Email = "olivia.nelson@example.com",
                EmailConfirmed = true,
                FullName = "Olivia Nelson",
                Description = "Product designer with a focus on SaaS platforms.",
                JoinedAt = DateTime.UtcNow.AddDays(-90),
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/28.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User18 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "matthew.smith@example.com",
                Email = "matthew.smith@example.com",
                EmailConfirmed = true,
                FullName = "Matthew Smith",
                Description = "UI developer and web accessibility consultant.",
                JoinedAt = DateTime.UtcNow.AddDays(-240),
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/15.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User19 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "zoe.martinez@example.com",
                Email = "zoe.martinez@example.com",
                EmailConfirmed = true,
                FullName = "Zoe Martinez",
                Description = "UX designer with experience in AR/VR interfaces.",
                JoinedAt = DateTime.UtcNow.AddDays(-55),
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/13.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User20 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin@user",
                Email = "admin@example.com",
                EmailConfirmed = true,
                FullName = "Admin",
                JoinedAt = DateTime.UtcNow.AddDays(-310),
                ProfileImageUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAALEAAACUCAMAAADrljhyAAAAMFBMVEXk5ueutLfo6uu/xMeqsbTY29zh4+TJzc+6v8KnrrG3vL/R1NbO0dPFycuzubze4OGQjycwAAAENUlEQVR4nO2cy7LjKAxAjQwYDIb//9vGzmOS3DwAEYlMcRbdi5nFKUqRZCR6mgaDwWAwGAwGg8FgMMgAgNsgn+SqnPPeqdi/N0xRbUZYa6WU6U8rzLJ7c3u9ArSbQ1IVNyTxsDndpTNMyyrubK/WIiw9hsdin+pepH1fsQGTC298d2xwHSmDnt/rniJ66yacQX064LPzGjtR9jJLOClbxe16sGT6Hs6+g1MuEU7wKxcKp1Pm9YVS4ZTmWLMcKFsqnFB8yqBDhbBYNZvxZIpjYkfOXL7ga3x3Za5Q1pXCCR5jmKti4jjkjUU51uSJC5FBGAxCWGwMwgojLAL9IcNWHcU7DMU6VhWPm0MmN3aoI07tBXmrXJ/aTpAnOI1JbSdlYuOqpu3emLaFQ2aKg4XWGO0rxExrjD9i4jY5NjAmLXugWhhT/vQqPkifQNnXw9JAmPS2BTJuBj8bU6a3YTyM/xfG288ZN8nHpNkN+wVyQHoz9HNVetItejfSC4AW3aah7Y9RF0InaD9NwaMPmfpKFv8tbUl9E8grofTDo76QxdYQ0op3gLvaTMbEvlPt1OYqTD+9qR7bnGGYqdfN8i5HbBhmeriUzDJQx9xkGQbfveWsriKWazRde+staS8J/6N23CQD2yi99lPEcQnXDnp5RrwX5fKkLGk7+T/EvFW3G+GV1Tehyk65gy09KNqykIFdOKHX7MCQpo8NWZ2bMeTcza6pz6rX3Mt5t0D8HBky9BERV/z7NCdX/oXNByAu4mVs2OC7ieBbtBfPdumlDY5xi/AtAHFbw76Avu9Qy+OvsC6xx4cKV5K0W7bZmNUYM2+L61v3BADoE7/xuukXHKfjWCcdlXI+BcQagjwRwnpEhlJRH/9PJySTFLvJNGUF+fCs6fj5pQQSkrlXugfr9Evzcwji0fRPitv/ezCbYpVOkeDmpyn4tbi1q48TS71OoeALdc9YaRZFXgJhOp7ileuejzokacrwSKVNPH+KV4KhuhiCSZmaYPjL3h9R+Lq5PhoekXvT8V1fUC9eZlYTlq8mDpX/EZrNFx90pt9bm/h9xK7fGTHAx6ek1UjxhQedMDWYRL+jeaprsgLyDju3zHSgG6wbf0Ku7Y4Zpi+kiCfKzdZwQDVOwa+d27zEAUejeyi3mP1ip7mFyvjLzwbLKmXKM1K5zSpekTJuYk19widljHCLRbxy5fqJKkQGX4F58oTaoEBRW0qwz8PqqXsq0mb/tQ5Zt1qG3sHDKFfMIFpsZmIojgvKbuIpxR2Gxq2zNaCwW+YodvcUlz62VHylbPsJsWbVjlBkzG27U/KimqcD+kNBR9TkERCeglrN1wLdk1342KvHmfzv1BbPt5uQ/caFv95dyM0WceU2PSNz1yX7yG2i4I6IsZW/J3thkvyO4iW5//TJbGUvZBovcy/k3g9BP2Secd/8A5PsOkHLyXDMAAAAAElFTkSuQmCC",
                ModerationStatus = ModerationStatus.Active,
            };
            User1 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "alice.johnson@example.com",
                Email = "alice.johnson@example.com",
                EmailConfirmed = true,
                FullName = "Alice Johnson",
                Description = "UI/UX designer with a passion for clean, functional interfaces.",
                JoinedAt = DateTime.UtcNow.AddDays(-120),
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/81.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User2 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),

                UserName = "carlos.ramirez@example.com",
                Email = "carlos.ramirez@example.com",
                EmailConfirmed = true,
                FullName = "Carlos Ramirez",
                Description = "Full-stack developer specializing in modern JavaScript frameworks.",
                JoinedAt = DateTime.UtcNow.AddDays(-200),
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/76.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User3 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),

                UserName = "fatima.alsayed@example.com",
                Email = "fatima.alsayed@example.com",
                EmailConfirmed = true,
                FullName = "Fatima Al-Sayed",
                Description = "Freelance illustrator and mobile UI artist.",
                JoinedAt = DateTime.UtcNow.AddDays(-50),
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/9.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User4 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),

                UserName = "liam.oconnor@example.com",
                Email = "liam.oconnor@example.com",
                EmailConfirmed = true,
                FullName = "Liam O'Connor",
                Description = "UX researcher with a strong background in psychology.",
                JoinedAt = DateTime.UtcNow.AddDays(-300),
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/88.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User5 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),

                UserName = "sakura.tanaka@example.com",
                Email = "sakura.tanaka@example.com",
                EmailConfirmed = true,
                FullName = "Sakura Tanaka",
                Description = "Interaction designer focused on accessibility and usability.",
                JoinedAt = DateTime.UtcNow.AddDays(-90),
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/7.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User6 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),

                UserName = "mohamed.abdallah@example.com",
                Email = "mohamed.abdallah@example.com",
                EmailConfirmed = true,
                FullName = "Mohamed Abdallah",
                Description = "Front-end developer and UX prototyper.",
                JoinedAt = DateTime.UtcNow.AddDays(-60),
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/92.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User7 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),

                UserName = "nina.petrova@example.com",
                Email = "nina.petrova@example.com",
                EmailConfirmed = true,
                FullName = "Nina Petrova",
                Description = "Web designer with expertise in Adobe XD and Figma.",
                JoinedAt = DateTime.UtcNow.AddDays(-180),
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/62.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User8 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),

                UserName = "thomas.muller@example.com",
                Email = "thomas.muller@example.com",
                EmailConfirmed = true,
                FullName = "Thomas Müller",
                Description = "UX strategist and product designer.",
                JoinedAt = DateTime.UtcNow.AddDays(-250),
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/23.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User9 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),

                UserName = "isabella.rossi@example.com",
                Email = "isabella.rossi@example.com",
                EmailConfirmed = true,
                FullName = "Isabella Rossi",
                Description = "Creative UX designer with a flair for branding.",
                JoinedAt = DateTime.UtcNow.AddDays(-40),
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/65.jpg",
                ModerationStatus = ModerationStatus.Active,
            };
            User10 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),

                UserName = "david.kim@example.com",
                Email = "david.kim@example.com",
                EmailConfirmed = true,
                FullName = "David Kim",
                Description = "UX/UI designer passionate about human-centered design.",
                JoinedAt = DateTime.UtcNow.AddDays(-130),
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/49.jpg",
                ModerationStatus = ModerationStatus.Active,
            };

            var users = new[]
            {
    User1, User2, User3, User4, User5, User6, User7, User8, User9, User10,
    User11, User12, User13, User14, User15, User16, User17, User18, User19, User20
};

            foreach (var user in users)
            {
                var result = await _userManager.CreateAsync(user, "Password123!");
                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Failed to create user {user.UserName}: {errors}");
                }
            }
        }
    }

    private async Task SeedBuyers()
    {
        if (!_context.Buyers.Any())
        {
            Buyer1 = new Buyer { UserId = User1.Id };
            Buyer2 = new Buyer { UserId = User2.Id };
            Buyer3 = new Buyer { UserId = User3.Id };
            Buyer4 = new Buyer { UserId = User4.Id };
            Buyer5 = new Buyer { UserId = User5.Id };
            Buyer6 = new Buyer { UserId = User6.Id };
            Buyer7 = new Buyer { UserId = User7.Id };
            Buyer8 = new Buyer { UserId = User8.Id };
            Buyer9 = new Buyer { UserId = User9.Id };
            Buyer10 = new Buyer { UserId = User10.Id };
            Buyer11 = new Buyer { UserId = User11.Id };
            Buyer12 = new Buyer { UserId = User12.Id };
            Buyer13 = new Buyer { UserId = User13.Id };
            Buyer14 = new Buyer { UserId = User14.Id };
            Buyer15 = new Buyer { UserId = User15.Id };
            Buyer16 = new Buyer { UserId = User16.Id };
            Buyer17 = new Buyer { UserId = User17.Id };
            Buyer18 = new Buyer { UserId = User18.Id };
            Buyer19 = new Buyer { UserId = User19.Id };



            var buyers = new List<Buyer>
{
    Buyer1,
    Buyer2,
    Buyer3,
    Buyer4,
    Buyer5,
    Buyer6,
    Buyer7,
    Buyer8,
    Buyer9,
    Buyer10,
    Buyer11,
    Buyer12,
    Buyer13,
    Buyer14,
    Buyer15,
    Buyer16,
    Buyer17,
    Buyer18,
    Buyer19
};

            _context.Buyers.AddRange(buyers);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedSellers()
    {
        if (!_context.Sellers.Any())
        {
            Seller1 = new Seller
            {
                UserId = User1.Id,
                Clicks = 1000,
                IndustryId = MainCategory1.Id,
            };

            Seller2 = new Seller
            {
                UserId = User2.Id,
                Clicks = 350,
                IndustryId = MainCategory1.Id,
            };

            Seller3 = new Seller
            {
                UserId = User3.Id,
                Clicks = 200,
                IndustryId = MainCategory1.Id,
            };

            Seller4 = new Seller
            {
                UserId = User4.Id,
                Clicks = 250,
                IndustryId = MainCategory1.Id,
            };

            Seller5 = new Seller
            {
                UserId = User5.Id,
                Clicks = 300,
                IndustryId = MainCategory1.Id,
            };

            Seller6 = new Seller
            {
                UserId = User6.Id,
                Clicks = 350,
                IndustryId = MainCategory1.Id,
            };

            Seller7 = new Seller
            {
                UserId = User7.Id,
                Clicks = 400,
                IndustryId = MainCategory1.Id,
            };

            Seller8 = new Seller
            {
                UserId = User8.Id,
                Clicks = 450,
                IndustryId = MainCategory1.Id,
            };

            Seller9 = new Seller
            {
                UserId = User9.Id,
                Clicks = 500,
                IndustryId = MainCategory1.Id,
            };

            Seller10 = new Seller
            {
                UserId = User10.Id,
                Clicks = 550,
                IndustryId = MainCategory1.Id,
            };



            var sellers = new List<Seller>
{
    Seller1, Seller2, Seller3, Seller4, Seller5,
    Seller6, Seller7, Seller8, Seller9, Seller10
};

            _context.Sellers.AddRange(sellers);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedEducations()
    {
        if (!_context.Educations.Any())
        {
            var educationsForSeller1 = new List<Education>
{
    new Education
    {
        Degree = "Bachelor of Fine Arts",
        Institution = "Art University",
        Major = "Graphic Design",
        GraduationYear = 2015,
        SellerId = Seller1.Id
    },
    new Education
    {
        Degree = "Master of Interaction Design",
        Institution = "Design Institute",
        Major = "Human-Computer Interaction",
        GraduationYear = 2018,
        SellerId = Seller1.Id
    }
};

            _context.Educations.AddRange(educationsForSeller1);
            await _context.SaveChangesAsync();
        }
        
    }
    private async Task SeedAddresses()
    {
        if (!_context.Addresses.Any())
        {
            Address1 = new Address
            {
                Street = "221B Baker St",
                City = "London",
                ZipCode = "NW1 6XE",
                IsBillingAddress = true,
                CountryId = Country3.Id,
                UserId = User1.Id
            };

            Address2 = new Address
            {
                Street = "10 Downing St",
                City = "London",
                ZipCode = "SW1A 2AA",
                UserId = User2.Id,
                IsBillingAddress = true,
                CountryId = Country3.Id,
            };

            Address3 = new Address
            {
                Street = "50 Queen’s Rd",
                City = "Brighton",
                ZipCode = "BN1 3XB",
                UserId = User3.Id,
                IsBillingAddress = true,
                CountryId = Country3.Id,

            };

            Address4 = new Address
            {
                Street = "14 King St",
                City = "Manchester",
                ZipCode = "M2 6AG",
                UserId = User4.Id,
                IsBillingAddress = true,
                CountryId = Country3.Id,

            };

            Address5 = new Address
            {
                Street = "25 Royal Ave",
                City = "Belfast",
                ZipCode = "BT1 1FB",
                UserId = User5.Id,
                IsBillingAddress = true,
                CountryId = Country3.Id,

            };

            Address6 = new Address
            {
                Street = "18 Princes St",
                City = "Edinburgh",
                ZipCode = "EH2 2AN",
                UserId = User6.Id,
                IsBillingAddress = true,
                CountryId = Country3.Id,

            };

            Address7 = new Address
            {
                Street = "12 Victoria Rd",
                City = "Glasgow",
                ZipCode = "G42 7AA",
                UserId = User7.Id,
                IsBillingAddress = true,
                CountryId = Country3.Id,

            };

            Address8 = new Address
            {
                Street = "123 Main St",
                City = "New York",
                ZipCode = "10001",
                UserId = User8.Id,
                IsBillingAddress = true,
                CountryId = Country1.Id,

            };

            Address9 = new Address
            {
                Street = "456 Elm St",
                City = "Chicago",
                ZipCode = "60610",
                UserId = User9.Id,
                IsBillingAddress = true,
                Country = Country1, // USA
                CountryId = Country1.Id
            };

            Address10 = new Address
            {
                Street = "789 Sunset Blvd",
                City = "Los Angeles",
                ZipCode = "90028",
                UserId = User10.Id,
                IsBillingAddress = true,
                Country = Country1, // USA
                CountryId = Country1.Id
            };

            var list = new List<Address>()
        {
            Address1, Address2, Address3, Address4, Address5, Address6, Address7,
            Address8, Address9, Address10
        };

            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedCertifications()
    {
        if (!_context.Certifications.Any())
        {
            var certificationsForSeller1 = new List<Certification>
{
    new Certification
    {
        Name = "Adobe Certified Expert (ACE)",
        Issuer = "Adobe",
        IssuedAt = new DateTime(2019, 5, 12),
        SellerId = Seller1.Id
    },
    new Certification
    {
        Name = "UX Design Professional Certificate",
        Issuer = "Google",
        IssuedAt = new DateTime(2020, 9, 1),
        SellerId = Seller1.Id
    },
    new Certification
    {
        Name = "Human-Centered Design Certificate",
        Issuer = "Coursera / University of California San Diego",
        IssuedAt = new DateTime(2021, 3, 20),
        SellerId = Seller1.Id
    }
};

            _context.Certifications.AddRange(certificationsForSeller1);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedSkills()
    {
        if (!_context.Skills.Any())
        {
            var skillsForSeller1 = new List<Skill>
{
    new Skill { Name = "UI Design", SellerId = Seller1.Id },
    new Skill { Name = "UX Research", SellerId = Seller1.Id },
    new Skill { Name = "Prototyping", SellerId = Seller1.Id },
    new Skill { Name = "Wireframing", SellerId = Seller1.Id },
    new Skill { Name = "Adobe XD", SellerId = Seller1.Id },
    new Skill { Name = "Figma", SellerId = Seller1.Id },
    new Skill { Name = "User Testing", SellerId = Seller1.Id },
};

            _context.Skills.AddRange(skillsForSeller1);
            await _context.SaveChangesAsync();
        }
    }   
    private async Task SeedPortfolios() { 
        if (!_context.Portfolios.Any())
        {
            var portfoliosForSeller1 = new List<Portfolio>
{
    new Portfolio
    {
        Title = "Mobile App Redesign",
        Description = "Redesigned the UI/UX for a leading fitness mobile application to improve usability and engagement.",
        ImageUrl = "https://fiverr-res.cloudinary.com/images/t_main1,q_auto,f_auto,q_auto,f_auto/gigs/183302979/original/6b0e602157e118b8dc576730b2199ee071ea173b/design-wireframes-ui-design-prototype-on-figma.png",
        SellerId = Seller1.Id,
        MainCategoryId = Seller1.IndustryId
    },
    new Portfolio
    {
        Title = "E-Commerce Website UI",
        Description = "Created an intuitive and accessible interface for a global e-commerce platform with a focus on mobile-first design.",
        ImageUrl = "https://fiverr-res.cloudinary.com/images/t_main1,q_auto,f_auto,q_auto,f_auto/gigs/391888067/original/d5c79dd691b418b88da77cf4f1581c1cec75e0aa/design-stunning-ui-ux-experiences-in-figma-for-your-project.png",
        SellerId = Seller1.Id,
        MainCategoryId = Seller1.IndustryId
    },
    new Portfolio
    {
        Title = "Landing Page for Startup",
        Description = "Designed and prototyped a modern landing page for a SaaS startup to increase conversion rates.",
        ImageUrl = "https://fiverr-res.cloudinary.com/images/t_main1,q_auto,f_auto,q_auto,f_auto/gigs/370976683/original/d1f54595465130e8e005253713e4c812df36b6c0/do-saas-design-desktop-application-and-web-app-ui-ux-in-figma.jpg",
        SellerId = Seller1.Id,
        MainCategoryId = Seller1.IndustryId
    }
};

            _context.Portfolios.AddRange(portfoliosForSeller1);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedLanguages()
    {
        if (!_context.Languages.Any())
        {
            Language1 = new Language { Name = "English" };
            Language2 = new Language {Name = "Spanish" };
            Language3 = new Language {Name = "French" };
            Language4 = new Language {  Name = "German" };
            Language5 = new Language { Name = "Chinese" };
            Language6 = new Language {Name = "Japanese" };
            Language7 = new Language { Name = "Korean" };
            Language8 = new Language {Name = "Arabic" };
            Language9 = new Language {  Name = "Portuguese" };
            Language10 = new Language { Name = "Russian" };
            Language11 = new Language { Name = "Hindi" };
            Language12 = new Language {  Name = "Italian" };
            Language13 = new Language {  Name = "Dutch" };
            Language14 = new Language { Name = "Turkish" };
            Language15 = new Language {  Name = "Polish" };

            _context.Languages.AddRange(Language1, Language2, Language2, Language3, Language4, 
                Language5, Language6, Language7, Language8, Language9, Language10, Language11, 
                Language12, Language13, Language14, Language15);

            await _context.SaveChangesAsync();
        }      
    }
    private async Task SeedUserLanguages()
    {
        if (!_context.UserLanguages.Any())
        {
            UserLanguage1 = new UserLanguage { UserId = User1.Id, LanguageId = Language1.Id }; // Alice - English
            UserLanguage2 = new UserLanguage { UserId = User1.Id, LanguageId = Language3.Id }; // Alice - French

            UserLanguage3 = new UserLanguage { UserId = User2.Id, LanguageId = Language2.Id }; // Carlos - Spanish
            UserLanguage4 = new UserLanguage { UserId = User2.Id, LanguageId = Language1.Id }; // Carlos - English

            UserLanguage5 = new UserLanguage { UserId = User3.Id, LanguageId = Language8.Id }; // Fatima - Arabic
            UserLanguage6 = new UserLanguage { UserId = User3.Id, LanguageId = Language1.Id }; // Fatima - English

            UserLanguage7 = new UserLanguage { UserId = User4.Id, LanguageId = Language1.Id }; // Liam - English
            UserLanguage8 = new UserLanguage { UserId = User4.Id, LanguageId = Language4.Id }; // Liam - German

            UserLanguage9 = new UserLanguage { UserId = User5.Id, LanguageId = Language6.Id }; // Sakura - Japanese
            UserLanguage10 = new UserLanguage { UserId = User5.Id, LanguageId = Language1.Id }; // Sakura - English

            UserLanguage11 = new UserLanguage { UserId = User6.Id, LanguageId = Language8.Id }; // Mohamed - Arabic
            UserLanguage12 = new UserLanguage { UserId = User6.Id, LanguageId = Language1.Id }; // Mohamed - English

            UserLanguage13 = new UserLanguage { UserId = User7.Id, LanguageId = Language10.Id }; // Nina - Russian
            UserLanguage14 = new UserLanguage { UserId = User7.Id, LanguageId = Language1.Id };  // Nina - English

            UserLanguage15 = new UserLanguage { UserId = User8.Id, LanguageId = Language4.Id };  // Thomas - German
            UserLanguage16 = new UserLanguage { UserId = User8.Id, LanguageId = Language1.Id };  // Thomas - English

            UserLanguage17 = new UserLanguage { UserId = User9.Id, LanguageId = Language12.Id }; // Isabella - Italian
            UserLanguage18 = new UserLanguage { UserId = User9.Id, LanguageId = Language1.Id };  // Isabella - English

            UserLanguage19 = new UserLanguage { UserId = User10.Id, LanguageId = Language7.Id }; // David - Korean
            UserLanguage20 = new UserLanguage { UserId = User10.Id, LanguageId = Language1.Id }; // David - English

            // Then add to context:
            _context.UserLanguages.AddRange(
                UserLanguage1, UserLanguage2, UserLanguage3, UserLanguage4, UserLanguage5,
                UserLanguage6, UserLanguage7, UserLanguage8, UserLanguage9, UserLanguage10,
                UserLanguage11, UserLanguage12, UserLanguage13, UserLanguage14, UserLanguage15,
                UserLanguage16, UserLanguage17, UserLanguage18, UserLanguage19, UserLanguage20
            );
            await _context.SaveChangesAsync();
        }      
    }
    private async Task SeedBillingDetails()
    {
        if (!_context.BillingDetails.Any())
        {
            BillingDetails1 = new BillingDetails
            {
                FullName = User1.FullName,
                Address = Address1,
                AddressId = Address1.Id,
                CompanyName = "UX Studio",
                UserId = User1.Id
            };
            BillingDetails2 = new BillingDetails
            {
                FullName = User2.FullName,
                Address = Address2,
                AddressId = Address2.Id,
                CompanyName = "JS Solutions",
                UserId = User2.Id
            };
            BillingDetails3 = new BillingDetails
            {
                FullName = User3.FullName,
                Address = Address3,
                AddressId = Address3.Id,
                CompanyName = "Fatima Illustrations",
                UserId = User3.Id
            };
            BillingDetails4 = new BillingDetails
            {
                FullName = User4.FullName,
                Address = Address4,
                AddressId = Address4.Id,
                CompanyName = "Insight UX",
                UserId = User4.Id
            };
            BillingDetails5 = new BillingDetails
            {
                FullName = User5.FullName,
                Address = Address5,
                AddressId = Address5.Id,
                CompanyName = "Tanaka Interaction Design",
                UserId = User5.Id
            };
            BillingDetails6 = new BillingDetails
            {
                FullName = User6.FullName,
                Address = Address6,
                AddressId = Address6.Id,
                CompanyName = "Frontend Works",
                UserId = User6.Id
            };
            BillingDetails7 = new BillingDetails
            {
                FullName = User7.FullName,
                Address = Address7,
                AddressId = Address7.Id,
                CompanyName = "Nina Web Studio",
                UserId = User7.Id
            };
            BillingDetails8 = new BillingDetails
            {
                FullName = User8.FullName,
                Address = Address8,
                AddressId = Address8.Id,
                CompanyName = "UX Strategies GmbH",
                UserId = User8.Id
            };
            BillingDetails9 = new BillingDetails
            {
                FullName = User9.FullName,
                Address = Address9,
                AddressId = Address9.Id,
                CompanyName = "Rossi Branding",
                UserId = User9.Id
            };
            BillingDetails10 = new BillingDetails
            {
                FullName = User10.FullName,
                Address = Address10,
                AddressId = Address10.Id,
                CompanyName = "Human-Centered Design Co.",
                UserId = User10.Id
            };

            var billingList = new[]
            {
            BillingDetails1, BillingDetails2, BillingDetails3, BillingDetails4, BillingDetails5,
            BillingDetails6, BillingDetails7, BillingDetails8, BillingDetails9, BillingDetails10
        };

            _context.BillingDetails.AddRange(billingList);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedCountries()
    {
        if (!_context.Countries.Any())
        {
            Country1 = new Country{ Name = "USA"};
            Country2 = new Country { Name = "Canada"};
            Country3 = new Country { Name = "UK"};
            Country4 = new Country { Name = "France"};
            Country5 = new Country { Name = "Germany"};
            Country6 = new Country {Name = "Spain"};
            Country7 = new Country { Name = "Italy"};
            Country8 = new Country { Name = "Australia"};
            Country9 = new Country { Name = "India"};
            Country10 = new Country { Name = "Brazil"};
            Country11 = new Country { Name = "Japan"};

            var countries = new List<Country>
            {
                Country1, Country2, Country3, Country4, Country5,
                Country6, Country7, Country8, Country9, Country10,
                Country11
            };
            _context.Countries.AddRange(countries);
            await _context.SaveChangesAsync();

        }
    } 
    private async Task SeedMainCategories()
    {
        if (!_context.MainCategories.Any())
        {
            var random = new Random();

            MainCategory1 = new MainCategory
            {
                Name = "Graphics & Design",
                Description = "Designs to make you stand out.",
                Clicks = 10,
                CreatedOn = DateTime.UtcNow.AddDays(-random.Next(1, 365))
            };
           MainCategory2 = new MainCategory
           {
               Name = "Programming & Tech",
               Description = "Solutions built to power your ideas.",
               Clicks = 1,
               CreatedOn = DateTime.UtcNow.AddDays(-random.Next(1, 365))
           };
            MainCategory3 = new MainCategory
            {
                Name = "Digital Marketing",
                Description = "Marketing that moves the needle.",
                Clicks = 2,
                CreatedOn = DateTime.UtcNow.AddDays(-random.Next(1, 365))
            };
           MainCategory4 = new MainCategory
           {
               Name = "Video & Animation",
               Description = "Bring your story to life on screen.",
               Clicks = 3,
               CreatedOn = DateTime.UtcNow.AddDays(-random.Next(1, 365))
           };
         MainCategory5 = new MainCategory
         {
             Name = "Writing & Translation",
             Description = "Words that inspire, sell, and connect.",
             Clicks = 4,
             CreatedOn = DateTime.UtcNow.AddDays(-random.Next(1, 365))
         };
          MainCategory6 = new MainCategory
          {
              Name = "Music & Audio",
              Description = "Soundtracks to elevate your brand.",
              Clicks = 5,
              CreatedOn = DateTime.UtcNow.AddDays(-random.Next(1, 365))
          };
           MainCategory7 = new MainCategory
           {
               Name = "Business",
               Description = "Support and strategy for serious growth.",
               Clicks = 6,
               CreatedOn = DateTime.UtcNow.AddDays(-random.Next(1, 365))
           };
            MainCategory8 = new MainCategory
            {
                Name = "Finance",
                Description = "Expert help to manage your money smarter.",
                Clicks = 7,
                CreatedOn = DateTime.UtcNow.AddDays(-random.Next(1, 365))
            };
            MainCategory9 = new MainCategory
            {
                Name = "AI Services",
                Description = "Intelligent tools built by AI experts.",
                Clicks = 8,
                CreatedOn = DateTime.UtcNow.AddDays(-random.Next(1, 365))
            };
            MainCategory10 = new MainCategory
            {
                Name = "Personal Growth",
                Description = "Coaching and courses to level up your life.",
                Clicks = 9,
                CreatedOn = DateTime.UtcNow.AddDays(-random.Next(1, 365))
            };

        var mainCategories = new List<MainCategory>
        {
                MainCategory1,
                MainCategory2,
                MainCategory3,
                MainCategory4,
                MainCategory5,
                MainCategory6,
                MainCategory7,
                MainCategory8,
                MainCategory9,
                MainCategory10
            };
            _context.AddRange(mainCategories);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedMainCategoriesFAQs()
    {
        if (!_context.FAQs.Any())
        {
            FAQ1ForMainCategory1 = new FAQ
            {
                Question = "What is graphic design?",
                Answer = "Put simply, graphic design is the art of visually communicating ideas using typography, imagery, color, and layout. It’s used to tell stories, build brands, and inspire action.",
                MainCategoryId = MainCategory1.Id
            };

            FAQ2ForMainCategory1 = new FAQ
            {
                Question = "What services do graphic designers offer?",
                Answer = "From logos and brand kits to social media posts and packaging, graphic designers create all the visual elements your business needs to shine.",
                MainCategoryId = MainCategory1.Id
            };

            FAQ3ForMainCategory1 = new FAQ
            {
                Question = "How do I pick the right designer?",
                Answer = "Check their portfolio, read reviews, and see if their style matches your vision. Don’t be afraid to ask for a sample or a quick concept sketch.",
                MainCategoryId = MainCategory1.Id
            };
            FAQ4ForMainCategory1 = new FAQ
            {
                Question = "How do I ensure my design aligns with my brand?",
                Answer = "Provide your freelancer with brand guidelines, including your color palette, fonts, and tone of voice. This will ensure consistency across your visual identity.",
                MainCategoryId = MainCategory1.Id
            };

            FAQ5ForMainCategory1 = new FAQ
            {
                Question = "Can I request multiple design revisions?",
                Answer = "Yes, most designers offer a set number of revisions. Be sure to check revision policies before starting, so you can clarify expectations.",
                MainCategoryId = MainCategory1.Id
            };

            FAQ1ForMainCategory2 = new FAQ
            {
                Question = "What kind of programming services can I hire?",
                Answer = "From full-stack development to API integrations and bug fixes, developers on Fiverr offer it all—across dozens of frameworks and languages.",
                MainCategoryId = MainCategory2.Id
            };

            FAQ2ForMainCategory2 = new FAQ
            {
                Question = "Can I get my website built here?",
                Answer = "Absolutely. You can hire web developers to build personal, business, or eCommerce sites tailored to your brand.",
                MainCategoryId = MainCategory2.Id
            };

            FAQ3ForMainCategory2 = new FAQ
            {
                Question = "What if I don’t know what tech stack to use?",
                Answer = "Many developers offer consultation services to help you choose the best tools and technologies for your goals.",
                MainCategoryId = MainCategory2.Id
            };

            FAQ1ForMainCategory3 = new FAQ
            {
                Question = "What is digital marketing?",
                Answer = "Digital marketing includes SEO, social media, paid ads, and email campaigns to help grow your brand online.",
                MainCategoryId = MainCategory3.Id
            };

            FAQ2ForMainCategory3 = new FAQ
            {
                Question = "Can I get help with my social media?",
                Answer = "Yes! You can hire marketers to create posts, plan strategies, and even manage your social media accounts.",
                MainCategoryId = MainCategory3.Id
            };

            FAQ3ForMainCategory3 = new FAQ
            {
                Question = "How do I know if a campaign worked?",
                Answer = "Most sellers provide performance reports including metrics like reach, engagement, conversions, and ROI.",
                MainCategoryId = MainCategory3.Id
            };
            FAQ1ForMainCategory4 = new FAQ
            {
                Question = "What types of videos can I get made?",
                Answer = "Explainers, YouTube intros, 3D animation, product demos—you name it. There's a freelancer for every story.",
                MainCategoryId = MainCategory4.Id
            };
            FAQ2ForMainCategory4 = new FAQ
            {
                Question = "How do I brief a video creator?",
                Answer = "Provide a script or key points, tone of voice, duration, references, and your branding style. The more context, the better the outcome.",
                MainCategoryId = MainCategory4.Id
            };
            FAQ3ForMainCategory4 = new FAQ
            {
                Question = "What file formats will I get?",
                Answer = "Typically MP4, MOV, or custom formats as needed. Discuss final usage (social, web, TV) to ensure correct specs.",
                MainCategoryId = MainCategory4.Id
            };
            FAQ1ForMainCategory5 = new FAQ
            {
                Question = "What writing services are available?",
                Answer = "Copywriting, blog posts, product descriptions, and even full eBooks—our writers do it all.",
                MainCategoryId = MainCategory5.Id
            };
            FAQ2ForMainCategory5 = new FAQ
            {
                Question = "Can I get content in multiple languages?",
                Answer = "Yes! Many freelancers are bilingual or work in teams to deliver multilingual content with cultural accuracy.",
                MainCategoryId = MainCategory5.Id
            };
            FAQ3ForMainCategory5 = new FAQ
            {
                Question = "Do writers help with SEO?",
                Answer = "Definitely. Many specialize in SEO-optimized writing that helps boost your Google rankings and clicks.",
                MainCategoryId = MainCategory5.Id
            };
            FAQ1ForMainCategory6 = new FAQ
            {
                Question = "What can I hire a freelancer for in audio?",
                Answer = "Voiceovers, mixing & mastering, jingles, podcasts, and even full song production. Sound is just a gig away.",
                MainCategoryId = MainCategory6.Id
            };
            FAQ2ForMainCategory6 = new FAQ
            {
                Question = "How do I find the right voice for my brand?",
                Answer = "Filter by language, accent, tone, and listen to audio samples to find the perfect voice for your message.",
                MainCategoryId = MainCategory6.Id
            };
            FAQ3ForMainCategory6 = new FAQ
            {
                Question = "Do freelancers compose original music?",
                Answer = "Yes! Whether it’s for games, ads, or albums—original compositions are one of Fiverr’s creative gems.",
                MainCategoryId = MainCategory6.Id
            };
            FAQ1ForMainCategory7 = new FAQ
            {
                Question = "What types of business services are on Fiverr?",
                Answer = "From virtual assistants to business plans, presentations, market research, and pitch decks—grow smarter with expert help.",
                MainCategoryId = MainCategory7.Id
            };
            FAQ2ForMainCategory7 = new FAQ
            {
                Question = "Can freelancers help with business strategy?",
                Answer = "Absolutely. Many offer consulting gigs and will help define your roadmap, pricing models, or go-to-market strategy.",
                MainCategoryId = MainCategory7.Id
            };
            FAQ3ForMainCategory7 = new FAQ
            {
                Question = "How secure is it to share sensitive documents?",
                Answer = "Fiverr has NDAs and secure channels—plus, you can always ask freelancers to sign a custom agreement.",
                MainCategoryId = MainCategory7.Id
            };
            FAQ1ForMainCategory8 = new FAQ
            {
                Question = "What financial services are available?",
                Answer = "Bookkeeping, financial modeling, investment analysis, tax prep—you can hire experts for startups or enterprise.",
                MainCategoryId = MainCategory8.Id
            };
            FAQ2ForMainCategory8 = new FAQ
            {
                Question = "Can I trust financial freelancers?",
                Answer = "Freelancers are rated by previous clients, and many hold professional certifications like CPA or CFA.",
                MainCategoryId = MainCategory8.Id
            };
            FAQ3ForMainCategory8 = new FAQ
            {
                Question = "How do I know what reports I need?",
                Answer = "Start with your goals—monthly reports, forecasts, or tax prep—and your freelancer will guide the rest.",
                MainCategoryId = MainCategory8.Id
            };
            FAQ1ForMainCategory9 = new FAQ
            {
                Question = "What can AI freelancers do?",
                Answer = "They build AI models, chatbots, content generators, data classifiers, and more. Your next AI project starts here.",
                MainCategoryId = MainCategory9.Id
            };
            FAQ2ForMainCategory9 = new FAQ
            {
                Question = "How much AI experience do freelancers have?",
                Answer = "Many come from top universities or have worked in startups. Look for project examples and specialties like NLP or vision.",
                MainCategoryId = MainCategory9.Id
            };
            FAQ3ForMainCategory9 = new FAQ
            {
                Question = "Can I use AI to automate my business?",
                Answer = "Yes. From automating emails to predicting customer behavior—AI freelancers help you scale intelligently.",
                MainCategoryId = MainCategory9.Id
            };
            FAQ1ForMainCategory10 = new FAQ
            {
                Question = "What does personal growth coaching include?",
                Answer = "Life coaching, productivity mentoring, mindfulness, or learning new skills—get guidance to reach your goals.",
                MainCategoryId = MainCategory10.Id
            };
            FAQ2ForMainCategory10 = new FAQ
            {
                Question = "Can I find health and wellness experts?",
                Answer = "Yes! From fitness plans to meditation guides, Fiverr has freelancers to support your mind and body.",
                MainCategoryId = MainCategory10.Id
            };
            FAQ3ForMainCategory10 = new FAQ
            {
                Question = "How often should I meet with a coach?",
                Answer = "It depends on your goals. Some meet weekly, others monthly. Set milestones and choose what keeps you accountable.",
                MainCategoryId = MainCategory10.Id
            };

            var list = new List<FAQ>
        {
            FAQ1ForMainCategory1,
            FAQ2ForMainCategory1,
            FAQ3ForMainCategory1,
            FAQ4ForMainCategory1,
            FAQ5ForMainCategory1,
            FAQ1ForMainCategory2,
            FAQ2ForMainCategory2,
            FAQ3ForMainCategory2,
            FAQ1ForMainCategory3,
            FAQ2ForMainCategory3,
            FAQ3ForMainCategory3,
            FAQ1ForMainCategory4,
            FAQ2ForMainCategory4,
            FAQ3ForMainCategory4,
            FAQ1ForMainCategory5,
            FAQ2ForMainCategory5,
            FAQ3ForMainCategory5,
            FAQ1ForMainCategory6,
            FAQ2ForMainCategory6,
            FAQ3ForMainCategory6,
            FAQ1ForMainCategory7,
            FAQ2ForMainCategory7,
            FAQ3ForMainCategory7,
            FAQ1ForMainCategory8,
            FAQ2ForMainCategory8,
            FAQ3ForMainCategory8,
            FAQ1ForMainCategory9,
            FAQ2ForMainCategory9,
            FAQ3ForMainCategory9,
            FAQ1ForMainCategory10,
            FAQ2ForMainCategory10,
            FAQ3ForMainCategory10
        };

            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedSubCategories()
    {
        if(!_context.SubCategories.Any())
        {
            SubCategory1ForMainCategory1 = new SubCategory
            {
                Name = "Logo & Brand Identity",
                MainCategoryId = MainCategory1.Id,
                ImageUrl = "https://fiverr-res.cloudinary.com/image/upload/f_auto,q_auto/v1/attachments/generic_asset/asset/68011f21cd41c664951df861d9f876ac-1682402649968/Logo%20_%20Brand%20Identity.png",
                Clicks = 24,
                CreatedOn = DateTime.UtcNow.AddDays(-3)
            };

            SubCategory2ForMainCategory1 = new SubCategory
            {
                Name = "Web & App Design",
                MainCategoryId = MainCategory1.Id,
                ImageUrl = "https://fiverr-res.cloudinary.com/image/upload/f_auto,q_auto/v1/attachments/generic_asset/asset/68011f21cd41c664951df861d9f876ac-1682402649980/Web%20_%20App%20Design.png",
                Clicks = 30,
                CreatedOn = DateTime.UtcNow.AddDays(-2)
            };

            SubCategory3ForMainCategory1 = new SubCategory
            {
                Name = "Art & Illustration",
                MainCategoryId = MainCategory1.Id,
                Clicks = 22,
                ImageUrl= "https://fiverr-res.cloudinary.com/image/upload/f_auto,q_auto/v1/attachments/generic_asset/asset/68011f21cd41c664951df861d9f876ac-1682402649988/Art%20_%20Illustration.png",
                CreatedOn = DateTime.UtcNow.AddDays(-1)
            };

            SubCategory4ForMainCategory1 = new SubCategory
            {
                Name = "Architecture & Building Design",
                MainCategoryId = MainCategory1.Id,
                ImageUrl = "https://fiverr-res.cloudinary.com/image/upload/f_auto,q_auto/v1/attachments/generic_asset/asset/68011f21cd41c664951df861d9f876ac-1682402649984/Architecture%20_%20Building%E2%80%A8Design.png",
                Clicks = 14,
                CreatedOn = DateTime.UtcNow.AddDays(-4)
            };

            SubCategory5ForMainCategory1 = new SubCategory
            {
                Name = "Product & Gaming",
                MainCategoryId = MainCategory1.Id,
                ImageUrl = "https://fiverr-res.cloudinary.com/image/upload/f_auto,q_auto/v1/attachments/generic_asset/asset/68011f21cd41c664951df861d9f876ac-1682402649966/Product%20_%20Gaming.png",
                Clicks = 18,
                CreatedOn = DateTime.UtcNow.AddDays(-5)
            };

            SubCategory6ForMainCategory1 = new SubCategory
            {
                Name = "Visual Design",
                ImageUrl = "https://fiverr-res.cloudinary.com/image/upload/f_auto,q_auto/v1/attachments/generic_asset/asset/68011f21cd41c664951df861d9f876ac-1682402649982/Visual%20Design.png",
                MainCategoryId = MainCategory1.Id,
                Clicks = 16,
                CreatedOn = DateTime.UtcNow.AddDays(-6)
            };

            SubCategory7ForMainCategory1 = new SubCategory
            {
                Name = "Print Design",
                ImageUrl = "https://fiverr-res.cloudinary.com/image/upload/f_auto,q_auto/v1/attachments/generic_asset/asset/68011f21cd41c664951df861d9f876ac-1682402649969/Print%20Design.png",
                MainCategoryId = MainCategory1.Id,
                Clicks = 13,
                CreatedOn = DateTime.UtcNow.AddDays(-7)
            };

            SubCategory8ForMainCategory1 = new SubCategory
            {
                Name = "Packaging & Covers",
                ImageUrl = "https://fiverr-res.cloudinary.com/image/upload/f_auto,q_auto/v1/attachments/generic_asset/asset/68011f21cd41c664951df861d9f876ac-1682402649987/Packaging%20_%20Labels.png",
                MainCategoryId = MainCategory1.Id,
                Clicks = 10,
                CreatedOn = DateTime.UtcNow.AddDays(-8)
            };

            SubCategory1ForMainCategory2 = new SubCategory
            {
                Name = "Web Development",
                MainCategoryId = MainCategory2.Id,
                Clicks = 130,
                CreatedOn = DateTime.UtcNow.AddDays(-1)
            };

            SubCategory2ForMainCategory2 = new SubCategory
            {
                Name = "Mobile App Development",
                MainCategoryId = MainCategory2.Id,
                Clicks = 18,
                CreatedOn = DateTime.UtcNow.AddDays(-2)
            };

            SubCategory3ForMainCategory2 = new SubCategory
            {
                Name = "Software Development",
                MainCategoryId = MainCategory2.Id,
                Clicks = 2,
                CreatedOn = DateTime.UtcNow.AddDays(-3)
            };

            SubCategory4ForMainCategory2 = new SubCategory
            {
                Name = "Game Development",
                MainCategoryId = MainCategory2.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-4)
            };


            SubCategory1ForMainCategory3 = new SubCategory
            {
                Name = "Social Media Marketing",
                MainCategoryId = MainCategory3.Id,
                Clicks = 2,
                CreatedOn = DateTime.UtcNow.AddDays(-1)
            };

            SubCategory2ForMainCategory3 = new SubCategory
            {
                Name = "SEO & SEM",
                MainCategoryId = MainCategory3.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-2)
            };

            SubCategory3ForMainCategory3 = new SubCategory
            {
                Name = "Email Marketing",
                MainCategoryId = MainCategory3.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-3)
            };

            SubCategory4ForMainCategory3 = new SubCategory
            {
                Name = "Content Marketing",
                MainCategoryId = MainCategory3.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-4)
            };

            SubCategory1ForMainCategory4 = new SubCategory
            {
                Name = "Explainer Videos",
                MainCategoryId = MainCategory4.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-1)
            };

            SubCategory2ForMainCategory4 = new SubCategory
            {
                Name = "Whiteboard Animation",
                MainCategoryId = MainCategory4.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-2)
            };

            SubCategory3ForMainCategory4 = new SubCategory
            {
                Name = "2D/3D Animation",
                MainCategoryId = MainCategory4.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-3)
            };

            SubCategory4ForMainCategory4 = new SubCategory
            {
                Name = "Video Editing",
                MainCategoryId = MainCategory4.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-4)
            };

            SubCategory1ForMainCategory5 = new SubCategory
            {
                Name = "Copywriting",
                MainCategoryId = MainCategory5.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-1)
            };

            SubCategory2ForMainCategory5 = new SubCategory
            {
                Name = "SEO Writing",
                MainCategoryId = MainCategory5.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-2)
            };

            SubCategory3ForMainCategory5 = new SubCategory
            {
                Name = "Translation Services",
                MainCategoryId = MainCategory5.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-3)
            };

            SubCategory4ForMainCategory5 = new SubCategory
            {
                Name = "Creative Writing",
                MainCategoryId = MainCategory5.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-4)
            };

            SubCategory1ForMainCategory6 = new SubCategory
            {
                Name = "Voice Over",
                MainCategoryId = MainCategory6.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-1)
            };

            SubCategory2ForMainCategory6 = new SubCategory
            {
                Name = "Music Production",
                MainCategoryId = MainCategory6.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-2)
            };

            SubCategory3ForMainCategory6 = new SubCategory
            {
                Name = "Podcast Editing",
                MainCategoryId = MainCategory6.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-3)
            };

            SubCategory4ForMainCategory6 = new SubCategory
            {
                Name = "Sound Design",
                MainCategoryId = MainCategory6.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-4)
            };

            SubCategory1ForMainCategory7 = new SubCategory
            {
                Name = "Business Consulting",
                MainCategoryId = MainCategory7.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-1)
            };

            SubCategory2ForMainCategory7 = new SubCategory
            {
                Name = "Virtual Assistant",
                MainCategoryId = MainCategory7.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-2)
            };

            SubCategory3ForMainCategory7 = new SubCategory
            {
                Name = "Market Research",
                MainCategoryId = MainCategory7.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-3)
            };

            SubCategory4ForMainCategory7 = new SubCategory
            {
                Name = "Project Management",
                MainCategoryId = MainCategory7.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-4)
            };

            SubCategory1ForMainCategory8 = new SubCategory
            {
                Name = "Financial Consulting",
                MainCategoryId = MainCategory8.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-1)
            };

            SubCategory2ForMainCategory8 = new SubCategory
            {
                Name = "Accounting Services",
                MainCategoryId = MainCategory8.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-2)
            };

            SubCategory3ForMainCategory8 = new SubCategory
            {
                Name = "Tax Preparation",
                MainCategoryId = MainCategory8.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-3)
            };

            SubCategory4ForMainCategory8 = new SubCategory
            {
                Name = "Investment Advice",
                MainCategoryId = MainCategory8.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-4)
            };

            SubCategory1ForMainCategory9 = new SubCategory
            {
                Name = "Machine Learning",
                MainCategoryId = MainCategory9.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-1)
            };

            SubCategory2ForMainCategory9 = new SubCategory
            {
                Name = "AI Chatbots",
                MainCategoryId = MainCategory9.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-2)
            };

            SubCategory3ForMainCategory9 = new SubCategory
            {
                Name = "AI-Powered Automation",
                MainCategoryId = MainCategory9.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-3)
            };

            SubCategory4ForMainCategory9 = new SubCategory
            {
                Name = "Data Analysis & Predictive Modeling",
                MainCategoryId = MainCategory9.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-4)
            };

            SubCategory1ForMainCategory10 = new SubCategory
            {
                Name = "Life Coaching",
                MainCategoryId = MainCategory10.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-1)
            };

            SubCategory2ForMainCategory10 = new SubCategory
            {
                Name = "Mindfulness & Meditation",
                MainCategoryId = MainCategory10.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-2)
            };

            SubCategory3ForMainCategory10 = new SubCategory
            {
                Name = "Motivational Speaking",
                MainCategoryId = MainCategory10.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-3)
            };

            SubCategory4ForMainCategory10 = new SubCategory
            {
                Name = "Wellness Coaching",
                MainCategoryId = MainCategory10.Id,
                Clicks = 1,
                CreatedOn = DateTime.UtcNow.AddDays(-4)
            };

            var list = new List<SubCategory>
            {
                SubCategory1ForMainCategory1,
                SubCategory2ForMainCategory1,
                SubCategory3ForMainCategory1,
                SubCategory4ForMainCategory1,
                SubCategory5ForMainCategory1,
                SubCategory6ForMainCategory1,
                SubCategory7ForMainCategory1,
                SubCategory8ForMainCategory1,
                SubCategory1ForMainCategory2,
                SubCategory2ForMainCategory2,
                SubCategory3ForMainCategory2,
                SubCategory4ForMainCategory2,
                SubCategory1ForMainCategory3,
                SubCategory2ForMainCategory3,
                SubCategory3ForMainCategory3,
                SubCategory4ForMainCategory3,
                SubCategory1ForMainCategory4,
                SubCategory2ForMainCategory4,
                SubCategory3ForMainCategory4,
                SubCategory4ForMainCategory4,
                SubCategory1ForMainCategory5,
                SubCategory2ForMainCategory5,
                SubCategory3ForMainCategory5,
                SubCategory4ForMainCategory5,
                SubCategory1ForMainCategory6,
                SubCategory2ForMainCategory6,
                SubCategory3ForMainCategory6,
                SubCategory4ForMainCategory6,
                SubCategory1ForMainCategory7,
                SubCategory2ForMainCategory7,
                SubCategory3ForMainCategory7,
                SubCategory4ForMainCategory7,
                SubCategory1ForMainCategory8,
                SubCategory2ForMainCategory8,
                SubCategory3ForMainCategory8,
                SubCategory4ForMainCategory8,
                SubCategory1ForMainCategory9,
                SubCategory2ForMainCategory9,
                SubCategory3ForMainCategory9,
                SubCategory4ForMainCategory9,
                SubCategory1ForMainCategory10,
                SubCategory2ForMainCategory10,
                SubCategory3ForMainCategory10,
                SubCategory4ForMainCategory10
            };
            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedSubSubCategories()
    {
        if(!_context.SubSubCategories.Any())
        {
            SubSubCategory1ForSubCategory1ForMainCategory1 = new SubSubCategory
            {
                Name = "Logo Design",
                SubCategoryId = SubCategory1ForMainCategory1.Id,
                Clicks = 3,
                CreatedOn = DateTime.Now.AddDays(-200)
            };

            SubSubCategory2ForSubCategory1ForMainCategory1 = new SubSubCategory
            {
                Name = "Brand Style Guides",
                SubCategoryId = SubCategory1ForMainCategory1.Id,
                Clicks = 34,
                CreatedOn = DateTime.Now.AddDays(-198)
            };

            SubSubCategory3ForSubCategory1ForMainCategory1 = new SubSubCategory
            {
                Name = "Business Cards & Stationery",
                SubCategoryId = SubCategory1ForMainCategory1.Id,
                Clicks = 33,
                CreatedOn = DateTime.Now.AddDays(-196)
            };

            SubSubCategory4ForSubCategory1ForMainCategory1 = new SubSubCategory
            {
                Name = "Fonts & Typography",
                SubCategoryId = SubCategory1ForMainCategory1.Id,
                Clicks = 32,
                CreatedOn = DateTime.Now.AddDays(-194)
            };

            SubSubCategory1ForSubCategory2ForMainCategory1 = new SubSubCategory
            {
                Name = "Website Design",
                SubCategoryId = SubCategory2ForMainCategory1.Id,
                Clicks = 20,
                CreatedOn = DateTime.Now.AddDays(-190)
            };

            SubSubCategory2ForSubCategory2ForMainCategory1 = new SubSubCategory
            {
                Name = "App Design",
                SubCategoryId = SubCategory2ForMainCategory1.Id,
                Clicks = 30,
                CreatedOn = DateTime.Now.AddDays(-188)
            };

            SubSubCategory3ForSubCategory2ForMainCategory1 = new SubSubCategory
            {
                Name = "UX Design",
                SubCategoryId = SubCategory2ForMainCategory1.Id,
                Clicks = 40,
                CreatedOn = DateTime.Now.AddDays(-186)
            };

            SubSubCategory4ForSubCategory2ForMainCategory1 = new SubSubCategory
            {
                Name = "Landing Page Design",
                SubCategoryId = SubCategory2ForMainCategory1.Id,
                Clicks = 20,
                CreatedOn = DateTime.Now.AddDays(-184)
            };

            SubSubCategory5ForSubCategory2ForMainCategory1 = new SubSubCategory
            {
                Name = "Icon Design",
                SubCategoryId = SubCategory2ForMainCategory1.Id,
                Clicks = 27,
                CreatedOn = DateTime.Now.AddDays(-182)
            };

            SubSubCategory1ForSubCategory3ForMainCategory1 = new SubSubCategory
            {
                Name = "Illustration",
                SubCategoryId = SubCategory3ForMainCategory1.Id,
                Clicks = 26,
                CreatedOn = DateTime.Now.AddDays(-180)
            };

            SubSubCategory2ForSubCategory3ForMainCategory1 = new SubSubCategory
            {
                Name = "AI Artists",
                SubCategoryId = SubCategory3ForMainCategory1.Id,
                Clicks = 25,
                CreatedOn = DateTime.Now.AddDays(-178)
            };

            SubSubCategory3ForSubCategory3ForMainCategory1 = new SubSubCategory
            {
                Name = "AI Avatar Design",
                SubCategoryId = SubCategory3ForMainCategory1.Id,
                Clicks = 24,
                CreatedOn = DateTime.Now.AddDays(-176)
            };

            SubSubCategory4ForSubCategory3ForMainCategory1 = new SubSubCategory
            {
                Name = "Children's Book Illustration",
                SubCategoryId = SubCategory3ForMainCategory1.Id,
                Clicks = 23,
                CreatedOn = DateTime.Now.AddDays(-174)
            };

            SubSubCategory5ForSubCategory3ForMainCategory1 = new SubSubCategory
            {
                Name = "Portraits & Caricatures",
                SubCategoryId = SubCategory3ForMainCategory1.Id,
                Clicks = 22,
                CreatedOn = DateTime.Now.AddDays(-172)
            };

            SubSubCategory6ForSubCategory3ForMainCategory1 = new SubSubCategory
            {
                Name = "Cartoons & Comics",
                SubCategoryId = SubCategory3ForMainCategory1.Id,
                Clicks = 21,
                CreatedOn = DateTime.Now.AddDays(-170)
            };

            SubSubCategory7ForSubCategory3ForMainCategory1 = new SubSubCategory
            {
                Name = "Pattern Design",
                SubCategoryId = SubCategory3ForMainCategory1.Id,
                Clicks = 20,
                CreatedOn = DateTime.Now.AddDays(-168)
            };

            SubSubCategory8ForSubCategory3ForMainCategory1 = new SubSubCategory
            {
                Name = "Tattoo Design",
                SubCategoryId = SubCategory3ForMainCategory1.Id,
                Clicks = 19,
                CreatedOn = DateTime.Now.AddDays(-166)
            };

            SubSubCategory9ForSubCategory3ForMainCategory1 = new SubSubCategory
            {
                Name = "Storyboards",
                SubCategoryId = SubCategory3ForMainCategory1.Id,
                Clicks = 18,
                CreatedOn = DateTime.Now.AddDays(-164)
            };

            SubSubCategory10ForSubCategory3ForMainCategory1 = new SubSubCategory
            {
                Name = "NFT Art",
                SubCategoryId = SubCategory3ForMainCategory1.Id,
                Clicks = 17,
                CreatedOn = DateTime.Now.AddDays(-162)
            };

            // SubCategory4: Architecture & Building Design
            SubSubCategory1ForSubCategory4ForMainCategory1 = new SubSubCategory
            {
                Name = "Architecture & Interior Design",
                SubCategoryId = SubCategory4ForMainCategory1.Id,
                Clicks = 16,
                CreatedOn = DateTime.Now.AddDays(-160)
            };

            SubSubCategory2ForSubCategory4ForMainCategory1 = new SubSubCategory
            {
                Name = "Landscape Design",
                SubCategoryId = SubCategory4ForMainCategory1.Id,
                Clicks = 15,
                CreatedOn = DateTime.Now.AddDays(-158)
            };

            SubSubCategory3ForSubCategory4ForMainCategory1 = new SubSubCategory
            {
                Name = "Building Engineering",
                SubCategoryId = SubCategory4ForMainCategory1.Id,
                Clicks = 14,
                CreatedOn = DateTime.Now.AddDays(-156)
            };

            SubSubCategory4ForSubCategory4ForMainCategory1 = new SubSubCategory
            {
                Name = "Lighting Design",
                SubCategoryId = SubCategory4ForMainCategory1.Id,
                Clicks = 13,
                CreatedOn = DateTime.Now.AddDays(-154)
            };

            SubSubCategory5ForSubCategory4ForMainCategory1 = new SubSubCategory
            {
                Name = "Building Information Modeling",
                SubCategoryId = SubCategory4ForMainCategory1.Id,
                Clicks = 12,
                CreatedOn = DateTime.Now.AddDays(-152)
            };

            SubSubCategory1ForSubCategory5ForMainCategory1 = new SubSubCategory
            {
                Name = "Industrial & Product Design",
                SubCategoryId = SubCategory5ForMainCategory1.Id,
                Clicks = 11,
                CreatedOn = DateTime.Now.AddDays(-150)
            };

            SubSubCategory2ForSubCategory5ForMainCategory1 = new SubSubCategory
            {
                Name = "Character Modeling",
                SubCategoryId = SubCategory5ForMainCategory1.Id,
                Clicks = 10,
                CreatedOn = DateTime.Now.AddDays(-148)
            };

            SubSubCategory3ForSubCategory5ForMainCategory1 = new SubSubCategory
            {
                Name = "Game Art",
                SubCategoryId = SubCategory5ForMainCategory1.Id,
                Clicks = 9,
                CreatedOn = DateTime.Now.AddDays(-146)
            };

            SubSubCategory4ForSubCategory5ForMainCategory1 = new SubSubCategory
            {
                Name = "Graphics for Streamers",
                SubCategoryId = SubCategory5ForMainCategory1.Id,
                Clicks = 8,
                CreatedOn = DateTime.Now.AddDays(-144)
            };

            SubSubCategory5ForSubCategory5ForMainCategory1 = new SubSubCategory
            {
                Name = "Twitch Store",
                SubCategoryId = SubCategory5ForMainCategory1.Id,
                Clicks = 7,
                CreatedOn = DateTime.Now.AddDays(-142)
            };

            SubSubCategory6ForSubCategory5ForMainCategory1 = new SubSubCategory
            {
                Name = "Trade Booth Design",
                SubCategoryId = SubCategory5ForMainCategory1.Id,
                Clicks = 6,
                CreatedOn = DateTime.Now.AddDays(-140)
            };

            SubSubCategory1ForSubCategory6ForMainCategory1 = new SubSubCategory
            {
                Name = "Image Editing",
                SubCategoryId = SubCategory6ForMainCategory1.Id,
                Clicks = 5,
                CreatedOn = DateTime.Now.AddDays(-138)
            };

            SubSubCategory2ForSubCategory6ForMainCategory1 = new SubSubCategory
            {
                Name = "AI Image Editing",
                SubCategoryId = SubCategory6ForMainCategory1.Id,
Clicks = 4,
CreatedOn = DateTime.Now.AddDays(-136)
};

        SubSubCategory3ForSubCategory6ForMainCategory1 = new SubSubCategory
        {
            Name = "Presentation Design",
            SubCategoryId = SubCategory6ForMainCategory1.Id,
            Clicks = 4,
            CreatedOn = DateTime.Now.AddDays(-134)
        };

        SubSubCategory4ForSubCategory6ForMainCategory1 = new SubSubCategory
        {
            Name = "Infographic Design",
            SubCategoryId = SubCategory6ForMainCategory1.Id,
            Clicks = 3,
            CreatedOn = DateTime.Now.AddDays(-132)
        };

        SubSubCategory5ForSubCategory6ForMainCategory1 = new SubSubCategory
        {
            Name = "Vector Tracing",
            SubCategoryId = SubCategory6ForMainCategory1.Id,
            Clicks = 3,
            CreatedOn = DateTime.Now.AddDays(-130)
        };

        SubSubCategory6ForSubCategory6ForMainCategory1 = new SubSubCategory
        {
            Name = "Resume Design",
            SubCategoryId = SubCategory6ForMainCategory1.Id,
            Clicks = 2,
            CreatedOn = DateTime.Now.AddDays(-128)
        };

        // SubCategory7: Print Design
        SubSubCategory1ForSubCategory7ForMainCategory1 = new SubSubCategory
        {
            Name = "Flyer Design",
            SubCategoryId = SubCategory7ForMainCategory1.Id,
            Clicks = 2,
            CreatedOn = DateTime.Now.AddDays(-126)
        };

        SubSubCategory2ForSubCategory7ForMainCategory1 = new SubSubCategory
        {
            Name = "Brochure Design",
            SubCategoryId = SubCategory7ForMainCategory1.Id,
            Clicks = 1,
            CreatedOn = DateTime.Now.AddDays(-124)
        };

        SubSubCategory3ForSubCategory7ForMainCategory1 = new SubSubCategory
        {
            Name = "Poster Design",
            SubCategoryId = SubCategory7ForMainCategory1.Id,
            Clicks = 1,
            CreatedOn = DateTime.Now.AddDays(-122)
        };

        SubSubCategory4ForSubCategory7ForMainCategory1 = new SubSubCategory
        {
            Name = "Catalog Design",
            SubCategoryId = SubCategory7ForMainCategory1.Id,
            Clicks = 1,
            CreatedOn = DateTime.Now.AddDays(-120)
        };

        SubSubCategory5ForSubCategory7ForMainCategory1 = new SubSubCategory
        {
            Name = "Menu Design",
            SubCategoryId = SubCategory7ForMainCategory1.Id,
            Clicks = 1,
            CreatedOn = DateTime.Now.AddDays(-118)
        };

        SubSubCategory6ForSubCategory7ForMainCategory1 = new SubSubCategory
        {
            Name = "Invitation Design",
            SubCategoryId = SubCategory7ForMainCategory1.Id,
            Clicks = 1,
            CreatedOn = DateTime.Now.AddDays(-116)
        };

        // SubCategory8: Packaging & Covers
        SubSubCategory1ForSubCategory8ForMainCategory1 = new SubSubCategory
        {
            Name = "Packaging & Label Design",
            SubCategoryId = SubCategory8ForMainCategory1.Id,
            Clicks = 9,
            CreatedOn = DateTime.Now.AddDays(-114)
        };

        SubSubCategory2ForSubCategory8ForMainCategory1 = new SubSubCategory
        {
            Name = "Book Design",
            SubCategoryId = SubCategory8ForMainCategory1.Id,
            Clicks = 8,
            CreatedOn = DateTime.Now.AddDays(-112)
        };

        SubSubCategory3ForSubCategory8ForMainCategory1 = new SubSubCategory
        {
            Name = "Book Covers",
            SubCategoryId = SubCategory8ForMainCategory1.Id,
            Clicks = 7,
            CreatedOn = DateTime.Now.AddDays(-110)
        };

        SubSubCategory4ForSubCategory8ForMainCategory1 = new SubSubCategory
        {
            Name = "Album Cover Design",
            SubCategoryId = SubCategory8ForMainCategory1.Id,
            Clicks = 6,
            CreatedOn = DateTime.Now.AddDays(-108)
        };

        SubSubCategory5ForSubCategory8ForMainCategory1 = new SubSubCategory
        {
            Name = "Podcast Cover Art",
            SubCategoryId = SubCategory8ForMainCategory1.Id,
            Clicks = 5,
            CreatedOn = DateTime.Now.AddDays(-106)
        };

        SubSubCategory6ForSubCategory8ForMainCategory1 = new SubSubCategory
        {
            Name = "Car Wraps",
            SubCategoryId = SubCategory8ForMainCategory1.Id,
            Clicks = 4,
            CreatedOn = DateTime.Now.AddDays(-104)
        };
        SubSubCategory1ForSubCategory1ForMainCategory2 = new SubSubCategory
            {
                Name = "Frontend Development",
                SubCategoryId = SubCategory1ForMainCategory2.Id,
                Clicks = 450,
                CreatedOn = DateTime.Now.AddDays(-180)
            };

            SubSubCategory2ForSubCategory1ForMainCategory2 = new SubSubCategory
            {
                Name = "Backend Development",
                SubCategoryId = SubCategory1ForMainCategory2.Id,
                Clicks = 440,
                CreatedOn = DateTime.Now.AddDays(-175)
            };

            SubSubCategory1ForSubCategory2ForMainCategory2 = new SubSubCategory
            {
                Name = "iOS Development",
                SubCategoryId = SubCategory2ForMainCategory2.Id,
                Clicks = 430,
                CreatedOn = DateTime.Now.AddDays(-170)
            };

            SubSubCategory2ForSubCategory2ForMainCategory2 = new SubSubCategory
            {
                Name = "Android Development",
                SubCategoryId = SubCategory2ForMainCategory2.Id,
                Clicks = 420,
                CreatedOn = DateTime.Now.AddDays(-165)
            };

            SubSubCategory1ForSubCategory3ForMainCategory2 = new SubSubCategory
            {
                Name = "Desktop Applications",
                SubCategoryId = SubCategory3ForMainCategory2.Id,
                Clicks = 410,
                CreatedOn = DateTime.Now.AddDays(-160)
            };

            SubSubCategory2ForSubCategory3ForMainCategory2 = new SubSubCategory
            {
                Name = "Cloud Solutions",
                SubCategoryId = SubCategory3ForMainCategory2.Id,
                Clicks = 400,
                CreatedOn = DateTime.Now.AddDays(-155)
            };

            SubSubCategory1ForSubCategory4ForMainCategory2 = new SubSubCategory
            {
                Name = "Unity Development",
                SubCategoryId = SubCategory4ForMainCategory2.Id,
                Clicks = 390,
                CreatedOn = DateTime.Now.AddDays(-150)
            };

            SubSubCategory2ForSubCategory4ForMainCategory2 = new SubSubCategory
            {
                Name = "Unreal Engine",
                SubCategoryId = SubCategory4ForMainCategory2.Id,
                Clicks = 380,
                CreatedOn = DateTime.Now.AddDays(-145)
            };

            SubSubCategory1ForSubCategory1ForMainCategory3 = new SubSubCategory
            {
                Name = "Facebook Campaigns",
                SubCategoryId = SubCategory1ForMainCategory3.Id,
                Clicks = 420,
                CreatedOn = DateTime.Now.AddDays(-160)
            };

            SubSubCategory2ForSubCategory1ForMainCategory3 = new SubSubCategory
            {
                Name = "Instagram Growth",
                SubCategoryId = SubCategory1ForMainCategory3.Id,
                Clicks = 410,
                CreatedOn = DateTime.Now.AddDays(-155)
            };

            SubSubCategory1ForSubCategory2ForMainCategory3 = new SubSubCategory
            {
                Name = "On-Page SEO",
                SubCategoryId = SubCategory2ForMainCategory3.Id,
                Clicks = 395,
                CreatedOn = DateTime.Now.AddDays(-150)
            };

            SubSubCategory2ForSubCategory2ForMainCategory3 = new SubSubCategory
            {
                Name = "Google Ads Management",
                SubCategoryId = SubCategory2ForMainCategory3.Id,
                Clicks = 385,
                CreatedOn = DateTime.Now.AddDays(-145)
            };

            SubSubCategory1ForSubCategory3ForMainCategory3 = new SubSubCategory
            {
                Name = "Newsletter Campaigns",
                SubCategoryId = SubCategory3ForMainCategory3.Id,
                Clicks = 370,
                CreatedOn = DateTime.Now.AddDays(-140)
            };

            SubSubCategory2ForSubCategory3ForMainCategory3 = new SubSubCategory
            {
                Name = "Automated Emails",
                SubCategoryId = SubCategory3ForMainCategory3.Id,
                Clicks = 360,
                CreatedOn = DateTime.Now.AddDays(-135)
            };

            SubSubCategory1ForSubCategory4ForMainCategory3 = new SubSubCategory
            {
                Name = "Blog Writing",
                SubCategoryId = SubCategory4ForMainCategory3.Id,
                Clicks = 345,
                CreatedOn = DateTime.Now.AddDays(-130)
            };

            SubSubCategory2ForSubCategory4ForMainCategory3 = new SubSubCategory
            {
                Name = "Video Content Creation",
                SubCategoryId = SubCategory4ForMainCategory3.Id,
                Clicks = 335,
                CreatedOn = DateTime.Now.AddDays(-125)
            };

            SubSubCategory1ForSubCategory1ForMainCategory4 = new SubSubCategory
            {
                Name = "Product Explainers",
                SubCategoryId = SubCategory1ForMainCategory4.Id,
                Clicks = 410,
                CreatedOn = DateTime.Now.AddDays(-140)
            };

            SubSubCategory2ForSubCategory1ForMainCategory4 = new SubSubCategory
            {
                Name = "Service Explainers",
                SubCategoryId = SubCategory1ForMainCategory4.Id,
                Clicks = 395,
                CreatedOn = DateTime.Now.AddDays(-135)
            };

            SubSubCategory1ForSubCategory2ForMainCategory4 = new SubSubCategory
            {
                Name = "Educational Whiteboard",
                SubCategoryId = SubCategory2ForMainCategory4.Id,
                Clicks = 380,
                CreatedOn = DateTime.Now.AddDays(-130)
            };

            SubSubCategory2ForSubCategory2ForMainCategory4 = new SubSubCategory
            {
                Name = "Marketing Whiteboard",
                SubCategoryId = SubCategory2ForMainCategory4.Id,
                Clicks = 365,
                CreatedOn = DateTime.Now.AddDays(-125)
            };

            SubSubCategory1ForSubCategory3ForMainCategory4 = new SubSubCategory
            {
                Name = "2D Character Animation",
                SubCategoryId = SubCategory3ForMainCategory4.Id,
                Clicks = 350,
                CreatedOn = DateTime.Now.AddDays(-120)
            };

            SubSubCategory2ForSubCategory3ForMainCategory4 = new SubSubCategory
            {
                Name = "3D Modeling",
                SubCategoryId = SubCategory3ForMainCategory4.Id,
                Clicks = 335,
                CreatedOn = DateTime.Now.AddDays(-115)
            };

            SubSubCategory1ForSubCategory4ForMainCategory4 = new SubSubCategory
            {
                Name = "YouTube Video Editing",
                SubCategoryId = SubCategory4ForMainCategory4.Id,
                Clicks = 320,
                CreatedOn = DateTime.Now.AddDays(-110)
            };

            SubSubCategory2ForSubCategory4ForMainCategory4 = new SubSubCategory
            {
                Name = "Corporate Video Editing",
                SubCategoryId = SubCategory4ForMainCategory4.Id,
                Clicks = 305,
                CreatedOn = DateTime.Now.AddDays(-105)
            };

            SubSubCategory1ForSubCategory1ForMainCategory5 = new SubSubCategory
            {
                Name = "Sales Copywriting",
                SubCategoryId = SubCategory1ForMainCategory5.Id,
                Clicks = 340,
                CreatedOn = DateTime.Now.AddDays(-130)
            };

            SubSubCategory2ForSubCategory1ForMainCategory5 = new SubSubCategory
            {
                Name = "Email Campaigns",
                SubCategoryId = SubCategory1ForMainCategory5.Id,
                Clicks = 325,
                CreatedOn = DateTime.Now.AddDays(-125)
            };

            SubSubCategory1ForSubCategory2ForMainCategory5 = new SubSubCategory
            {
                Name = "Blog Articles",
                SubCategoryId = SubCategory2ForMainCategory5.Id,
                Clicks = 350,
                CreatedOn = DateTime.Now.AddDays(-120)
            };

            SubSubCategory2ForSubCategory2ForMainCategory5 = new SubSubCategory
            {
                Name = "Website Content",
                SubCategoryId = SubCategory2ForMainCategory5.Id,
                Clicks = 335,
                CreatedOn = DateTime.Now.AddDays(-115)
            };

            SubSubCategory1ForSubCategory3ForMainCategory5 = new SubSubCategory
            {
                Name = "Spanish to English",
                SubCategoryId = SubCategory3ForMainCategory5.Id,
                Clicks = 310,
                CreatedOn = DateTime.Now.AddDays(-110)
            };

            SubSubCategory2ForSubCategory3ForMainCategory5 = new SubSubCategory
            {
                Name = "French to English",
                SubCategoryId = SubCategory3ForMainCategory5.Id,
                Clicks = 295,
                CreatedOn = DateTime.Now.AddDays(-105)
            };

            SubSubCategory1ForSubCategory4ForMainCategory5 = new SubSubCategory
            {
                Name = "Short Stories",
                SubCategoryId = SubCategory4ForMainCategory5.Id,
                Clicks = 280,
                CreatedOn = DateTime.Now.AddDays(-100)
            };

            SubSubCategory2ForSubCategory4ForMainCategory5 = new SubSubCategory
            {
                Name = "Screenplays",
                SubCategoryId = SubCategory4ForMainCategory5.Id,
                Clicks = 265,
                CreatedOn = DateTime.Now.AddDays(-95)
            };

            SubSubCategory1ForSubCategory1ForMainCategory6 = new SubSubCategory
            {
                Name = "Commercial Voice Overs",
                SubCategoryId = SubCategory1ForMainCategory6.Id,
                Clicks = 310,
                CreatedOn = DateTime.Now.AddDays(-110)
            };

            SubSubCategory2ForSubCategory1ForMainCategory6 = new SubSubCategory
            {
                Name = "Narration & Audiobooks",
                SubCategoryId = SubCategory1ForMainCategory6.Id,
                Clicks = 295,
                CreatedOn = DateTime.Now.AddDays(-105)
            };

            SubSubCategory1ForSubCategory2ForMainCategory6 = new SubSubCategory
            {
                Name = "Song Mixing",
                SubCategoryId = SubCategory2ForMainCategory6.Id,
                Clicks = 330,
                CreatedOn = DateTime.Now.AddDays(-100)
            };

            SubSubCategory2ForSubCategory2ForMainCategory6 = new SubSubCategory
            {
                Name = "Beats & Instrumentals",
                SubCategoryId = SubCategory2ForMainCategory6.Id,
                Clicks = 320,
                CreatedOn = DateTime.Now.AddDays(-95)
            };

            SubSubCategory1ForSubCategory3ForMainCategory6 = new SubSubCategory
            {
                Name = "Intro/Outro Creation",
                SubCategoryId = SubCategory3ForMainCategory6.Id,
                Clicks = 280,
                CreatedOn = DateTime.Now.AddDays(-90)
            };

            SubSubCategory2ForSubCategory3ForMainCategory6 = new SubSubCategory
            {
                Name = "Noise Reduction",
                SubCategoryId = SubCategory3ForMainCategory6.Id,
                Clicks = 270,
                CreatedOn = DateTime.Now.AddDays(-85)
            };

            SubSubCategory1ForSubCategory4ForMainCategory6 = new SubSubCategory
            {
                Name = "Game Sound Effects",
                SubCategoryId = SubCategory4ForMainCategory6.Id,
                Clicks = 290,
                CreatedOn = DateTime.Now.AddDays(-80)
            };

            SubSubCategory2ForSubCategory4ForMainCategory6 = new SubSubCategory
            {
                Name = "Foley Art",
                SubCategoryId = SubCategory4ForMainCategory6.Id,
                Clicks = 275,
                CreatedOn = DateTime.Now.AddDays(-75)
            };

            SubSubCategory1ForSubCategory1ForMainCategory7 = new SubSubCategory
            {
                Name = "Startup Strategy",
                SubCategoryId = SubCategory1ForMainCategory7.Id,
                Clicks = 370,
                CreatedOn = DateTime.Now.AddDays(-150)
            };

            SubSubCategory2ForSubCategory1ForMainCategory7 = new SubSubCategory
            {
                Name = "Operational Efficiency",
                SubCategoryId = SubCategory1ForMainCategory7.Id,
                Clicks = 355,
                CreatedOn = DateTime.Now.AddDays(-145)
            };

            SubSubCategory1ForSubCategory2ForMainCategory7 = new SubSubCategory
            {
                Name = "Email Management",
                SubCategoryId = SubCategory2ForMainCategory7.Id,
                Clicks = 380,
                CreatedOn = DateTime.Now.AddDays(-140)
            };

            SubSubCategory1ForSubCategory3ForMainCategory7 = new SubSubCategory
            {
                Name = "Competitor Analysis",
                SubCategoryId = SubCategory3ForMainCategory7.Id,
                Clicks = 395,
                CreatedOn = DateTime.Now.AddDays(-130)
            };

            SubSubCategory2ForSubCategory3ForMainCategory7 = new SubSubCategory
            {
                Name = "Industry Trends",
                SubCategoryId = SubCategory3ForMainCategory7.Id,
                Clicks = 375,
                CreatedOn = DateTime.Now.AddDays(-125)
            };

            SubSubCategory1ForSubCategory4ForMainCategory7 = new SubSubCategory
            {
                Name = "Agile Coaching",
                SubCategoryId = SubCategory4ForMainCategory7.Id,
                Clicks = 390,
                CreatedOn = DateTime.Now.AddDays(-120)
            };

            SubSubCategory1ForSubCategory1ForMainCategory8 = new SubSubCategory
            {
                Name = "Business Budgeting",
                SubCategoryId = SubCategory1ForMainCategory8.Id,
                Clicks = 380,
                CreatedOn = DateTime.Now.AddDays(-160)
            };

            SubSubCategory2ForSubCategory1ForMainCategory8 = new SubSubCategory
            {
                Name = "Debt Management Strategies",
                SubCategoryId = SubCategory1ForMainCategory8.Id,
                Clicks = 360,
                CreatedOn = DateTime.Now.AddDays(-155)
            };

            SubSubCategory1ForSubCategory2ForMainCategory8 = new SubSubCategory
            {
                Name = "Bookkeeping",
                SubCategoryId = SubCategory2ForMainCategory8.Id,
                Clicks = 400,
                CreatedOn = DateTime.Now.AddDays(-145)
            };

            SubSubCategory1ForSubCategory3ForMainCategory8 = new SubSubCategory
            {
                Name = "Individual Tax Returns",
                SubCategoryId = SubCategory3ForMainCategory8.Id,
                Clicks = 420,
                CreatedOn = DateTime.Now.AddDays(-135)
            };

            SubSubCategory2ForSubCategory3ForMainCategory8 = new SubSubCategory
            {
                Name = "Corporate Tax Filing",
                SubCategoryId = SubCategory3ForMainCategory8.Id,
                Clicks = 390,
                CreatedOn = DateTime.Now.AddDays(-130)
            };

            SubSubCategory1ForSubCategory4ForMainCategory8 = new SubSubCategory
            {
                Name = "Portfolio Diversification",
                SubCategoryId = SubCategory4ForMainCategory8.Id,
                Clicks = 410,
                CreatedOn = DateTime.Now.AddDays(-125)
            };

            SubSubCategory1ForSubCategory1ForMainCategory9 = new SubSubCategory
            {
                Name = "Model Training & Evaluation",
                SubCategoryId = SubCategory1ForMainCategory9.Id,
                Clicks = 420,
                CreatedOn = DateTime.Now.AddDays(-150)
            };

            SubSubCategory2ForSubCategory1ForMainCategory9 = new SubSubCategory
            {
                Name = "ML Algorithm Optimization",
                SubCategoryId = SubCategory1ForMainCategory9.Id,
                Clicks = 390,
                CreatedOn = DateTime.Now.AddDays(-140)
            };

            SubSubCategory1ForSubCategory2ForMainCategory9 = new SubSubCategory
            {
                Name = "Customer Support Bots",
                SubCategoryId = SubCategory2ForMainCategory9.Id,
                Clicks = 460,
                CreatedOn = DateTime.Now.AddDays(-130)
            };

            SubSubCategory2ForSubCategory2ForMainCategory9 = new SubSubCategory
            {
                Name = "Sales & Lead Generation Bots",
                SubCategoryId = SubCategory2ForMainCategory9.Id,
                Clicks = 430,
                CreatedOn = DateTime.Now.AddDays(-125)
            };

            SubSubCategory1ForSubCategory3ForMainCategory9 = new SubSubCategory
            {
                Name = "Workflow Automation",
                SubCategoryId = SubCategory3ForMainCategory9.Id,
                Clicks = 410,
                CreatedOn = DateTime.Now.AddDays(-115)
            };

            SubSubCategory2ForSubCategory3ForMainCategory9 = new SubSubCategory
            {
                Name = "Business Process Automation",
                SubCategoryId = SubCategory3ForMainCategory9.Id,
                Clicks = 395,
                CreatedOn = DateTime.Now.AddDays(-100)
            };

            SubSubCategory1ForSubCategory4ForMainCategory9 = new SubSubCategory
            {
                Name = "Forecasting & Trend Analysis",
                SubCategoryId = SubCategory4ForMainCategory9.Id,
                Clicks = 480,
                CreatedOn = DateTime.Now.AddDays(-90)
            };

            SubSubCategory2ForSubCategory4ForMainCategory9 = new SubSubCategory
            {
                Name = "Data Visualization & Insights",
                SubCategoryId = SubCategory4ForMainCategory9.Id,
                Clicks = 455,
                CreatedOn = DateTime.Now.AddDays(-85)
            };

            SubSubCategory1ForSubCategory1ForMainCategory10 = new SubSubCategory
            {
                Name = "Career Coaching",
                SubCategoryId = SubCategory1ForMainCategory10.Id,
                Clicks = 300,
                CreatedOn = DateTime.Now.AddDays(-178)
            };

            SubSubCategory1ForSubCategory2ForMainCategory10 = new SubSubCategory
            {
                Name = "Guided Meditation",
                SubCategoryId = SubCategory2ForMainCategory10.Id,
                Clicks = 400,
                CreatedOn = DateTime.Now.AddDays(-120)
            };

            SubSubCategory1ForSubCategory3ForMainCategory10 = new SubSubCategory
            {
                Name = "Youth Motivation",
                SubCategoryId = SubCategory3ForMainCategory10.Id,
                Clicks = 270,
                CreatedOn = DateTime.Now.AddDays(-95)
            };

            SubSubCategory1ForSubCategory4ForMainCategory10 = new SubSubCategory
            {
                Name = "Fitness Goal Setting",
                SubCategoryId = SubCategory4ForMainCategory10.Id,
                Clicks = 330,
                CreatedOn = DateTime.Now.AddDays(-70)
            };


            var list = new List<SubSubCategory>
{
    SubSubCategory1ForSubCategory1ForMainCategory1,
    SubSubCategory2ForSubCategory1ForMainCategory1,
    SubSubCategory3ForSubCategory1ForMainCategory1,
    SubSubCategory4ForSubCategory1ForMainCategory1,
    SubSubCategory1ForSubCategory2ForMainCategory1,
    SubSubCategory2ForSubCategory2ForMainCategory1,
    SubSubCategory3ForSubCategory2ForMainCategory1,
    SubSubCategory4ForSubCategory2ForMainCategory1,
    SubSubCategory5ForSubCategory2ForMainCategory1,
    SubSubCategory1ForSubCategory3ForMainCategory1,
    SubSubCategory2ForSubCategory3ForMainCategory1,
    SubSubCategory3ForSubCategory3ForMainCategory1,
    SubSubCategory4ForSubCategory3ForMainCategory1,
    SubSubCategory5ForSubCategory3ForMainCategory1,
    SubSubCategory6ForSubCategory3ForMainCategory1,
    SubSubCategory7ForSubCategory3ForMainCategory1,
    SubSubCategory8ForSubCategory3ForMainCategory1,
    SubSubCategory9ForSubCategory3ForMainCategory1,
    SubSubCategory10ForSubCategory3ForMainCategory1,
    SubSubCategory1ForSubCategory4ForMainCategory1,
    SubSubCategory2ForSubCategory4ForMainCategory1,
    SubSubCategory3ForSubCategory4ForMainCategory1,
    SubSubCategory4ForSubCategory4ForMainCategory1,
    SubSubCategory5ForSubCategory4ForMainCategory1,
    SubSubCategory1ForSubCategory5ForMainCategory1,
    SubSubCategory2ForSubCategory5ForMainCategory1,
    SubSubCategory3ForSubCategory5ForMainCategory1,
    SubSubCategory4ForSubCategory5ForMainCategory1,
    SubSubCategory5ForSubCategory5ForMainCategory1,
    SubSubCategory6ForSubCategory5ForMainCategory1,
    SubSubCategory1ForSubCategory6ForMainCategory1,
    SubSubCategory2ForSubCategory6ForMainCategory1,
    SubSubCategory3ForSubCategory6ForMainCategory1,
    SubSubCategory4ForSubCategory6ForMainCategory1,
    SubSubCategory5ForSubCategory6ForMainCategory1,
    SubSubCategory6ForSubCategory6ForMainCategory1,
    SubSubCategory1ForSubCategory7ForMainCategory1,
    SubSubCategory2ForSubCategory7ForMainCategory1,
    SubSubCategory3ForSubCategory7ForMainCategory1,
    SubSubCategory4ForSubCategory7ForMainCategory1,
    SubSubCategory5ForSubCategory7ForMainCategory1,
    SubSubCategory6ForSubCategory7ForMainCategory1,
    SubSubCategory1ForSubCategory8ForMainCategory1,
    SubSubCategory2ForSubCategory8ForMainCategory1,
    SubSubCategory3ForSubCategory8ForMainCategory1,
    SubSubCategory4ForSubCategory8ForMainCategory1,
    SubSubCategory5ForSubCategory8ForMainCategory1,
    SubSubCategory6ForSubCategory8ForMainCategory1,
    SubSubCategory1ForSubCategory1ForMainCategory2,
    SubSubCategory2ForSubCategory1ForMainCategory2,
    SubSubCategory1ForSubCategory2ForMainCategory2,
    SubSubCategory2ForSubCategory2ForMainCategory2,
    SubSubCategory1ForSubCategory3ForMainCategory2,
    SubSubCategory2ForSubCategory3ForMainCategory2,
    SubSubCategory1ForSubCategory4ForMainCategory2,
    SubSubCategory2ForSubCategory4ForMainCategory2,
    SubSubCategory1ForSubCategory1ForMainCategory3,
    SubSubCategory2ForSubCategory1ForMainCategory3,
    SubSubCategory1ForSubCategory2ForMainCategory3,
    SubSubCategory2ForSubCategory2ForMainCategory3,
    SubSubCategory1ForSubCategory3ForMainCategory3,
    SubSubCategory2ForSubCategory3ForMainCategory3,
    SubSubCategory1ForSubCategory4ForMainCategory3,
    SubSubCategory2ForSubCategory4ForMainCategory3,
    SubSubCategory1ForSubCategory1ForMainCategory4,
    SubSubCategory2ForSubCategory1ForMainCategory4,
    SubSubCategory1ForSubCategory2ForMainCategory4,
    SubSubCategory2ForSubCategory2ForMainCategory4,
    SubSubCategory1ForSubCategory3ForMainCategory4,
    SubSubCategory2ForSubCategory3ForMainCategory4,
    SubSubCategory1ForSubCategory4ForMainCategory4,
    SubSubCategory2ForSubCategory4ForMainCategory4,
    SubSubCategory1ForSubCategory1ForMainCategory5,
    SubSubCategory2ForSubCategory1ForMainCategory5,
    SubSubCategory1ForSubCategory2ForMainCategory5,
    SubSubCategory2ForSubCategory2ForMainCategory5,
    SubSubCategory1ForSubCategory3ForMainCategory5,
    SubSubCategory2ForSubCategory3ForMainCategory5,
    SubSubCategory1ForSubCategory4ForMainCategory5,
    SubSubCategory2ForSubCategory4ForMainCategory5,
    SubSubCategory1ForSubCategory1ForMainCategory6,
    SubSubCategory2ForSubCategory1ForMainCategory6,
    SubSubCategory1ForSubCategory2ForMainCategory6,
    SubSubCategory2ForSubCategory2ForMainCategory6,
    SubSubCategory1ForSubCategory3ForMainCategory6,
    SubSubCategory2ForSubCategory3ForMainCategory6,
    SubSubCategory1ForSubCategory4ForMainCategory6,
    SubSubCategory2ForSubCategory4ForMainCategory6,
    SubSubCategory1ForSubCategory1ForMainCategory7,
    SubSubCategory2ForSubCategory1ForMainCategory7,
    SubSubCategory1ForSubCategory2ForMainCategory7,
    SubSubCategory1ForSubCategory3ForMainCategory7,
    SubSubCategory2ForSubCategory3ForMainCategory7,
    SubSubCategory1ForSubCategory4ForMainCategory7,
    SubSubCategory1ForSubCategory1ForMainCategory8,
    SubSubCategory2ForSubCategory1ForMainCategory8,
    SubSubCategory1ForSubCategory2ForMainCategory8,
    SubSubCategory1ForSubCategory3ForMainCategory8,
    SubSubCategory2ForSubCategory3ForMainCategory8,
    SubSubCategory1ForSubCategory4ForMainCategory8,
    SubSubCategory1ForSubCategory1ForMainCategory9,
    SubSubCategory2ForSubCategory1ForMainCategory9,
    SubSubCategory1ForSubCategory2ForMainCategory9,
    SubSubCategory2ForSubCategory2ForMainCategory9,
    SubSubCategory1ForSubCategory3ForMainCategory9,
    SubSubCategory2ForSubCategory3ForMainCategory9,
    SubSubCategory1ForSubCategory4ForMainCategory9,
    SubSubCategory2ForSubCategory4ForMainCategory9,
    SubSubCategory1ForSubCategory1ForMainCategory10,
    SubSubCategory1ForSubCategory2ForMainCategory10,
    SubSubCategory1ForSubCategory3ForMainCategory10,
    SubSubCategory1ForSubCategory4ForMainCategory10,
};




            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }

    }
    private async Task SeedGigFiltersForSubSubCategories()
    {
         if(!_context.GigFilters.Any())
        {
            //MainCategory1: Graphics & Design
            //SuCategory2: Art and illustration
            //SubSubcategory1: "Illustration",
            GigFilter1ForSubSubCategory1ForSubCategory3ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory3ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Style"
            };

            GigFilter2ForSubSubCategory1ForSubCategory3ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory3ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Concept"
            };

            GigFilter3ForSubSubCategory1ForSubCategory3ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory3ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Service includes"
            };
            //SubSubcategory2: AI Artists
            GigFilter1ForSubSubCategory2ForSubCategory3ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory3ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "AI platform"
            };

            GigFilter2ForSubSubCategory2ForSubCategory3ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory3ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Expertise"
            };

            GigFilter3ForSubSubCategory2ForSubCategory3ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory3ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Service includes"
            };
            //SubSubcategory3: AI Avatar Design
            GigFilter1ForSubSubCategory3ForSubCategory3ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory3ForSubCategory3ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Avatar style"
            };

            GigFilter3ForSubSubCategory3ForSubCategory3ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory3ForSubCategory3ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Service includes"
            };
            //SubSubcategory4: Websitedesign
            //SubSubcategory5: Websitedesign
            //SubSubcategory6: Websitedesign
            //SubSubcategory7: Websitedesign
            //SubSubcategory8: Websitedesign
            //SubSubcategory9: Websitedesign
            //SubSubcategory10: Websitedesign

            //MainCategory1: Graphics & Design
            //SuCategory2: Web app design
            //SubSubcategory1: Websitedesign
            GigFilter1ForSubSubCategory1ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Platform"
            };

            GigFilter2ForSubSubCategory1ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Tool"
            };

            GigFilter3ForSubSubCategory1ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Service includes"
            };

            //SubSubcategory2: App Design
            GigFilter1ForSubSubCategory2ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "App type"
            };

            GigFilter2ForSubSubCategory2ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Device"
            };

            GigFilter3ForSubSubCategory2ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Service includes"
            };
            //SubSubCategory 3: UX Design
            GigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "App type"
            };

            GigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Design tool"
            };

            GigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Service includes"
            };
            //SubSubCategory 4: Landing Page Design
            GigFilter1ForSubSubCategory4ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory4ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Platform and tool"
            };

            GigFilter2ForSubSubCategory4ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory4ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Service includes"
            };
            //SubSubCategory 5: Icon Design
            GigFilter1ForSubSubCategory5ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory5ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Style"
            };

            GigFilter2ForSubSubCategory5ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory5ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Purpose"
            };

            GigFilter3ForSubSubCategory5ForSubCategory2ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory5ForSubCategory2ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Service includes"
            };


            //SuCategory2: Web app design
            //SubSubcategory1: Websitedesign
            // SubCategory1: Logo and Brand Identity
            //SubSubCategory1: Logo design

            GigFilter1ForSubSubCategory1ForSubCategory1ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory1ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Logo style"
            };

GigFilter2ForSubSubCategory1ForSubCategory1ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory1ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "File format"
            };

GigFilter3ForSubSubCategory1ForSubCategory1ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory1ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Service includes"
            };

            // SubSubCategory2: Brand Style Guides
            GigFilter1ForSubSubCategory2ForSubCategory1ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory1ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Style"
            };

            GigFilter2ForSubSubCategory2ForSubCategory1ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory1ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Service includes"
            };

            // SubSubCategory3: Business Cards & Stationery
            GigFilter1ForSubSubCategory3ForSubCategory1ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory3ForSubCategory1ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Main type"
            };

            GigFilter2ForSubSubCategory3ForSubCategory1ForMainCategory1 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory3ForSubCategory1ForMainCategory1.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Service includes"
            };

            // MainCategory5: Writing & Translation
            GigFilterForSubSubCategory1ForSubCategory1ForMainCategory5 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory1ForMainCategory5.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Copy Style"
            };

            GigFilterForSubSubCategory2ForSubCategory1ForMainCategory5 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory1ForMainCategory5.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Campaign Type"
            };

            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory5 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory2ForMainCategory5.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Blog Topic"
            };

            GigFilterForSubSubCategory2ForSubCategory2ForMainCategory5 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory2ForMainCategory5.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Website Section"
            };

            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory5 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory3ForMainCategory5.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Translation Direction"
            };

            GigFilterForSubSubCategory2ForSubCategory3ForMainCategory5 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory3ForMainCategory5.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Language Pair"
            };

            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory5 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory4ForMainCategory5.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Genre"
            };

            GigFilterForSubSubCategory2ForSubCategory4ForMainCategory5 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory4ForMainCategory5.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Script Format"
            };

            // MainCategory6: Music & Audio
            GigFilterForSubSubCategory1ForSubCategory1ForMainCategory6 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory1ForMainCategory6.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Voice Style"
            };

            GigFilterForSubSubCategory2ForSubCategory1ForMainCategory6 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory1ForMainCategory6.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Narration Type"
            };

            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory6 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory2ForMainCategory6.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Mixing Level"
            };

            GigFilterForSubSubCategory2ForSubCategory2ForMainCategory6 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory2ForMainCategory6.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Genre"
            };

            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory6 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory3ForMainCategory6.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Video Platform"
            };

            GigFilterForSubSubCategory2ForSubCategory3ForMainCategory6 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory3ForMainCategory6.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Noise Type"
            };

            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory6 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory4ForMainCategory6.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Game Type"
            };

            GigFilterForSubSubCategory2ForSubCategory4ForMainCategory6 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory4ForMainCategory6.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Foley Source"
            };

            // Startup Strategy
            GigFilterForSubSubCategory1ForSubCategory1ForMainCategory7 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory1ForMainCategory7.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Strategy Type"
            };

            // Operational Efficiency
            GigFilterForSubSubCategory2ForSubCategory1ForMainCategory7 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory1ForMainCategory7.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Efficiency Area"
            };

            // Email Management
            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory7 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory2ForMainCategory7.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Email Tools"
            };

            // Competitor Analysis
            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory7 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory3ForMainCategory7.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Analysis Method"
            };

            // Industry Trends
            GigFilterForSubSubCategory2ForSubCategory3ForMainCategory7 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory3ForMainCategory7.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Trend Source"
            };

            // Agile Coaching
            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory7 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory4ForMainCategory7.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Agile Method"
            };

            // Business Budgeting
            GigFilterForSubSubCategory1ForSubCategory1ForMainCategory8 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory1ForMainCategory8.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Budgeting Focus"
            };

            // Debt Management Strategies
            GigFilterForSubSubCategory2ForSubCategory1ForMainCategory8 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory1ForMainCategory8.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Debt Type"
            };

            // Bookkeeping
            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory8 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory2ForMainCategory8.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Accounting Software"
            };

            // Individual Tax Returns
            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory8 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory3ForMainCategory8.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Return Type"
            };

            // Corporate Tax Filing
            GigFilterForSubSubCategory2ForSubCategory3ForMainCategory8 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory3ForMainCategory8.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Filing Service"
            };

            // Portfolio Diversification
            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory8 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory4ForMainCategory8.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Investment Type"
            };
            // Model Training & Evaluation
            GigFilterForSubSubCategory1ForSubCategory1ForMainCategory9 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory1ForMainCategory9.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Training Framework"
            };

            // ML Algorithm Optimization
            GigFilterForSubSubCategory2ForSubCategory1ForMainCategory9 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory1ForMainCategory9.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Optimization Focus"
            };

            // Customer Support Bots
            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory9 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory2ForMainCategory9.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Bot Use Case"
            };

            // Sales & Lead Generation Bots
            GigFilterForSubSubCategory2ForSubCategory2ForMainCategory9 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory2ForMainCategory9.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Lead Gen Type"
            };

            // Workflow Automation
            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory9 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory3ForMainCategory9.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Workflow Type"
            };

            // Business Process Automation
            GigFilterForSubSubCategory2ForSubCategory3ForMainCategory9 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory3ForMainCategory9.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Process Type"
            };

            // Forecasting & Trend Analysis
            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory9 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory4ForMainCategory9.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Forecasting Method"
            };

            // Data Visualization & Insights
            GigFilterForSubSubCategory2ForSubCategory4ForMainCategory9 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory4ForMainCategory9.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Visualization Tools"
            };
            // Career Coaching
            GigFilterForSubSubCategory1ForSubCategory1ForMainCategory10 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory1ForMainCategory10.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Industry Focus"
            };

            // Guided Meditation
            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory10 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory2ForMainCategory10.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Meditation Style"
            };

            // Youth Motivation
            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory10 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory3ForMainCategory10.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Audience Age Group"
            };

            // Fitness Goal Setting
            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory10 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory4ForMainCategory10.Id,
                Type = Domain.Categories.Enums.GigFilterType.ServiceIncludes,
                Title = "Fitness Goal Type"
            };

            // Web Development
            GigFilterForSubSubCategory1ForSubCategory1ForMainCategory2 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory1ForMainCategory2.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Responsive Design"
            };

            GigFilterForSubSubCategory2ForSubCategory1ForMainCategory2 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory1ForMainCategory2.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "API Integration"
            };

            // Mobile Development
            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory2 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory2ForMainCategory2.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "App Store Submission"
            };

            GigFilterForSubSubCategory2ForSubCategory2ForMainCategory2 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory2ForMainCategory2.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Play Store Optimization"
            };
            // Software Solutions
            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory2 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory3ForMainCategory2.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Cross-Platform Support"
            };

            GigFilterForSubSubCategory2ForSubCategory3ForMainCategory2 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory3ForMainCategory2.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Cloud Integration"
            };

            // Game Development
            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory2 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory4ForMainCategory2.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "VR Support"
            };

            GigFilterForSubSubCategory2ForSubCategory4ForMainCategory2 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory4ForMainCategory2.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Multiplayer Integration"
            };
            // Social Media Marketing
            GigFilterForSubSubCategory1ForSubCategory1ForMainCategory3 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory1ForMainCategory3.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Audience Targeting"
            };

            GigFilterForSubSubCategory2ForSubCategory1ForMainCategory3 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory1ForMainCategory3.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Hashtag Strategy"
            };

            // Search Engine Marketing
            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory3 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory2ForMainCategory3.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Keyword Research"
            };

            GigFilterForSubSubCategory2ForSubCategory2ForMainCategory3 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory2ForMainCategory3.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Campaign Optimization"
            };
            // Email Marketing
            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory3 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory3ForMainCategory3.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Template Design"
            };

            GigFilterForSubSubCategory2ForSubCategory3ForMainCategory3 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory3ForMainCategory3.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "A/B Testing"
            };

            // Content Marketing
            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory3 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory4ForMainCategory3.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "SEO Optimization"
            };

            GigFilterForSubSubCategory2ForSubCategory4ForMainCategory3 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory4ForMainCategory3.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Video Scripting"
            };

            // Explainer Videos
            GigFilterForSubSubCategory1ForSubCategory1ForMainCategory4 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory1ForMainCategory4.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Storyboard Creation"
            };

            GigFilterForSubSubCategory2ForSubCategory1ForMainCategory4 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory1ForMainCategory4.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Voice Over"
            };

            // Whiteboard & Animated Explainers
            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory4 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory2ForMainCategory4.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Script Writing"
            };

            GigFilterForSubSubCategory2ForSubCategory2ForMainCategory4 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory2ForMainCategory4.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Background Music"
            };

            // Character Animation
            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory4 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory3ForMainCategory4.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Lip Sync"
            };

            GigFilterForSubSubCategory2ForSubCategory3ForMainCategory4 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory3ForMainCategory4.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "3D Rigging"
            };

            // Video Editing
            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory4 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory1ForSubCategory4ForMainCategory4.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Transitions"
            };

            GigFilterForSubSubCategory2ForSubCategory4ForMainCategory4 = new GigFilter
            {
                SubSubCategoryId = SubSubCategory2ForSubCategory4ForMainCategory4.Id,
                Type = GigFilterType.ServiceIncludes,
                Title = "Subtitling"
            };

            GigFilter1 = new GigFilter
            {
                Type = Domain.Categories.Enums.GigFilterType.PriceRange,
            };
            GigFilter2 = new GigFilter
            {
                Type = Domain.Categories.Enums.GigFilterType.DeliveryTime,
            };
            GigFilter3 = new GigFilter
            {
                Title = "Seller speaks",
                Type = Domain.Categories.Enums.GigFilterType.SellerDetails,
            };
            GigFilter4 = new GigFilter
            {
                Title = "Seller lives in",
                Type = Domain.Categories.Enums.GigFilterType.SellerDetails,
            };
            GigFilter5 = new GigFilter
            {
                Title = "Seller level",
                Type = Domain.Categories.Enums.GigFilterType.SellerDetails,
            };


            var list2 = new List<GigFilter> { GigFilter1, GigFilter2, GigFilter3, GigFilter4, GigFilter5 };
            _context.AddRange(list2);
            var list = new List<GigFilter>{
                GigFilter1ForSubSubCategory1ForSubCategory1ForMainCategory1,
    GigFilter2ForSubSubCategory1ForSubCategory1ForMainCategory1,
    GigFilter3ForSubSubCategory1ForSubCategory1ForMainCategory1,

    GigFilter1ForSubSubCategory2ForSubCategory1ForMainCategory1,
    GigFilter2ForSubSubCategory2ForSubCategory1ForMainCategory1,

    GigFilter1ForSubSubCategory3ForSubCategory1ForMainCategory1,
    GigFilter2ForSubSubCategory3ForSubCategory1ForMainCategory1,

    GigFilter1ForSubSubCategory1ForSubCategory2ForMainCategory1,
    GigFilter2ForSubSubCategory1ForSubCategory2ForMainCategory1,
    GigFilter3ForSubSubCategory1ForSubCategory2ForMainCategory1,

    GigFilter1ForSubSubCategory2ForSubCategory2ForMainCategory1,
    GigFilter2ForSubSubCategory2ForSubCategory2ForMainCategory1,
    GigFilter3ForSubSubCategory2ForSubCategory2ForMainCategory1,

    GigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1,
    GigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1,
    GigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1,

    GigFilter1ForSubSubCategory4ForSubCategory2ForMainCategory1,
    GigFilter2ForSubSubCategory4ForSubCategory2ForMainCategory1,

    GigFilter1ForSubSubCategory5ForSubCategory2ForMainCategory1,
    GigFilter2ForSubSubCategory5ForSubCategory2ForMainCategory1,
    GigFilter3ForSubSubCategory5ForSubCategory2ForMainCategory1,

    GigFilter1ForSubSubCategory1ForSubCategory3ForMainCategory1,
    GigFilter2ForSubSubCategory1ForSubCategory3ForMainCategory1,
    GigFilter3ForSubSubCategory1ForSubCategory3ForMainCategory1,

    GigFilter1ForSubSubCategory2ForSubCategory3ForMainCategory1,
    GigFilter2ForSubSubCategory2ForSubCategory3ForMainCategory1,
    GigFilter3ForSubSubCategory2ForSubCategory3ForMainCategory1,

    GigFilter1ForSubSubCategory3ForSubCategory3ForMainCategory1,
    GigFilter3ForSubSubCategory3ForSubCategory3ForMainCategory1,
                            GigFilterForSubSubCategory2ForSubCategory4ForMainCategory2,
                GigFilterForSubSubCategory1ForSubCategory4ForMainCategory2,
                GigFilterForSubSubCategory2ForSubCategory3ForMainCategory2,
                GigFilterForSubSubCategory1ForSubCategory3ForMainCategory2,
                GigFilterForSubSubCategory1ForSubCategory3ForMainCategory4,
                GigFilterForSubSubCategory2ForSubCategory3ForMainCategory4,
                GigFilterForSubSubCategory1ForSubCategory4ForMainCategory4,
                GigFilterForSubSubCategory2ForSubCategory4ForMainCategory4,
                GigFilterForSubSubCategory2ForSubCategory2ForMainCategory4,
                GigFilterForSubSubCategory1ForSubCategory2ForMainCategory4,
                GigFilterForSubSubCategory2ForSubCategory1ForMainCategory4,
                GigFilterForSubSubCategory1ForSubCategory1ForMainCategory4,
                GigFilterForSubSubCategory2ForSubCategory4ForMainCategory3,
                GigFilterForSubSubCategory1ForSubCategory4ForMainCategory3,
                GigFilterForSubSubCategory2ForSubCategory3ForMainCategory3,
                            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory3,
                GigFilterForSubSubCategory2ForSubCategory2ForMainCategory3,
                GigFilterForSubSubCategory1ForSubCategory2ForMainCategory3,
                GigFilterForSubSubCategory2ForSubCategory1ForMainCategory3,
                GigFilterForSubSubCategory1ForSubCategory1ForMainCategory3,
                            GigFilterForSubSubCategory1ForSubCategory1ForMainCategory3,
                GigFilterForSubSubCategory2ForSubCategory2ForMainCategory2,
                GigFilterForSubSubCategory1ForSubCategory2ForMainCategory2,
                GigFilterForSubSubCategory2ForSubCategory1ForMainCategory2,
                GigFilterForSubSubCategory1ForSubCategory1ForMainCategory2,



                GigFilterForSubSubCategory1ForSubCategory1ForMainCategory5,
            GigFilterForSubSubCategory2ForSubCategory1ForMainCategory5,
            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory5,
            GigFilterForSubSubCategory2ForSubCategory2ForMainCategory5,
            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory5,
            GigFilterForSubSubCategory2ForSubCategory3ForMainCategory5,
            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory5,
            GigFilterForSubSubCategory2ForSubCategory4ForMainCategory5,

            GigFilterForSubSubCategory1ForSubCategory1ForMainCategory6,
            GigFilterForSubSubCategory2ForSubCategory1ForMainCategory6,
            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory6,
            GigFilterForSubSubCategory2ForSubCategory2ForMainCategory6,
            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory6,
            GigFilterForSubSubCategory2ForSubCategory3ForMainCategory6,
            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory6,
            GigFilterForSubSubCategory2ForSubCategory4ForMainCategory6,
                GigFilterForSubSubCategory1ForSubCategory1ForMainCategory7,
            GigFilterForSubSubCategory2ForSubCategory1ForMainCategory7,
            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory7,
            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory7,
            GigFilterForSubSubCategory2ForSubCategory3ForMainCategory7,
            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory7,
            GigFilterForSubSubCategory1ForSubCategory1ForMainCategory8,
            GigFilterForSubSubCategory2ForSubCategory1ForMainCategory8,
            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory8,
            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory8,
            GigFilterForSubSubCategory2ForSubCategory3ForMainCategory8,
            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory8,
                GigFilterForSubSubCategory1ForSubCategory1ForMainCategory10,
            GigFilterForSubSubCategory1ForSubCategory2ForMainCategory10,
            GigFilterForSubSubCategory1ForSubCategory3ForMainCategory10,
            GigFilterForSubSubCategory1ForSubCategory4ForMainCategory10,
           GigFilterForSubSubCategory1ForSubCategory1ForMainCategory9,
           GigFilterForSubSubCategory2ForSubCategory1ForMainCategory9,
           GigFilterForSubSubCategory1ForSubCategory2ForMainCategory9,
           GigFilterForSubSubCategory2ForSubCategory2ForMainCategory9,
           GigFilterForSubSubCategory1ForSubCategory3ForMainCategory9,
           GigFilterForSubSubCategory2ForSubCategory3ForMainCategory9,
           GigFilterForSubSubCategory1ForSubCategory4ForMainCategory9,
           GigFilterForSubSubCategory2ForSubCategory4ForMainCategory9
            };

            _context.AddRange(list);

            await _context.SaveChangesAsync();

        }
    }
    private async Task SeedFilterOptions()
    {
        if (!_context.FilterOptions.Any())
        {
            FilterOption1 = new FilterOption
            {
                Name = "Under $50",
                GigFilterId = GigFilter1.Id
            };
            FilterOption2 = new FilterOption
            {
                Name = "$100 - $200",
                GigFilterId = GigFilter1.Id
            };
            FilterOption3 = new FilterOption
            {
                Name = "Over $500",
                GigFilterId = GigFilter1.Id
            };
            FilterOption4 = new FilterOption
            {
                Name = "Express 24H",
                GigFilterId = GigFilter2.Id
            };
            FilterOption5 = new FilterOption
            {
                Name = "Up to 3 days",
                GigFilterId = GigFilter2.Id
            };
            FilterOption6 = new FilterOption
            {
                Name = "Up to 7 days",
                GigFilterId = GigFilter2.Id
            };
            FilterOption7 = new FilterOption
            {
                Name = "Anytime",
                GigFilterId = GigFilter2.Id
            };
            FilterOption8 = new FilterOption
            {
                Name = "Top Rated",
                GigFilterId = GigFilter5.Id
            };
            FilterOption9 = new FilterOption
            {
                Name = "New Seller",
                GigFilterId = GigFilter5.Id
            };
             FilterOption1ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Mobile App (iOS/Android)"
            };

             FilterOption2ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Web Application"
            };

             FilterOption3ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Desktop Application"
            };

             FilterOption4ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "E-commerce Platform"
            };

             FilterOption5ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "SaaS Dashboard"
            };

             FilterOption6ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Landing Page / Marketing Site"
            };

             FilterOption7ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Progressive Web App (PWA)"
            };

             FilterOption8ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Enterprise / B2B Tool"
            };

             FilterOption1ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Figma"
            };

             FilterOption2ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Adobe XD"
            };

             FilterOption3ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Sketch"
            };

             FilterOption4ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Adobe Photoshop"
            };

             FilterOption5ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Adobe Illustrator"
            };

             FilterOption6ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "InVision"
            };

            FilterOption1ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "User Flow Diagrams"
            };

            FilterOption2ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Wireframes"
            };

            FilterOption3ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "Interactive Prototypes"
            };

            FilterOption4ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1 = new FilterOption()
            {
                GigFilterId = GigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id,
                Name = "UI Style Guide"
            };


            var list = new List<FilterOption> {FilterOption1ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption2ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption3ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption4ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption5ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption6ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption7ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption8ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1,

    // "Design tool"
    FilterOption1ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption2ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption3ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption4ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption5ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption6ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1,

    // "Service includes"
    FilterOption1ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption2ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption3ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1,
    FilterOption4ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1, FilterOption1, FilterOption2, FilterOption3, FilterOption4, FilterOption5, FilterOption6, FilterOption7, FilterOption8, FilterOption9 };
            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedGigs()
    {

        var gig1 = new Gig
        {
            Title = "I will design seamless, intuitive user flows that drive engagement in your mobile app",
            Description = "Struggling to keep users engaged with your mobile app? I specialize in crafting intuitive, user-centered flow designs that guide your audience effortlessly through your product. With a focus on clarity, simplicity, and user psychology, I’ll ensure every tap and swipe makes sense, increasing retention, satisfaction, and overall app success. Let’s transform your app’s usability together. In today's competitive mobile app market, user engagement can make or break your success. I provide comprehensive user flow design services that focus on understanding your users' needs, behaviors, and pain points. By mapping out clear, logical pathways within your app, I help eliminate confusion, reduce drop-offs, and create a smooth, enjoyable experience that keeps users coming back.\\n\\n\" +\r\n                      \"My approach integrates best practices in UX design, behavioral psychology, and accessibility to ensure your app is intuitive for all users. Whether you need flows for onboarding, feature discovery, or task completion, I tailor every design to your specific goals and audience. Expect detailed wireframes, interactive prototypes, and actionable recommendations to optimize usability.\\n\\n\" +\r\n                      \"Together, we will enhance your app's navigation, reduce friction, and drive meaningful engagement that translates into higher retention and better reviews. Let’s collaborate to create user flows that not only look good but truly work.",
            SellerId = Seller1.Id,
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            Clicks = 120,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/t_gig_cards_web_x2,q_auto,f_auto/gigs/194476801/original/cf1635f59548e65ccb0c8a41416792e7d9ee416a.png", "https://fiverr-res.cloudinary.com/images/t_main1,q_auto,f_auto,q_auto,f_auto/gigs/350317870/original/124775437d2adef5dde293b4632d79417fab3ece/do-ux-wireframe-mockup-design-and-user-flow-for-mobile-and-web.jpg" }
        };

        var gig2 = new Gig
        {
            Title = "I will develop detailed, research-driven user personas to supercharge your UX strategy",
            Description = "Great user experiences start with knowing your users. I’ll provide you with in-depth, research-backed user personas tailored specifically to your target market. These personas will help your team make smarter design and business decisions, ensuring every feature you build aligns with real user needs. Don’t guess who your users are—know them, and design accordingly.",
            SellerId = Seller1.Id,
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            Clicks = 95,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/images/q_auto,f_auto/gigs/434229210/original/29c6b3244d558741f37be55d02c830ab9949d613/perform-a-detailed-ux-ui-audit-of-your-app.png" }
        };

        var gig3 = new Gig
        {
            Title = "I will create high-fidelity wireframes that bring your web application ideas to life",
            Description = "Want to see your web app before investing in development? My expertly crafted wireframes offer a clear, visual blueprint of your entire platform. Whether you need low-fidelity sketches for brainstorming or detailed mockups for stakeholder presentations, I’ll help translate your concepts into powerful visual structures that make development faster, smoother, and more successful.",
            SellerId = Seller1.Id,
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow.AddYears(-1),
            Clicks = 160,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/images/t_main1,q_auto,f_auto,q_auto,f_auto/gigs/371794379/original/37e15b1ade683a0235768ecb7129d0b53c379de4/do-web-ui-ux-app-ui-ux-and-dahboard-ui-ux-design-in-figma.png" }
        };

        var gig4 = new Gig
        {
            Title = "I will deliver an expert UX audit to uncover hidden issues on your website",
            Description = "Is your website underperforming? I’ll conduct a thorough, expert-level UX audit to uncover pain points, bottlenecks, and missed opportunities that frustrate your users. You’ll receive a detailed report with actionable insights to improve navigation, accessibility, and user satisfaction. Perfect for startups, SaaS products, or established websites ready for a serious UX upgrade.",
            SellerId = Seller1.Id,
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow.AddMonths(-3),
            Clicks = 75,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/images/q_auto,f_auto/gigs/265069114/original/707a1be825dbbd97a4f52f6ce28b98b7264d7a97/provide-professional-ui-ux-audit-for-websites-landing-pages-apps.png" }
        };

        await _context.Gigs.AddRangeAsync(gig1, gig2, gig3, gig4);
       await _context.SaveChangesAsync();

       await SeedGigDetails(_context, gig1, gig2, gig3, gig4);
    }

    private async Task SeedGigDetails(ApplicationDbContext context, Gig gig1, Gig gig2, Gig gig3, Gig gig4)
    {
        await context.GigMetadatas.AddRangeAsync(
    // gig1 (User Flow Diagrams)
    new GigMetadata { Gig = gig1, FilterOptionId = FilterOption1ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // User Flow Diagrams
    new GigMetadata { Gig = gig1, FilterOptionId = FilterOption4ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Adobe Photoshop
    new GigMetadata { Gig = gig1, FilterOptionId = FilterOption1.Id }, // Under $50
    new GigMetadata { Gig = gig1, FilterOptionId = FilterOption4.Id }, // Express 24H

    // gig2 (Personas)
    new GigMetadata { Gig = gig2, FilterOptionId = FilterOption2ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Adobe XD
    new GigMetadata { Gig = gig2, FilterOptionId = FilterOption2ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Web Application
    new GigMetadata { Gig = gig2, FilterOptionId = FilterOption2.Id }, // $100 - $200
    new GigMetadata { Gig = gig2, FilterOptionId = FilterOption5.Id }, // Up to 3 days

    // gig3 (Wireframes)
    new GigMetadata { Gig = gig3, FilterOptionId = FilterOption2ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Wireframes
    new GigMetadata { Gig = gig3, FilterOptionId = FilterOption1ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Figma
    new GigMetadata { Gig = gig3, FilterOptionId = FilterOption2.Id }, // $100 - $200
    new GigMetadata { Gig = gig3, FilterOptionId = FilterOption5.Id }, // Up to 3 days

    // gig4 (UX Audit)
    new GigMetadata { Gig = gig4, FilterOptionId = FilterOption6ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Landing Page / Marketing Site
    new GigMetadata { Gig = gig4, FilterOptionId = FilterOption3ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Sketch
    new GigMetadata { Gig = gig4, FilterOptionId = FilterOption3.Id }, // Over $500
    new GigMetadata { Gig = gig4, FilterOptionId = FilterOption6.Id } // Up to 7 days
);

        context.FAQs.AddRange(
            new FAQ { GigId = gig1.Id, Question = "Do you provide wireframes?", Answer = "Yes, I will provide wireframes in Figma or Adobe XD." },
            new FAQ { GigId = gig1.Id, Question = "Can you redesign my existing app?", Answer = "Absolutely, I specialize in revamping old interfaces." },

            new FAQ { GigId = gig2.Id, Question = "How do you collect user data?", Answer = "Through surveys, interviews, and competitive research." },

            new FAQ { GigId = gig3.Id, Question = "Do you deliver in Figma or XD?", Answer = "I primarily use Figma, but XD is available on request." },

            new FAQ { GigId = gig4.Id, Question = "Can you audit e-commerce sites?", Answer = "Yes, I specialize in UX audits for e-commerce platforms." }
        );

        await context.GigRequirements.AddRangeAsync(
            new GigRequirement { GigId = gig1.Id, Question = "Please describe your app's primary functionality."},
            new GigRequirement { GigId = gig2.Id, Question = "Provide details about your target market or ideal customer." },
            new GigRequirement { GigId = gig3.Id, Question = "Share your website objectives and target audience." },
            new GigRequirement { GigId = gig4.Id, Question = "Provide your website URL and goals for the audit."}
        );

        var paymentPlan1Basic = new PaymentPlan
        {
            GigId = gig1.Id,
            Name = "Basic",
            Price = 150,
            DeliveryTimeInDays = 3,
            Revisions = 2,
            Description = "User flow for up to 3 screens."
        };
        var paymentPlan1Standard = new PaymentPlan
        {
            GigId = gig1.Id,
            Name = "Standard",
            Price = 300,
            DeliveryTimeInDays = 5,
            Revisions = 3,
            Description = "User flow for up to 6 screens."
        };
        var paymentPlan1Premium = new PaymentPlan
        {
            GigId = gig1.Id,
            Name = "Premium",
            Price = 500,
            DeliveryTimeInDays = 7,
            Revisions = 5,
            Description = "User flow for up to 10 screens with feedback sessions."
        };

        var paymentPlan2Basic = new PaymentPlan
        {
            GigId = gig2.Id,
            Name = "Basic",
            Price = 100,
            DeliveryTimeInDays = 2,
            Revisions = 1,
            Description = "1 user persona profile."
        };
        var paymentPlan2Standard = new PaymentPlan
        {
            GigId = gig2.Id,
            Name = "Standard",
            Price = 200,
            DeliveryTimeInDays = 4,
            Revisions = 2,
            Description = "3 detailed personas."
        };
        var paymentPlan2Premium = new PaymentPlan
        {
            GigId = gig2.Id,
            Name = "Premium",
            Price = 350,
            DeliveryTimeInDays = 6,
            Revisions = 3,
            Description = "5 personas with market analysis."
        };

        var paymentPlan3Basic = new PaymentPlan
        {
            GigId = gig3.Id,
            Name = "Basic",
            Price = 120,
            DeliveryTimeInDays = 2,
            Revisions = 1,
            Description = "Wireframes for up to 3 web pages."
        };
        var paymentPlan3Standard = new PaymentPlan
        {
            GigId = gig3.Id,
            Name = "Standard",
            Price = 240,
            DeliveryTimeInDays = 4,
            Revisions = 2,
            Description = "Wireframes for up to 6 web pages."
        };
        var paymentPlan3Premium = new PaymentPlan
        {
            GigId = gig3.Id,
            Name = "Premium",
            Price = 400,
            DeliveryTimeInDays = 6,
            Revisions = 3,
            Description = "Wireframes for 10+ web pages with consultation."
        };

        var paymentPlan4Basic = new PaymentPlan
        {
            GigId = gig4.Id,
            Name = "Basic",
            Price = 180,
            DeliveryTimeInDays = 3,
            Revisions = 1,
            Description = "UX audit of 1 landing page with a report."
        };
        var paymentPlan4Standard = new PaymentPlan
        {
            GigId = gig4.Id,
            Name = "Standard",
            Price = 350,
            DeliveryTimeInDays = 5,
            Revisions = 2,
            Description = "UX audit of up to 3 key pages."
        };
        var paymentPlan4Premium = new PaymentPlan
        {
            GigId = gig4.Id,
            Name = "Premium",
            Price = 600,
            DeliveryTimeInDays = 7,
            Revisions = 3,
            Description = "UX audit of 5+ pages with actionable recommendations."
        };

       await context.PaymentPlans.AddRangeAsync(
            paymentPlan1Basic, paymentPlan1Standard, paymentPlan1Premium,
            paymentPlan2Basic, paymentPlan2Standard, paymentPlan2Premium,
            paymentPlan3Basic, paymentPlan3Standard, paymentPlan3Premium,
            paymentPlan4Basic, paymentPlan4Standard, paymentPlan4Premium
        );
        context.FAQs.AddRange(
    new FAQ { GigId = gig1.Id, Question = "What if I don’t have a clear app idea yet?", Answer = "No worries! I can help you clarify your concept and suggest optimal flows based on your goals." },
    new FAQ { GigId = gig1.Id, Question = "Can you collaborate with my developer?", Answer = "Yes, I’m happy to coordinate with your development team to ensure smooth handoff." },
    new FAQ { GigId = gig1.Id, Question = "Are revisions included?", Answer = "Yes, each package comes with a specific number of revisions. Additional revisions can be arranged if needed." }
);
        await context.GigRequirements.AddRangeAsync(
            new GigRequirement { GigId = gig1.Id, Question = "Do you have any competitor apps you admire for reference?" },
            new GigRequirement { GigId = gig1.Id, Question = "What is the main goal you want your users to achieve with this app?" },
            new GigRequirement { GigId = gig1.Id, Question = "Are there specific branding guidelines or color schemes to follow?" }
        );

        await context.Tags.AddRangeAsync(
       new Tag { GigId = gig1.Id, Name = "Mobile UX" },
       new Tag { GigId = gig1.Id, Name = "User Flow Design" },
       new Tag { GigId = gig1.Id, Name = "Figma" },
       new Tag { GigId = gig1.Id, Name = "User Journey" },
       new Tag { GigId = gig1.Id, Name = "App Design" },
       new Tag { GigId = gig1.Id, Name = "Retention Optimization" }
   );


        await context.SaveChangesAsync();

        await context.PaymentPlanIncludes.AddRangeAsync(
// Extra inclusions for gig1 Basic
new PaymentPlanInclude { PaymentPlanId = paymentPlan1Basic.Id, Name = "User Flow Format", Value = "Interactive prototype link" },
new PaymentPlanInclude { PaymentPlanId = paymentPlan1Basic.Id, Name = "Revision Support", Value = "Email only" },

// Extra inclusions for gig1 Standard
new PaymentPlanInclude { PaymentPlanId = paymentPlan1Standard.Id, Name = "User Flow Format", Value = "Clickable prototype + PDF" },
new PaymentPlanInclude { PaymentPlanId = paymentPlan1Standard.Id, Name = "Revision Support", Value = "Email + Chat" },

// Extra inclusions for gig1 Premium
new PaymentPlanInclude { PaymentPlanId = paymentPlan1Premium.Id, Name = "Priority Support", Value = "24-hour response time" },
new PaymentPlanInclude { PaymentPlanId = paymentPlan1Premium.Id, Name = "Consultation Calls", Value = "Includes strategy call" },
new PaymentPlanInclude { PaymentPlanId = paymentPlan1Premium.Id, Name = "Bonus", Value = "Free post-delivery support for 1 week" }
);

        await context.PaymentPlanIncludes.AddRangeAsync(
            // Gig 1
            new PaymentPlanInclude { PaymentPlanId = paymentPlan1Basic.Id, Name = "Screens", Value = "3" },
            new PaymentPlanInclude { PaymentPlanId = paymentPlan1Basic.Id, Name = "Format", Value = "Figma" },

            new PaymentPlanInclude { PaymentPlanId = paymentPlan1Standard.Id, Name = "Screens", Value = "6" },
            new PaymentPlanInclude { PaymentPlanId = paymentPlan1Standard.Id, Name = "Format", Value = "Figma + PDF" },

            new PaymentPlanInclude { PaymentPlanId = paymentPlan1Premium.Id, Name = "Screens", Value = "10" },
            new PaymentPlanInclude { PaymentPlanId = paymentPlan1Premium.Id, Name = "Consultations", Value = "2 video calls" },

            // Gig 2
            new PaymentPlanInclude { PaymentPlanId = paymentPlan2Basic.Id, Name = "Personas", Value = "1" },
            new PaymentPlanInclude { PaymentPlanId = paymentPlan2Standard.Id, Name = "Personas", Value = "3" },
            new PaymentPlanInclude { PaymentPlanId = paymentPlan2Premium.Id, Name = "Personas", Value = "5" },
            new PaymentPlanInclude { PaymentPlanId = paymentPlan2Premium.Id, Name = "Includes", Value = "Market Analysis PDF" },

            // Gig 3
            new PaymentPlanInclude { PaymentPlanId = paymentPlan3Basic.Id, Name = "Pages", Value = "3" },
            new PaymentPlanInclude { PaymentPlanId = paymentPlan3Standard.Id, Name = "Pages", Value = "6" },
            new PaymentPlanInclude { PaymentPlanId = paymentPlan3Premium.Id, Name = "Pages", Value = "10+" },
            new PaymentPlanInclude { PaymentPlanId = paymentPlan3Premium.Id, Name = "Includes", Value = "30-minute consultation" },

            // Gig 4
            new PaymentPlanInclude { PaymentPlanId = paymentPlan4Basic.Id, Name = "Pages Audited", Value = "1" },
            new PaymentPlanInclude { PaymentPlanId = paymentPlan4Standard.Id, Name = "Pages Audited", Value = "3" },
            new PaymentPlanInclude { PaymentPlanId = paymentPlan4Premium.Id, Name = "Pages Audited", Value = "5+" },
            new PaymentPlanInclude { PaymentPlanId = paymentPlan4Premium.Id, Name = "Includes", Value = "Action Plan Document" }
        );

        var gigMetadata = new List<GigMetadata>
{
    // Gig 1 Metadata
    new GigMetadata { Gig = gig1, FilterOptionId = FilterOption1ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Mobile App (iOS/Android)
    new GigMetadata { Gig = gig1, FilterOptionId = FilterOption1ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Figma
    new GigMetadata { Gig = gig1, FilterOptionId = FilterOption1ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // User Flow Diagrams

    // Gig 2 Metadata
    new GigMetadata { Gig = gig2, FilterOptionId = FilterOption2ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Web Application
    new GigMetadata { Gig = gig2, FilterOptionId = FilterOption2ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Adobe XD
    new GigMetadata { Gig = gig2, FilterOptionId = FilterOption4ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // UI Style Guide

    // Gig 3 Metadata
    new GigMetadata { Gig = gig3, FilterOptionId = FilterOption2ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Web Application
    new GigMetadata { Gig = gig3, FilterOptionId = FilterOption3ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id   }, // Sketch
    new GigMetadata { Gig = gig3, FilterOptionId = FilterOption2ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Wireframes

    // Gig 4 Metadata
    new GigMetadata { Gig = gig4, FilterOptionId = FilterOption6ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id    }, // Landing Page / Marketing Site
    new GigMetadata { Gig = gig4, FilterOptionId = FilterOption4ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Adobe Photoshop
    new GigMetadata { Gig = gig4, FilterOptionId = FilterOption3ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Interactive Prototypes
};

        await context.GigMetadatas.AddRangeAsync(gigMetadata);


        await context.SaveChangesAsync();
    }

    private async Task SeedSeller2Gigs(ApplicationDbContext context, Guid seller2Id, Guid uxDesignSubSubCategoryId)
    {
        var gig5 = new Gig
        {
            SellerId = Seller2.Id,
            Title = "I will create intuitive UX flows for web applications",
            Description = "I specialize in designing user experiences that maximize engagement and conversions for web platforms.",
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/images/t_main1,q_auto,f_auto,q_auto,f_auto/gigs/236795969/original/bcf590258d0fd5b80678a9767897a3e8fa13d14f/develop-your-ui-ux-web-flow-on-figma-and-illustrator.png" }
        };

        var gig6 = new Gig
        {
            SellerId = Seller2.Id,
            Title = "I will audit your UX and provide actionable improvements",
            Description = "Get a detailed UX review of your existing product with prioritized recommendations and redesign suggestions.",
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/images/t_main1,q_auto,f_auto,q_auto,f_auto/gigs/420981423/original/b51b57e1cb2e05455932fce858e6ac4dec1e7a0d/design-an-eye-catching-landing-page-with-conversion-focused.jpg" }
        };

        var gig7 = new Gig
        {
            SellerId = Seller2.Id,
            Title = "I will design mobile-first UX wireframes",
            Description = "Mobile-first design is key today. I will craft detailed UX wireframes for your app to optimize usability and flow.",
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/t_gig_cards_web_x2,q_auto,f_auto/gigs/181358952/original/71ce59dd7a3bf2007ad6a8e9edb4a313012e3554.png" }
        };

        await context.Gigs.AddRangeAsync(gig5, gig6, gig7);
        await context.SaveChangesAsync();

        var gigMetadata = new List<GigMetadata>
    {
        // For gig5 (e.g. "I will create intuitive UX flows for web applications")
        new GigMetadata
        {
            GigId = gig5.Id,
            FilterOptionId = FilterOption5ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id // "SaaS Dashboard"
        },
        new GigMetadata
        {
            GigId = gig5.Id,
            FilterOptionId = FilterOption2ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id // "Web Application"
        },

        // For gig6
        new GigMetadata
        {
            GigId = gig6.Id,
            FilterOptionId = FilterOption1ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id // "Figma"
        },
        new GigMetadata
        {
            GigId = gig6.Id,
            FilterOptionId = FilterOption3ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id // "Interactive Prototypes"
        },

        // For gig7
        new GigMetadata
        {
            GigId = gig7.Id,
            FilterOptionId = FilterOption7ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id // "Progressive Web App (PWA)"
        },
        new GigMetadata
        {
            GigId = gig7.Id,
            FilterOptionId = FilterOption4ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id // "E-commerce Platform"
        }
    };
        await context.GigMetadatas.AddRangeAsync(gigMetadata);

        // PaymentPlans
        var plans = new List<PaymentPlan>
    {
        new PaymentPlan { GigId = gig5.Id, Name = "Basic", Price = 120, DeliveryTimeInDays = 3, Revisions = 2, Description = "UX flow for up to 3 web screens." },
        new PaymentPlan { GigId = gig5.Id, Name = "Standard", Price = 250, DeliveryTimeInDays = 5, Revisions = 3, Description = "UX flow for up to 6 web screens." },
        new PaymentPlan { GigId = gig5.Id, Name = "Premium", Price = 400, DeliveryTimeInDays = 7, Revisions = 5, Description = "UX flow for up to 10 screens with prototype feedback." },

        new PaymentPlan { GigId = gig6.Id, Name = "Basic", Price = 80, DeliveryTimeInDays = 2, Revisions = 1, Description = "UX audit with 3 key recommendations." },
        new PaymentPlan { GigId = gig6.Id, Name = "Standard", Price = 150, DeliveryTimeInDays = 4, Revisions = 2, Description = "UX audit with 6 recommendations and screenshots." },
        new PaymentPlan { GigId = gig6.Id, Name = "Premium", Price = 250, DeliveryTimeInDays = 6, Revisions = 3, Description = "UX audit with 10 recommendations + wireframe suggestions." },

        new PaymentPlan { GigId = gig7.Id, Name = "Basic", Price = 140, DeliveryTimeInDays = 3, Revisions = 2, Description = "Mobile UX wireframes for 3 screens." },
        new PaymentPlan { GigId = gig7.Id, Name = "Standard", Price = 280, DeliveryTimeInDays = 5, Revisions = 3, Description = "Mobile UX wireframes for 6 screens." },
        new PaymentPlan { GigId = gig7.Id, Name = "Premium", Price = 450, DeliveryTimeInDays = 7, Revisions = 5, Description = "Complete mobile-first UX wireframes for up to 10 screens." }
    };
        await context.PaymentPlans.AddRangeAsync(plans);
        await context.SaveChangesAsync();

        // PaymentPlanIncludes
        var includes = new List<PaymentPlanInclude>
    {
        new PaymentPlanInclude { PaymentPlanId = plans[0].Id, Name = "User flow diagram" },
        new PaymentPlanInclude { PaymentPlanId = plans[1].Id, Name = "Clickable prototype (optional)" },
        new PaymentPlanInclude { PaymentPlanId = plans[2].Id, Name = "Clickable prototype + feedback loop" },

        new PaymentPlanInclude { PaymentPlanId = plans[3].Id, Name = "3 recommendations" },
        new PaymentPlanInclude { PaymentPlanId = plans[4].Id, Name = "6 recommendations with screenshots" },
        new PaymentPlanInclude { PaymentPlanId = plans[5].Id, Name = "Full report + wireframe suggestions" },

        new PaymentPlanInclude { PaymentPlanId = plans[6].Id, Name = "3 screen wireframes" },
        new PaymentPlanInclude { PaymentPlanId = plans[7].Id, Name = "6 screen wireframes" },
        new PaymentPlanInclude { PaymentPlanId = plans[8].Id, Name = "Up to 10 screen wireframes with notes" }
    };
        await context.PaymentPlanIncludes.AddRangeAsync(includes);
        await context.SaveChangesAsync();

        // FAQs
        await context.FAQs.AddRangeAsync(
            new FAQ { GigId = gig5.Id, Question = "Do you provide clickable prototypes?", Answer = "Yes, available for Premium packages." },
            new FAQ { GigId = gig5.Id, Question = "Can you follow existing design guidelines?", Answer = "Absolutely, just provide your brand guidelines." },

            new FAQ { GigId = gig6.Id, Question = "What formats will the UX audit be in?", Answer = "Typically PDF with screenshots and recommendations." },
            new FAQ { GigId = gig6.Id, Question = "Is the audit suitable for mobile apps?", Answer = "Yes, I tailor audits to the platform you specify." },

            new FAQ { GigId = gig7.Id, Question = "Can you design for both iOS and Android?", Answer = "Yes, wireframes will consider both platform guidelines." },
            new FAQ { GigId = gig7.Id, Question = "Do you use tools like Figma or Sketch?", Answer = "Yes, Figma is my primary tool for wireframes." }
        );
        await context.SaveChangesAsync();

        // GigRequirements
        await context.GigRequirements.AddRangeAsync(
            new GigRequirement { GigId = gig5.Id, Question = "Provide a description of your product and target audience." },
            new GigRequirement { GigId = gig6.Id, Question = "Provide link to your existing platform or screenshots." },
            new GigRequirement { GigId = gig7.Id, Question = "Describe the core functionality of your app and preferred platform (iOS/Android)." }
        );
        await context.SaveChangesAsync();

        // Tags
        await context.Tags.AddRangeAsync(
            new Tag { GigId = gig5.Id, Name = "UX Design" },
            new Tag { GigId = gig5.Id, Name = "User Flow" },
            new Tag { GigId = gig6.Id, Name = "UX Audit" },
            new Tag { GigId = gig6.Id, Name = "Usability Review" },
            new Tag { GigId = gig7.Id, Name = "Mobile UX" },
            new Tag { GigId = gig7.Id, Name = "Wireframes" }
        );
        await context.SaveChangesAsync();
    }

    private async Task SeedSeller4Gigs(ApplicationDbContext context, Guid seller4Id, Guid uxDesignSubSubCategoryId)
    {
        // Create 4 gigs for Seller4
        var gig1 = new Gig
        {
            SellerId = Seller4.Id,
            Title = "I will design engaging UX for your mobile apps",
            Description = "Creating seamless and intuitive user experiences tailored for mobile platforms (iOS and Android).",
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/t_gig_cards_web_x2,q_auto,f_auto/gigs/316929973/original/e7078dbec5944fc4e08b9474e19ee1ce5920f49a.jpg" }
        };

        var gig2 = new Gig
        {
            SellerId = Seller4.Id,
            Title = "I will craft wireframes and prototypes for your web projects",
            Description = "Delivering detailed wireframes and interactive prototypes to visualize your web app ideas effectively.",
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/t_gig_cards_web_x2,q_auto,f_auto/gigs/154467581/original/24bafd54bf8963111e9f844c25ee4223705b6168.png" }
        };

        var gig3 = new Gig
        {
            SellerId = Seller4.Id,
            Title = "I will perform UX audits and usability testing",
            Description = "Comprehensive UX audits with actionable feedback to improve user engagement and flow.",
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/t_gig_cards_web_x2,q_auto,f_auto/gigs/259429616/original/f7388c43bcf192fe4097296675c6eba6bd2c7e6e.png" }
        };

        var gig4 = new Gig
        {
            SellerId = Seller4.Id,
            Title = "I will create SaaS dashboard UX designs",
            Description = "Designing scalable and user-friendly dashboards optimized for SaaS platforms.",
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/t_gig_cards_web_x2,q_auto,f_auto/gigs/294093683/original/e707e043d404810dbcb59da14caabe41500c3d90.png" }
        };

        await context.Gigs.AddRangeAsync(gig1, gig2, gig3, gig4);
        await context.SaveChangesAsync();

        // Add GigMetadata for filter options
        var gigMetadata = new List<GigMetadata>
    {
        // gig1 - Mobile app focus
        new GigMetadata { GigId = gig1.Id, FilterOptionId = FilterOption1ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Mobile App (iOS/Android)
        new GigMetadata { GigId = gig1.Id, FilterOptionId = FilterOption1ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Figma

        // gig2 - Web projects with wireframes and prototypes
        new GigMetadata { GigId = gig2.Id, FilterOptionId = FilterOption2ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Web Application
        new GigMetadata { GigId = gig2.Id, FilterOptionId = FilterOption2ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Adobe XD
        new GigMetadata { GigId = gig2.Id, FilterOptionId = FilterOption3ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Interactive Prototypes

        // gig3 - UX audits
        new GigMetadata { GigId = gig3.Id, FilterOptionId = FilterOption4ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Adobe Photoshop
        new GigMetadata { GigId = gig3.Id, FilterOptionId = FilterOption1ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // User Flow Diagrams

        // gig4 - SaaS dashboard
        new GigMetadata { GigId = gig4.Id, FilterOptionId = FilterOption5ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // SaaS Dashboard
        new GigMetadata { GigId = gig4.Id, FilterOptionId = FilterOption5ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }  // Adobe Illustrator
    };
        await context.GigMetadatas.AddRangeAsync(gigMetadata);

        // PaymentPlans for each gig
        var plans = new List<PaymentPlan>
    {
        // gig1 plans
        new PaymentPlan { GigId = gig1.Id, Name = "Basic", Price = 100, DeliveryTimeInDays = 3, Revisions = 1, Description = "Basic UX design for up to 3 mobile screens." },
        new PaymentPlan { GigId = gig1.Id, Name = "Standard", Price = 200, DeliveryTimeInDays = 5, Revisions = 2, Description = "UX design for up to 7 mobile screens." },
        new PaymentPlan { GigId = gig1.Id, Name = "Premium", Price = 350, DeliveryTimeInDays = 7, Revisions = 3, Description = "Complete UX design for up to 10 mobile screens with prototype." },

        // gig2 plans
        new PaymentPlan { GigId = gig2.Id, Name = "Basic", Price = 90, DeliveryTimeInDays = 2, Revisions = 1, Description = "Wireframes for up to 3 web pages." },
        new PaymentPlan { GigId = gig2.Id, Name = "Standard", Price = 180, DeliveryTimeInDays = 4, Revisions = 2, Description = "Wireframes + basic interactive prototype for 5 pages." },
        new PaymentPlan { GigId = gig2.Id, Name = "Premium", Price = 300, DeliveryTimeInDays = 6, Revisions = 3, Description = "Wireframes + interactive prototype for up to 10 pages." },

        // gig3 plans
        new PaymentPlan { GigId = gig3.Id, Name = "Basic", Price = 70, DeliveryTimeInDays = 3, Revisions = 1, Description = "UX audit with 3 recommendations." },
        new PaymentPlan { GigId = gig3.Id, Name = "Standard", Price = 140, DeliveryTimeInDays = 5, Revisions = 2, Description = "UX audit with detailed report and 6 recommendations." },
        new PaymentPlan { GigId = gig3.Id, Name = "Premium", Price = 250, DeliveryTimeInDays = 7, Revisions = 3, Description = "Full UX audit with usability testing and 10 recommendations." },

        // gig4 plans
        new PaymentPlan { GigId = gig4.Id, Name = "Basic", Price = 120, DeliveryTimeInDays = 4, Revisions = 1, Description = "Basic SaaS dashboard UX for 3 widgets." },
        new PaymentPlan { GigId = gig4.Id, Name = "Standard", Price = 230, DeliveryTimeInDays = 6, Revisions = 2, Description = "SaaS dashboard UX with 6 widgets." },
        new PaymentPlan { GigId = gig4.Id, Name = "Premium", Price = 400, DeliveryTimeInDays = 9, Revisions = 4, Description = "Complete SaaS dashboard UX design with prototype and feedback." }
    };
        await context.PaymentPlans.AddRangeAsync(plans);
        await context.SaveChangesAsync();

        // PaymentPlanIncludes
        var includes = new List<PaymentPlanInclude>
    {
        // gig1 includes
        new PaymentPlanInclude { PaymentPlanId = plans[0].Id, Name = "Basic mobile UX screens" },
        new PaymentPlanInclude { PaymentPlanId = plans[1].Id, Name = "Extended mobile UX screens" },
        new PaymentPlanInclude { PaymentPlanId = plans[2].Id, Name = "Interactive prototype with feedback" },

        // gig2 includes
        new PaymentPlanInclude { PaymentPlanId = plans[3].Id, Name = "Basic wireframes" },
        new PaymentPlanInclude { PaymentPlanId = plans[4].Id, Name = "Wireframes + clickable prototype" },
        new PaymentPlanInclude { PaymentPlanId = plans[5].Id, Name = "Full interactive prototype" },

        // gig3 includes
        new PaymentPlanInclude { PaymentPlanId = plans[6].Id, Name = "Summary report with recommendations" },
        new PaymentPlanInclude { PaymentPlanId = plans[7].Id, Name = "Detailed audit report" },
        new PaymentPlanInclude { PaymentPlanId = plans[8].Id, Name = "Usability testing results and report" },

        // gig4 includes
        new PaymentPlanInclude { PaymentPlanId = plans[9].Id, Name = "Basic dashboard UX" },
        new PaymentPlanInclude { PaymentPlanId = plans[10].Id, Name = "Dashboard UX + interaction design" },
        new PaymentPlanInclude { PaymentPlanId = plans[11].Id, Name = "Full dashboard UX with prototype" }
    };
        await context.PaymentPlanIncludes.AddRangeAsync(includes);
        await context.SaveChangesAsync();

        // FAQs
        await context.FAQs.AddRangeAsync(
            // gig1 FAQs
            new FAQ { GigId = gig1.Id, Question = "Can you design for both iOS and Android?", Answer = "Yes, I design UX optimized for both platforms." },
            new FAQ { GigId = gig1.Id, Question = "Do you provide clickable prototypes?", Answer = "Clickable prototypes are included in Standard and Premium plans." },

            // gig2 FAQs
            new FAQ { GigId = gig2.Id, Question = "What tools do you use?", Answer = "I mainly use Figma and Adobe XD for wireframes and prototypes." },
            new FAQ { GigId = gig2.Id, Question = "Can I request revisions?", Answer = "Yes, revisions depend on the plan you choose." },

            // gig3 FAQs
            new FAQ { GigId = gig3.Id, Question = "How is the UX audit delivered?", Answer = "You will receive a detailed PDF report with screenshots and recommendations." },
            new FAQ { GigId = gig3.Id, Question = "Is usability testing included?", Answer = "Included only in Premium packages." },

            // gig4 FAQs
            new FAQ { GigId = gig4.Id, Question = "Do you follow SaaS best practices?", Answer = "Absolutely, I design dashboards optimized for SaaS user needs." },
            new FAQ { GigId = gig4.Id, Question = "Can you provide ongoing support?", Answer = "Yes, please contact me for custom support plans." }
        );
        await context.SaveChangesAsync();

        // GigRequirements
        await context.GigRequirements.AddRangeAsync(
            new GigRequirement { GigId = gig1.Id, Question = "Please provide your app idea and target users." },
            new GigRequirement { GigId = gig2.Id, Question = "Share any existing wireframes or designs you have." },
            new GigRequirement { GigId = gig3.Id, Question = "Provide access or screenshots of your current product for audit." },
            new GigRequirement { GigId = gig4.Id, Question = "Describe your SaaS platform and dashboard requirements." }
        );
        await context.SaveChangesAsync();

        // Tags
        await context.Tags.AddRangeAsync(
            new Tag { GigId = gig1.Id, Name = "Mobile UX" },
            new Tag { GigId = gig1.Id, Name = "App Design" },
            new Tag { GigId = gig2.Id, Name = "Wireframes" },
            new Tag { GigId = gig2.Id, Name = "Prototypes" },
            new Tag { GigId = gig3.Id, Name = "UX Audit" },
            new Tag { GigId = gig3.Id, Name = "Usability" },
            new Tag { GigId = gig4.Id, Name = "SaaS Dashboard" },
            new Tag { GigId = gig4.Id, Name = "Dashboard UX" }
        );
        await context.SaveChangesAsync();
    }

    private async Task SeedSeller5Gigs(ApplicationDbContext context, Guid seller5Id, Guid uxDesignSubSubCategoryId)
    {
        // Create 4 gigs for Seller5
        var gig1 = new Gig
        {
            SellerId = Seller5.Id,
            Title = "I will design intuitive UX for your e-commerce site",
            Description = "Crafting user-friendly and engaging UX designs tailored for e-commerce platforms.",
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/t_gig_cards_web_x2,q_auto,f_auto/gigs/363963639/original/66775e4135ae7a3664f6ceb6366ccddfadbd2cbb.png" }
        };

        var gig2 = new Gig
        {
            SellerId = Seller5.Id,
            Title = "I will create responsive UX wireframes and mockups",
            Description = "Developing responsive wireframes and mockups to enhance usability across devices.",
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/t_gig_cards_web_x2,q_auto,f_auto/gigs/259633200/original/8bc33392a1d6d4f5eb139e656b401f258ffe0b48.jpg" }
        };

        var gig3 = new Gig
        {
            SellerId = Seller5.Id,
            Title = "I will conduct UX research and persona development",
            Description = "Performing detailed UX research and creating personas to guide design strategy.",
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/t_gig_cards_web_x2,q_auto,f_auto/gigs/410953063/original/c3ec8687bc10ea00ce21e15f06c9ebaf7132c2d4.png" }
        };

        var gig4 = new Gig
        {
            SellerId = Seller5.Id,
            Title = "I will optimize SaaS UX for better user retention",
            Description = "Improving SaaS platform UX focusing on user retention and engagement.",
            SubSubCategoryId = SubSubCategory3ForSubCategory2ForMainCategory1.Id,
            CreatedAt = DateTime.UtcNow,
            ModerationStatus = ModerationStatus.Active,
            ImageUrls = new List<string> { "https://fiverr-res.cloudinary.com/t_gig_cards_web_x2,q_auto,f_auto/gigs/314592551/original/6efd4b7752e90cf965bf95aa60501035e7161053.jpg" }
        };

        await context.Gigs.AddRangeAsync(gig1, gig2, gig3, gig4);
        await context.SaveChangesAsync();

        // Add GigMetadata for filter options
        var gigMetadata = new List<GigMetadata>
    {
        // gig1 - E-commerce UX focus
        new GigMetadata { GigId = gig1.Id, FilterOptionId = FilterOption4ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // E-commerce Platform
        new GigMetadata { GigId = gig1.Id, FilterOptionId = FilterOption1ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Figma

        // gig2 - Responsive wireframes
        new GigMetadata { GigId = gig2.Id, FilterOptionId = FilterOption2ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Web Application
        new GigMetadata { GigId = gig2.Id, FilterOptionId = FilterOption3ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Sketch
        new GigMetadata { GigId = gig2.Id, FilterOptionId = FilterOption2ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Wireframes

        // gig3 - UX research
        new GigMetadata { GigId = gig3.Id, FilterOptionId = FilterOption1ForGigFilter3ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // User Flow Diagrams
        new GigMetadata { GigId = gig3.Id, FilterOptionId = FilterOption4ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // Adobe Photoshop

        // gig4 - SaaS UX optimization
        new GigMetadata { GigId = gig4.Id, FilterOptionId = FilterOption5ForGigFilter1ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }, // SaaS Dashboard
        new GigMetadata { GigId = gig4.Id, FilterOptionId = FilterOption5ForGigFilter2ForSubSubCategory3ForSubCategory2ForMainCategory1.Id }  // Adobe Illustrator
    };
        await context.GigMetadatas.AddRangeAsync(gigMetadata);

        // PaymentPlans for each gig
        var plans = new List<PaymentPlan>
    {
        // gig1 plans
        new PaymentPlan { GigId = gig1.Id, Name = "Basic", Price = 110, DeliveryTimeInDays = 3, Revisions = 1, Description = "Basic UX for e-commerce homepage." },
        new PaymentPlan { GigId = gig1.Id, Name = "Standard", Price = 210, DeliveryTimeInDays = 5, Revisions = 2, Description = "UX design for up to 5 e-commerce pages." },
        new PaymentPlan { GigId = gig1.Id, Name = "Premium", Price = 360, DeliveryTimeInDays = 8, Revisions = 3, Description = "Full e-commerce UX design including checkout flow." },

        // gig2 plans
        new PaymentPlan { GigId = gig2.Id, Name = "Basic", Price = 95, DeliveryTimeInDays = 2, Revisions = 1, Description = "Responsive wireframes for 3 pages." },
        new PaymentPlan { GigId = gig2.Id, Name = "Standard", Price = 190, DeliveryTimeInDays = 4, Revisions = 2, Description = "Wireframes + mockups for 6 pages." },
        new PaymentPlan { GigId = gig2.Id, Name = "Premium", Price = 310, DeliveryTimeInDays = 7, Revisions = 3, Description = "Complete responsive wireframes and mockups for 10 pages." },

        // gig3 plans
        new PaymentPlan { GigId = gig3.Id, Name = "Basic", Price = 80, DeliveryTimeInDays = 4, Revisions = 1, Description = "Basic UX research and 2 personas." },
        new PaymentPlan { GigId = gig3.Id, Name = "Standard", Price = 150, DeliveryTimeInDays = 6, Revisions = 2, Description = "Detailed UX research and 4 personas." },
        new PaymentPlan { GigId = gig3.Id, Name = "Premium", Price = 270, DeliveryTimeInDays = 9, Revisions = 3, Description = "Comprehensive research, personas, and user journey maps." },

        // gig4 plans
        new PaymentPlan { GigId = gig4.Id, Name = "Basic", Price = 130, DeliveryTimeInDays = 4, Revisions = 1, Description = "Basic SaaS UX improvements for 3 features." },
        new PaymentPlan { GigId = gig4.Id, Name = "Standard", Price = 240, DeliveryTimeInDays = 6, Revisions = 2, Description = "UX optimization for 6 SaaS features." },
        new PaymentPlan { GigId = gig4.Id, Name = "Premium", Price = 420, DeliveryTimeInDays = 10, Revisions = 4, Description = "Complete UX overhaul and prototyping for SaaS." }
    };
        await context.PaymentPlans.AddRangeAsync(plans);
        await context.SaveChangesAsync();

        // PaymentPlanIncludes
        var includes = new List<PaymentPlanInclude>
    {
        // gig1 includes
        new PaymentPlanInclude { PaymentPlanId = plans[0].Id, Name = "Basic homepage UX" },
        new PaymentPlanInclude { PaymentPlanId = plans[1].Id, Name = "Extended product pages UX" },
        new PaymentPlanInclude { PaymentPlanId = plans[2].Id, Name = "Complete e-commerce UX with checkout" },

        // gig2 includes
        new PaymentPlanInclude { PaymentPlanId = plans[3].Id, Name = "Responsive wireframes" },
        new PaymentPlanInclude { PaymentPlanId = plans[4].Id, Name = "Wireframes + mockups" },
        new PaymentPlanInclude { PaymentPlanId = plans[5].Id, Name = "Full wireframes and mockups" },

        // gig3 includes
        new PaymentPlanInclude { PaymentPlanId = plans[6].Id, Name = "Basic UX research and personas" },
        new PaymentPlanInclude { PaymentPlanId = plans[7].Id, Name = "Detailed research and personas" },
        new PaymentPlanInclude { PaymentPlanId = plans[8].Id, Name = "Research, personas, and journey maps" },

        // gig4 includes
        new PaymentPlanInclude { PaymentPlanId = plans[9].Id, Name = "Basic UX improvements" },
        new PaymentPlanInclude { PaymentPlanId = plans[10].Id, Name = "UX optimization for multiple features" },
        new PaymentPlanInclude { PaymentPlanId = plans[11].Id, Name = "Full UX overhaul and prototyping" }
    };
        await context.PaymentPlanIncludes.AddRangeAsync(includes);
        await context.SaveChangesAsync();

        // FAQs
        await context.FAQs.AddRangeAsync(
            // gig1 FAQs
            new FAQ { GigId = gig1.Id, Question = "Do you design custom e-commerce flows?", Answer = "Yes, I tailor the UX specifically for your product and customers." },
            new FAQ { GigId = gig1.Id, Question = "Can you include mobile UX?", Answer = "Absolutely, mobile optimization is included." },

            // gig2 FAQs
            new FAQ { GigId = gig2.Id, Question = "Are wireframes responsive?", Answer = "Yes, all wireframes are designed for multiple screen sizes." },
            new FAQ { GigId = gig2.Id, Question = "Can I request changes?", Answer = "Revisions are available according to the selected plan." },

            // gig3 FAQs
            new FAQ { GigId = gig3.Id, Question = "What research methods do you use?", Answer = "I use interviews, surveys, and analytics review." },
            new FAQ { GigId = gig3.Id, Question = "Will I get user personas?", Answer = "Yes, personas are included in all plans." },

            // gig4 FAQs
            new FAQ { GigId = gig4.Id, Question = "How do you improve user retention?", Answer = "By analyzing user behavior and refining UX flows." },
            new FAQ { GigId = gig4.Id, Question = "Do you provide prototypes?", Answer = "Prototypes are included in the Premium plan." }
        );
        await context.SaveChangesAsync();

        // GigRequirements
        await context.GigRequirements.AddRangeAsync(
            new GigRequirement { GigId = gig1.Id, Question = "Please provide your e-commerce platform details and goals." },
            new GigRequirement { GigId = gig2.Id, Question = "Share any current wireframes or design ideas." },
            new GigRequirement { GigId = gig3.Id, Question = "Provide information on your users and market." },
            new GigRequirement { GigId = gig4.Id, Question = "Describe your SaaS platform and user challenges." }
        );
        await context.SaveChangesAsync();

        // Tags
        await context.Tags.AddRangeAsync(
            new Tag { GigId = gig1.Id, Name = "E-commerce UX" },
            new Tag { GigId = gig1.Id, Name = "User Experience" },
            new Tag { GigId = gig2.Id, Name = "Responsive Design" },
            new Tag { GigId = gig2.Id, Name = "Wireframes" },
            new Tag { GigId = gig3.Id, Name = "UX Research" },
            new Tag { GigId = gig3.Id, Name = "Personas" },
            new Tag { GigId = gig4.Id, Name = "SaaS UX" },
            new Tag { GigId = gig4.Id, Name = "User Retention" }
        );
        await context.SaveChangesAsync();
    }



}
