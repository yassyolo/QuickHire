using Microsoft.AspNetCore.Identity;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Moderation.Enums;
using QuickHire.Domain.Users;
using QuickHire.Domain.Users.Enums;
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
    public UserLanguage UserLanguage1 { get; set; } = null!;
    public UserLanguage UserLanguage2 { get; set; } = null!;
    public UserLanguage UserLanguage3 { get; set; } = null!;
    public UserLanguage UserLanguage4 { get; set; } = null!;
    public UserLanguage UserLanguage5 { get; set; } = null!;
    public UserLanguage UserLanguage6 { get; set; } = null!;
    public ApplicationUser User1 { get; set; } = null!;
    public ApplicationUser User2 { get; set; } = null!;
    public ApplicationUser User3 { get; set; } = null!;
    public ApplicationUser User4 { get; set; } = null!;
    public Buyer Buyer1 { get; set; } = null!;  
    public Seller Seller1 { get; set; } = null!;
    public Seller Seller2 { get; set; } = null!;
    public Seller Seller3 { get; set; } = null!;
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
        await SeedUserLanguages();
        await SeedLanguages();
        await SeedBillingDetails();
        await SeedBuyers();
        await SeedSellers();
        await SeedCertifications();
        await SeedEducations();
        await SeedSkills();
        await SeedPortfolios();
        await SeedMainCategories();
    }
    

    private async Task SeedApplicationUsers()
    {
        if (!_context.Users.Any())
        {
            User1 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "jhonehenry333",
                Email = "jhonehenry333@example.com",
                PhoneNumber = "0890320456",
                FullName = "Jhon H Rifat",
                Description = "I am Rifat, a professional graphic designer ,HIGH QUALITY WORK, FAST DELIVERY and BEST PRICE this is what I can assure you. Your Satisfaction and Quality of my work is my main priority. Top Services: ✔️ Logo Design ✔️ Logo redesign ✔️ Logo Update to modern and professional ✔️ T-shirt design ✔️ Branding and stationary design ✔️ Business card letterhead ✔️ Flyer, Brochure",
                JoinedAt = DateTime.Now.AddYears(-1),
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/1.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User2 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "nextui",
                Email = "nextui@example.com",
                PhoneNumber = "0890370826",
                FullName = "Jahid Hasan",
                Description = "Hello Fiverr Community! At Nextui, we are a team of passionate designers committed to crafting exceptional user experiences and delivering top-quality digital products. Whether you need a website, dashboard, mobile app UI/UX design, branding, or identity design, we've got you covered. Our mission is to provide outstanding design solutions that will captivate your audience, engage your users, and drive your business forward. Reach out to us today, and let's create something amazing together! 😎",
                JoinedAt = DateTime.Now.AddYears(-2),
                ProfileImageUrl = "https://randomuser.me/api/portraits/women/2.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User3 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "matiasfontenla",
                Email = "matiasfontenla@example.com",
                PhoneNumber = "08903470327",
                FullName = "Matias Fontenla",
                Description = "I am a professional UX/UI designer and developer with over 3 years of experience in the field. I have previously worked for US & AR-based companies and are now offering our services on Fiverr. I am highly skilled in various design tools such as Figma, Adobe XD, and Sketch, as well as multiple programming languages. I have been working with companies as a freelancer since 2021. Please feel free to contact us to collaborate on creating something amazing together. Thank you!",
                JoinedAt = DateTime.Now.AddDays(-70),
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/3.jpg",
                ModerationStatus = ModerationStatus.Active
            };

            User4 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "wajid_uiux",
                Email = "wajid_uiux@example.com",
                PhoneNumber = "+1234567893",
                FullName = "Wajid K",
                Description = "Hi, Welcome to my seller profile. I am Wajid Khan Ui Ux designer with 2+ years of experience. i have got\r\nexpertise in Mobile App design, Website Design, Landing page design and Webb App design. in my work\r\nincluding Prototyping, Responsive Design, Usability Testing.",
                JoinedAt = DateTime.Now.AddMonths(-8),
                ProfileImageUrl = "https://randomuser.me/api/portraits/men/4.jpg",
                ModerationStatus = ModerationStatus.Active
            };


            var users = new[] { User1, User2, User3, User4};
            foreach (var user in users)
            {
                var result = await _userManager.CreateAsync(user, "Password123!");
            }
        }
    }

    private async Task SeedBuyers()
    {
        if (!_context.Buyers.Any())
        {
            Buyer1 = new Buyer
            {
                Id = 1,
                UserId = User1.Id,
                BillingDetailsId = BillingDetails1.Id,
            };

            var list = new List<Buyer>() { Buyer1 };
            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedSellers()
    {
        if (!_context.Sellers.Any())
        {
            Seller1 = new Seller
            {
                Id = 1,
                UserId = User2.Id,
            };

            Seller2 = new Seller
            {
                Id = 2,
                UserId = User3.Id,
            };

            Seller3 = new Seller
            {
                Id = 3,
                UserId = User4.Id,
            };

            var list = new List<Seller>() { Seller1, Seller2, Seller3 };
            _context.AddRange(list);
            await _context.SaveChangesAsync();
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
                IsBillingAddress = true
            };


            var list = new List<Address>() { Address1, Address2, Address3, Address4  };

            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    
    }
    private async Task SeedCertifications()
    {
        if (!_context.Certifications.Any())
        {
            Certification1 = new Certification
            {
                Id = 1,
                Name = "Certified Graphic Designer",
                Issuer = "Adobe",
                IssuedAt = DateTime.Now.AddYears(-2),
                SellerId = Seller1.Id
            };

            Certification2 = new Certification
            {
                Id = 2,
                Name = "Creative IT Institute Diploma In graphics Designing",
                Issuer = "Creative IT",
                IssuedAt = DateTime.Now.AddYears(-5),
                SellerId = Seller1.Id
            };

            Certification3 = new Certification
            {
                Id = 3,
                Name = "Creative IT Institute Diploma In graphics Designing",
                Issuer = "Creative IT",
                IssuedAt = DateTime.Now.AddYears(-3),
                SellerId = Seller2.Id
            };

            Certification4 = new Certification
            {
                Id = 4,
                Name = "Ostad Ostad Mobile App UI Design",
                Issuer = "Ostad Ostad",
                IssuedAt = DateTime.Now.AddYears(-1),
                SellerId = Seller2.Id
            };

            Certification5 = new Certification
            {
                Id = 5,
                Name = "Codo a codo Fullstack developer with node js",
                Issuer = "Codo a codo",
                IssuedAt = DateTime.Now.AddYears(-2),
                SellerId = Seller3.Id
            };

            Certification6 = new Certification
            {
                Id = 6,
                Name = "Codo a codo ux/ui designer",
                Issuer = "Codo a codo",
                IssuedAt = DateTime.Now.AddYears(-3),
                SellerId = Seller3.Id
            };

            var list = new List<Certification>() { Certification1, Certification2, Certification3, Certification4, Certification5, Certification6 };
            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedSkills()
    {
        if (!_context.Skills.Any())
        {
            Skill1 = new Skill { Id = 1, Name = "Graphic designer", Level = SkillLevel.Advanced };
            Skill2 = new Skill { Id = 2, Name = "UI/UX designer", Level = SkillLevel.Advanced, SellerId = Seller1.Id };
            Skill3 = new Skill { Id = 3, Name = "Creative designer", Level = SkillLevel.Advanced, SellerId = Seller1.Id };
            Skill4 = new Skill { Id = 4, Name = "Adobe Photoshop Expert", Level = SkillLevel.Advanced, SellerId = Seller1.Id };
            Skill5 = new Skill { Id = 5, Name = "Adobe Illustration Expert", Level = SkillLevel.Advanced, SellerId = Seller1.Id };
            Skill6 = new Skill { Id = 6, Name = "UI designer", Level = SkillLevel.Advanced, SellerId = Seller2.Id };
            Skill7 = new Skill { Id = 7, Name = "Adobe XD expert", Level = SkillLevel.Advanced, SellerId = Seller2.Id };
            Skill8 = new Skill { Id = 8, Name = "Figma designer", Level = SkillLevel.Advanced, SellerId = Seller2.Id };
            Skill9 = new Skill { Id = 9, Name = "Brand identity designer", Level = SkillLevel.Advanced, SellerId = Seller2.Id };
            Skill10 = new Skill { Id = 10, Name = "UX designer", Level = SkillLevel.Advanced, SellerId = Seller3.Id };
            Skill11 = new Skill { Id = 11, Name = "Wordpress", Level = SkillLevel.Advanced, SellerId = Seller3.Id };
            Skill12 = new Skill { Id = 12, Name = "UX writer", Level = SkillLevel.Advanced, SellerId = Seller3.Id };
            Skill13 = new Skill { Id = 13, Name = "Mobile UX writer", Level = SkillLevel.Advanced, SellerId = Seller3.Id };
            Skill14 = new Skill { Id = 14, Name = "UI designer", Level = SkillLevel.Advanced, SellerId = Seller3.Id };
            Skill15 = new Skill { Id = 15, Name = "WEbsite UX designer", Level = SkillLevel.Advanced, SellerId = Seller3.Id };

            var list = new List<Skill>() { Skill1, Skill2, Skill3, Skill4, Skill5, Skill6, Skill7, Skill8, Skill9, Skill10, Skill11, Skill12, Skill13, Skill14, Skill15 };
            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }  
    
    private async Task SeedPortfolios() { 
        if (!_context.Portfolios.Any())
        {
            Portfolio1 = new Portfolio
            {
                Id = 1,
                Title = "Clean Cut Lawn services",
                Description = "Clean cut is a lawn services company based on USA. i did the branding of this company and this is the final logo\r\nClean Cut Lawn services",
                ImageUrl = "https://media.istockphoto.com/id/1394774878/photo/green-earth-recycle-concept-earth-day-surrounded-by-globes-trees-leaf-and-plant-on-a-brown.jpg?b=1&s=612x612&w=0&k=20&c=cE0-hpJI_6vuEwgaIXHWmT7lRpEsLxoTnCT191vHgkQ=",
                SellerId = Seller1.Id
            };

            Portfolio2 = new Portfolio
            {
                Id = 2,
                Title = "Eastcoast paint and refinishing",
                Description = "Eastcoast paint and refinishing llc\r\nAs a premier painting contractor, we are committed to delivering customer service that is second to none. We offer a wide range of services to meet your residential and commercial painting needs. Big job or small, we have you covered.",
                ImageUrl = "https://media.istockphoto.com/id/1149597075/photo/graphic-designer-development-process-drawing-sketch-design-creative-ideas-draft-logo-product.jpg?b=1&s=612x612&w=0&k=20&c=P1IkfkPH7vMMqjhIJdrJ-5MaGkBqt-w0xJPtCuBzANE=" ,
                SellerId = Seller1.Id
            };

            Portfolio3 = new Portfolio
            {
                Id = 3,
                Title = "Logo Design",
                Description = "Essential Service Group is an well known company in USA, they provide different kind of home services.\r\nESG logo",
                ImageUrl = "https://fiverr-res.cloudinary.com/image/upload/f_auto,q_auto/v1/attachments/project_item/attachment/7213e406838acb8f269dd5b7e1b42bfb-1711736427941/ESG-logo.jpg",
                SellerId = Seller1.Id
            };

            Portfolio4 = new Portfolio
            {
                Id = 4,
                Title = "creative and unique logo project",
                Description = "creative and unique logo project\r\n#real estate logo\r\nmodern logo\r\n#unique logo\r\n#business logo\r\n#logo design",
                ImageUrl = "https://fiverr-res.cloudinary.com/image/upload/f_auto,q_auto/v1/attachments/project_item/attachment/e89be40dd7c5468cf9d426ad115dd210-1711183376534/3d-unique-logo-design-for-company-with-unlimited-revsions.jpg",
                SellerId = Seller1.Id
            };

            Portfolio5 = new Portfolio
            {
                Id = 5,
                Title = "Jewelry Landing Page",
                Description = "I’m delighted to present my new jewelry landing page! It’s designed to be elegant and captivating, showcasing exquisite collections in a visually stunning way. The layout is refined and easy to navigate, allowing you to browse through products, learn about craftsmanship, and make purchases effortlessly. If you have any questions or want to know more about the jewelry, feel free to reach out! I’d be thrilled to share more about these beautiful pieces with you.",
                ImageUrl = "https://fiverr-res.cloudinary.com/image/upload/f_auto,q_auto/v1/attachments/project_item/attachment/94882e7ae51bad1a1b32178463446e22-1720935490873/Jewelery%20Landing%20Page-1.png",
                SellerId = Seller2.Id
            };

            Portfolio6 = new Portfolio
            {
                Id = 6,
                Title = "Gardenlife - redesign",
                Description = "The project focused on redesigning the GardenLife website using Figma to enhance user experience and modernize the site's visual appeal. The objective was to create a fresh and engaging online presence for GardenLife, a company specializing in gardening products and advice.",
                ImageUrl = "https://fiverr-res.cloudinary.com/image/upload/f_auto,q_auto,t_portfolio_project_card/v1/attachments/project_item/attachment/c96819b68a55601b691d68d14eccb27d-1724171461929/HOME_DESKTOP.png",
                SellerId = Seller3.Id
            };

            var list = new List<Portfolio>() { Portfolio1, Portfolio2, Portfolio3 };
            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }
    private async Task SeedEducations()
    {
        if(!_context.Educations.Any())
        {
            Education1 = new Education
            {
                Id = 1,
                Degree = EducationDegree.Masters,
                Institution = "Comilla Polytechnic Institute",
                StartDate = DateTime.Now.AddYears(-4),
                EndDate = DateTime.Now.AddYears(-2),
                SellerId = Seller1.Id
            };

            Education2 = new Education
            {
                Id = 2,
                Degree = EducationDegree.Bachelors,
                Institution = "United International University",
                StartDate = DateTime.Now.AddYears(-6),
                EndDate = DateTime.Now.AddYears(-3),
                SellerId = Seller2.Id
            };

            Education3 = new Education
            {
                Id = 3,
                Degree = EducationDegree.PhD,
                Institution = "Universidad Nacional de Quilmes",
                StartDate = DateTime.Now.AddYears(-5),
                EndDate = DateTime.Now.AddYears(-3),
                SellerId = Seller3.Id
            };

            var list = new List<Education>() { Education1, Education2, Education3 };
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

    private async Task SeedUserLanguages()
    {
        if (!_context.UserLanguages.Any())
        {
            UserLanguage1 = new UserLanguage
            {
                UserId = User1.Id,
                LanguageId = Language1.Id,
                Proficiency = ProficiencyLevel.Fluent
            };
            UserLanguage2 = new UserLanguage
            {
                UserId = User1.Id,
                LanguageId = Language2.Id,
                Proficiency = ProficiencyLevel.Basic
            };
            UserLanguage3 = new UserLanguage
            {
                UserId = User2.Id,
                LanguageId = Language1.Id,
                Proficiency = ProficiencyLevel.Fluent
            };
            UserLanguage4 = new UserLanguage
            {
                UserId = User3.Id,
                LanguageId = Language1.Id,
                Proficiency = ProficiencyLevel.Fluent
            };
            UserLanguage5 = new UserLanguage
            {
                UserId = User4.Id,
                LanguageId = Language3.Id,
                Proficiency = ProficiencyLevel.Basic
            };
            UserLanguage6 = new UserLanguage
            {
                UserId = User1.Id,
                LanguageId = Language4.Id,
                Proficiency = ProficiencyLevel.Basic
            };
            var list = new List<UserLanguage>
            {
                UserLanguage1,
                UserLanguage2,
                UserLanguage3,
                UserLanguage4,
                UserLanguage5,
                UserLanguage6
            };

            _context.UserLanguages.AddRange(list);
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
                FullName = "Wajid K",
                CompanyName = "K Enterprises",
                AddressId = Address4.Id
            };

            var list = new BillingDetails[] { BillingDetails1 };
            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }

    private async Task SeedMainCategories()
    {
        if (!_context.MainCategories.Any())
        {
            MainCategory1 = new MainCategory
            {
                Id = 1,
                Name = "Graphics & Design",
                Description = "Bring ideas to life with stunning visuals, logos, and pixel-perfect designs!"
            };

            MainCategory2 = new MainCategory
            {
                Id = 2,
                Name = "Programming & Tech",
                Description = "Code your dreams—from websites to mobile apps, tech wizards live here!"
            };

            MainCategory3 = new MainCategory
            {
                Id = 3,
                Name = "Digital Marketing",
                Description = "Boost your brand with smart SEO, viral content, and marketing magic!"
            };

           MainCategory4 = new MainCategory
            {
                Id = 4,
                Name = "Writing & Translation",
                Description = "Words that wow—get crisp copy, catchy slogans, or fluent translations!"
            };

            MainCategory5 = new MainCategory
            {
                Id = 5,
                Name = "Video & Animation",
                Description = "From explainer videos to cool animations—bring your stories to life!"
            };

            MainCategory6 = new MainCategory
            {
                Id = 6,
                Name = "Music & Audio",
                Description = "Catchy jingles, voiceovers, and killer beats all in one place!"
            };

            MainCategory7 = new MainCategory
            {
                Id = 7,
                Name = "Business",
                Description = "From business plans to virtual assistants—get expert help to grow smart!"
            };

            MainCategory8 = new MainCategory
            {
                Id = 8,
                Name = "Lifestyle",
                Description = "Astrology, fitness, gaming tips—you name it, we’ve got the life hacks!"
            };

            MainCategory9 = new MainCategory
            {
                Id = 9,
                Name = "Data",
                Description = "Crunch numbers, visualize insights, or clean messy data like a pro!"
            };

            MainCategory10 = new MainCategory
            {
                Id = 10,
                Name = "AI Services",
                Description = "Supercharge your ideas with AI—chatbots, image gen, voice clones & more!"
            };


            var list = new List<MainCategory>() { MainCategory1, MainCategory2, MainCategory3, MainCategory4, MainCategory5, MainCategory6, MainCategory7, MainCategory8, MainCategory9, MainCategory10 };
            _context.AddRange(list);
            await _context.SaveChangesAsync();
        }
    }
}
