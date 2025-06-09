using Microsoft.AspNetCore.Http;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Buyer.AddBuyerDetails;

public record AddBuyerDetailsCommand(string Description, IFormFile Image) : ICommand<AddBuyerDetailsResponseModel>;

