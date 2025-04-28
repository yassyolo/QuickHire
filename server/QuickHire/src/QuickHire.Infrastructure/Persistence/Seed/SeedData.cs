using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuickHire.Domain.Moderation.Enums;
using QuickHire.Domain.Users;
using ApplicationUser = QuickHire.Infrastructure.Persistence.Identity.ApplicationUser;

namespace QuickHire.Infrastructure.Persistence.Seed;

public class SeedData
{
    private readonly ApplicationDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private readonly RoleManager<ApplicationUser> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
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
    public Address Address11 { get; set; } = null!;
    public Address Address12 { get; set; } = null!;
    public Address Address13 { get; set; } = null!;
    public BillingDetails BillingDetails1 { get; set; } = null!;
    public BillingDetails BillingDetails2 { get; set; } = null!;




    public SeedData(ApplicationDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public async Task SeedAsync()
    {
        await SeedApplicationUsers();
        await SeedAddresses();
        await SeedLanguages();
        await SeedBillingDetails();
    }
    

    private async Task SeedApplicationUsers()
    {
        if (!_context.Users.Any())
        {
            User1 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "johndoe",
                Email = "johndoe@example.com",
                PhoneNumber = "+1234567890",
                FullName = "John Doe",
                Description = "Experienced graphic designer with a passion for creative design and visual storytelling. I specialize in logo design, branding, and digital art.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/1.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User2 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "janesmith",
                Email = "janesmith@example.com",
                PhoneNumber = "+1234567891",
                FullName = "Jane Smith",
                Description = "I am a freelance web developer with expertise in building responsive websites and web applications using modern technologies like React and Node.js.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/2.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User3 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "alexjohnson",
                Email = "alexjohnson@example.com",
                PhoneNumber = "+1234567892",
                FullName = "Alex Johnson",
                Description = "Digital marketing strategist with 7+ years of experience in SEO, content marketing, and social media strategy to help businesses grow.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/3.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User4 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "chrislee",
                Email = "chrislee@example.com",
                PhoneNumber = "+1234567893",
                FullName = "Chris Lee",
                Description = "Professional voiceover artist specializing in commercials, narration, and e-learning projects. Let's bring your scripts to life!",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/4.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User5 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "sarahmiller",
                Email = "sarahmiller@example.com",
                PhoneNumber = "+1234567894",
                FullName = "Sarah Miller",
                Description = "I am a full-stack developer with expertise in building scalable and efficient web applications. My stack includes Angular, Java, and Spring Boot.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/25.jpgg",
                ModerationStatus = ModerationStatus.Active
            };

            User6 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "davidharris",
                Email = "davidharris@example.com",
                PhoneNumber = "+1234567895",
                FullName = "David Harris",
                Description = "A seasoned business consultant who helps organizations optimize operations and enhance performance through data-driven strategies.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/6.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User7 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "emilygreen",
                Email = "emilygreen@example.com",
                PhoneNumber = "+1234567896",
                FullName = "Emily Green",
                Description = "Expert in SEO and content marketing. I help companies improve their online presence and drive traffic to their websites through engaging content.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/4.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User8 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "michaelbrown",
                Email = "michaelbrown@example.com",
                PhoneNumber = "+1234567897",
                FullName = "Michael Brown",
                Description = "Skilled photographer with over 10 years of experience in portrait and event photography. I specialize in capturing special moments.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/25.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User9 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "sophiadavis",
                Email = "sophiadavis@example.com",
                PhoneNumber = "+1234567898",
                FullName = "Sophia Davis",
                Description = "As a professional UI/UX designer, I focus on creating intuitive user interfaces and seamless experiences for mobile and web applications.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/3.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User10 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "jameswilson",
                Email = "jameswilson@example.com",
                PhoneNumber = "+1234567899",
                FullName = "James Wilson",
                Description = "Creative copywriter with expertise in crafting persuasive and engaging copy for websites, blogs, and advertisements.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/26.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User11 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "marktwain",
                Email = "marktwain@example.com",
                PhoneNumber = "+1234567901",
                FullName = "Mark Twain",
                Description = "Looking for creative freelance work for my small business.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/7.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User12 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "emilybrown",
                Email = "emilybrown@example.com",
                PhoneNumber = "+1234567902",
                FullName = "Emily Brown",
                Description = "I need help with social media marketing for my online store.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/7.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User13 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "robertclark",
                Email = "robertclark@example.com",
                PhoneNumber = "+1234567903",
                FullName = "Robert Clark",
                Description = "I am searching for freelance help with content writing for my blog.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/8.jpg",
                ModerationStatus = ModerationStatus.Deactivated
            };

            User14 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "lindajames",
                Email = "lindajames@example.com",
                PhoneNumber = "+1234567904",
                FullName = "Linda James",
                Description = "Looking for an expert to build a custom e-commerce website for my startup.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/10.jpg",
                ModerationStatus = ModerationStatus.PendingReview
            };

            User15 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "williamjones",
                Email = "williamjones@example.com",
                PhoneNumber = "+1234567905",
                FullName = "William Jones",
                Description = "Seeking a consultant for market research and business analysis.",
                JoinedAt = DateTime.Now,
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/11.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            var users = new[] { User1, User2, User3, User4, User5, User6, User7, User8, User9, User10, User11, User12, User13, User14, User15 };
            foreach (var user in users)
            {
                var result = await _userManager.CreateAsync(user, "Password123!");
            }
        }
    }


    private async Task SeedAddresses()
    {
        if (!_context.Addresses.Any())
        {
            Address1 = new Address()
            {
                Id = 1,
                Street = "123 Main St",
                City = "New York",
                ZipCode = "10001",
                Country = "USA",
                UserId = User1.Id,
                IsBillingAddress = false
            };

            Address2 = new Address()
            {
                Id = 2,
                Street = "456 Queen St",
                City = "Toronto",
                ZipCode = "M5V 2B6",
                Country = "Canada",
                UserId = User2.Id,
                IsBillingAddress = false
            };

            Address3 = new Address()
            {
                Id = 3,
                Street = "789 Oxford Rd",
                City = "London",
                ZipCode = "SW1A 1AA",
                Country = "UK",
                UserId = User3.Id,
                IsBillingAddress = false
            };

            Address4 = new Address()
            {
                Id = 4,
                Street = "Rue de Rivoli 25",
                City = "Paris",
                ZipCode = "75001",
                Country = "France",
                UserId = User4.Id,
                IsBillingAddress = false
            };

            Address5 = new Address()
            {
                Id = 5,
                Street = "Via Roma 14",
                City = "Rome",
                ZipCode = "00185",
                Country = "Italy",
                UserId = User5.Id,
                IsBillingAddress = false
            };

            Address6 = new Address()
            {
                Id = 6,
                Street = "Unter den Linden 10",
                City = "Berlin",
                ZipCode = "10117",
                Country = "Germany",
                UserId = User6.Id,
                IsBillingAddress = false
            };

            Address7 = new Address()
            {
                Id = 7,
                Street = "Passeig de Gràcia 3",
                City = "Barcelona",
                ZipCode = "08007",
                Country = "Spain",
                UserId = User7.Id,
                IsBillingAddress = true
            };

            Address8 = new Address()
            {
                Id = 8,
                Street = "Karl Johans gate 15",
                City = "Oslo",
                ZipCode = "0154",
                Country = "Norway",
                UserId = User8.Id,
                IsBillingAddress = false
            };

            Address9 = new Address()
            {
                Id = 9,
                Street = "Södra Vägen 22",
                City = "Gothenburg",
                ZipCode = "412 54",
                Country = "Sweden",
                UserId = User9.Id,
                IsBillingAddress = true
            };

            Address10 = new Address()
            {
                Id = 10,
                Street = "Váci út 45",
                City = "Budapest",
                ZipCode = "1134",
                Country = "Hungary",
                UserId = User10.Id,
                IsBillingAddress = false
            };

            Address11 = new Address()
            {
                Id = 11,
                Street = "King Fahd Road 88",
                City = "Riyadh",
                ZipCode = "12212",
                Country = "Saudi Arabia",
                UserId = User11.Id,
                IsBillingAddress = true
            };

            Address12 = new Address()
            {
                Id = 12,
                Street = "George Street 101",
                City = "Sydney",
                ZipCode = "2000",
                Country = "Australia",
                UserId = User12.Id,
                IsBillingAddress = false
            };

            Address13 = new Address()
            {
                Id = 13,
                Street = "Orchard Road 50",
                City = "Singapore",
                ZipCode = "238880",
                Country = "Singapore",
                UserId = User13.Id,
                IsBillingAddress = true
            };

            var list = new List<Address>() { Address1, Address2, Address3, Address4, Address5, Address6,  Address7, Address8, Address9, Address10, Address11, Address12, Address13 };

            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    
    }

    private async Task SeedLanguages()
    {
        if (!_context.Languages.Any())
        {
            Language1 = new Language { Id = 1, Name = "English" };
            Language2 = new Language { Id = 2, Name = "Spanish" };
            Language3 = new Language { Id = 3, Name = "French" };
            Language4 = new Language { Id = 4, Name = "German" };
            Language5 = new Language { Id = 5, Name = "Chinese" };
            Language6 = new Language { Id = 6, Name = "Japanese" };
            Language7 = new Language { Id = 7, Name = "Korean" };
            Language8 = new Language { Id = 8, Name = "Arabic" };
            Language9 = new Language { Id = 9, Name = "Portuguese" };
            Language10 = new Language { Id = 10, Name = "Russian" };
            Language11 = new Language { Id = 11, Name = "Hindi" };
            Language12 = new Language { Id = 12, Name = "Italian" };
            Language13 = new Language { Id = 13, Name = "Dutch" };
            Language14 = new Language { Id = 14, Name = "Turkish" };
            Language15 = new Language { Id = 15, Name = "Polish" };

            _context.Languages.AddRange(Language1, Language2, Language2, Language3, Language4, 
                Language5, Language6, Language7, Language8, Language9, Language10, Language11, 
                Language12, Language13, Language14, Language15);

            await _context.SaveChangesAsync();
        }      
    }
    private async Task SeedBillingDetails()
    {
        if (!_context.BillingDetails.Any())
        {
            BillingDetails1 = new BillingDetails
            {
                Id = 1,
                FullName = "Mark Twain",
                AddressId = Address11.Id
            };
            BillingDetails2 = new BillingDetails
            {
                Id = 2,
                FullName = "Robert Clarke",
                AddressId = Address13.Id,
                CompanyName = "Robert Clark and Sons"
            };

            var list = new BillingDetails[] { BillingDetails1, BillingDetails2 };
            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }
}
