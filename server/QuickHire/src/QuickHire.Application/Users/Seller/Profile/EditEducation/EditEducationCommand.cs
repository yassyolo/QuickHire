using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Seller.Profile.EditEducation;

public record EditEducationCommand(EducationModel[] Educations) : ICommand<List<EducationModel>>;

