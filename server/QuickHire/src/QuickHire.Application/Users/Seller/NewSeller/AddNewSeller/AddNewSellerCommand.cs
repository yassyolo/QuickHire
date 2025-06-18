using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Seller.NewSeller.AddNewSeller;

public class AddNewSellerCommand : ICommand<Unit>
{
    [FromForm] public int IndustryId { get; init; }
    [FromForm] public string FullName { get; init; }
    [FromForm] public string Username { get; init; }
    [FromForm] public string Description { get; init; }
    [FromForm] public IFormFile? ProfilePicture { get; init; }
    [FromForm] public int[] Languages { get; init; }
    [FromForm] public List<CertificationModel> Certifications { get; init; }
    [FromForm] public List<SkillModel> Skills { get; init; }
    [FromForm] public List<EducationModel> Educations { get; init; }
}
