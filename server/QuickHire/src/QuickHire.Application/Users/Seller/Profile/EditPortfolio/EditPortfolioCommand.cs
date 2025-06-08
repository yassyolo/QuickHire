using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Seller.Profile.EditPortfolio;

public record EditPortfolioCommand(List<EditPortfolioMode> Portfolios) : ICommand<List<PortfolioModel>>;

