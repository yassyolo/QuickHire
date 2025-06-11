using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;
using QuickHire.Domain.Users;
using System.Globalization;

namespace QuickHire.Application.Users.Seller.Profile.EditCertification;

public class EditCertificationCommandHandler : ICommandHandler<EditCertificationCommand, List<CertificationModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public EditCertificationCommandHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<List<CertificationModel>> Handle(EditCertificationCommand request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var existingCertifications = _repository.GetAllReadOnly<Domain.Users.Certification>().Where(c => c.SellerId == sellerId);
        var existingCertificationsList = await _repository.ToListAsync(existingCertifications);
        foreach (var existingCertification in existingCertificationsList)
        {
            if (!request.Certifications.Any(x => x.Id == existingCertification.Id))
            {
                existingCertification.DeletedAt = DateTime.Now;
                existingCertification.IsDeleted = true;
                await _repository.UpdateAsync(existingCertification);
            }
        }

        foreach (var certification in request.Certifications)
        {
            var existingCertification = await _repository.GetByIdAsync<Domain.Users.Certification, int>(certification.Id);
            if (existingCertification != null)
            {
                existingCertification.Name = certification.Certification;
                existingCertification.Issuer = certification.Issuer;
                existingCertification.IssuedAt = DateTime.ParseExact(certification.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                await _repository.UpdateAsync(existingCertification);
            }
            else
            {
                var newCertification = new Domain.Users.Certification
                {
                    SellerId = sellerId,
                    Name = certification.Certification,
                    Issuer = certification.Issuer,
                    IssuedAt = DateTime.Parse(certification.Date),
                };
                await _repository.AddAsync(newCertification);
            }
        }

        await _repository.SaveChangesAsync();

        var updatedCertifications = _repository.GetAllReadOnly<Domain.Users.Certification>().Where(c => c.SellerId == sellerId && !c.IsDeleted);
        var updatedCertificationsList = await _repository.ToListAsync(updatedCertifications);

        return updatedCertificationsList.Select(cert => new CertificationModel
        {
            Id = cert.Id,
            Certification = cert.Name,
            Issuer = cert.Issuer,
            Date = cert.IssuedAt.ToString("yyyy-MM-dd")
        }).ToList();
    }
}
