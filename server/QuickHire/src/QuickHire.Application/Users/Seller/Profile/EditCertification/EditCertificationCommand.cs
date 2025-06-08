using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Profile;
using System.Windows.Input;

namespace QuickHire.Application.Users.Seller.Profile.EditCertification;

public record EditCertificationCommand(CertificationModel[] Certifications) : ICommand<List<CertificationModel>>;

