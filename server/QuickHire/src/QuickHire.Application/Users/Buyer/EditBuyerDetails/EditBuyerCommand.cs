using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Buyer.EditBuyerDetails;

public class EditBuyerCommand : ICommand<AddBuyerDetailsResponseModel>
{
    [FromForm(Name = "description")]
    public string Description { get; set; } = default!;

    [FromForm(Name = "image")]
    public IFormFile? Image { get; set; }
}
