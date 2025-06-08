using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.Seller.Profile.EditPortfolio;

public class EditPortfolioCommandHandler : ICommandHandler<EditPortfolioCommand, List<PortfolioModel>>
{
    private readonly IRepository _repository;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IUserService _userService;

    public EditPortfolioCommandHandler(IRepository repository, ICloudinaryService cloudinaryService, IUserService userService)
    {
        _repository = repository;
        _cloudinaryService = cloudinaryService;
        _userService = userService;
    }

    public async Task<List<PortfolioModel>> Handle(EditPortfolioCommand request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var existingPortfoliosQueryable = _repository.GetAllReadOnly<Portfolio>().Where(x => x.SellerId == sellerId);
        var existingPortfolios = await _repository.ToListAsync<Portfolio>(existingPortfoliosQueryable);
        var existingPortfolioIds = existingPortfolios.Select(x => x.Id).ToList();

        var requestIds = request.Portfolios.Select(x => x.Id).ToList();

        var portfoliosToDelete = existingPortfolios
            .Where(x => !requestIds.Contains(x.Id))
            .ToList();

        if (portfoliosToDelete.Any())
        {
            foreach (var p in portfoliosToDelete)
            {
                p.IsDeleted = true;
                p.DeletedAt = DateTime.Now;
            }
        }

        var existingPortfolioIdsToUpdate = existingPortfolios
            .Where(x => requestIds.Contains(x.Id))
            .ToList();

        foreach (var p in existingPortfolioIdsToUpdate)
        {
            var requestPortfolio = request.Portfolios.FirstOrDefault(x => x.Id == p.Id);
            if (requestPortfolio != null)
            {
                p.Title = requestPortfolio.Title;
                p.Description = requestPortfolio.Description;
                p.MainCategoryId = requestPortfolio.MainCategoryId;

                if (requestPortfolio.Image != null)
                {
                    var imagePath = _cloudinaryService.UploadFile(requestPortfolio.Image);
                    if (imagePath == null)
                    {
                        throw new BadRequestException("Image upload failed", "Image upload failed.");
                    }

                    p.ImageUrl = imagePath;
                }
            }
        }

        foreach ( var newPortfolio in request.Portfolios.Where(x => !existingPortfolioIds.Contains(x.Id)) )
        {
            var newPortfolioToAdd = new Portfolio
            {
                Title = newPortfolio.Title,
                Description = newPortfolio.Description,
                MainCategoryId = newPortfolio.MainCategoryId,
                SellerId = sellerId
            };

            if (newPortfolio.Image != null)
            {
                var imagePath = _cloudinaryService.UploadFile(newPortfolio.Image);
                if (imagePath == null)
                {
                    throw new BadRequestException("Image upload failed", "Image upload failed.");
                }

                newPortfolioToAdd.ImageUrl = imagePath;
            }

            await _repository.AddAsync(newPortfolioToAdd);
        }

        var portfoliosForSellerQueryable = _repository.GetAllReadOnly<Portfolio>()
            .Where(x => x.SellerId == sellerId);
        portfoliosForSellerQueryable = _repository.GetAllIncluding<Portfolio>(x => x.MainCategory);
        var portfoliosForSeller = await _repository.ToListAsync<Portfolio>(portfoliosForSellerQueryable);

        return portfoliosForSeller.Select(x => new PortfolioModel
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            MainCategoryId = x.MainCategoryId,
            ImageUrl = x.ImageUrl,
            MainCategoryName = x.MainCategory.Name
        }).ToList();
    }
}
