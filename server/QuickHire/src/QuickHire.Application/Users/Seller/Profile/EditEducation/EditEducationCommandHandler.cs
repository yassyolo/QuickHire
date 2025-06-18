using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Seller.Profile.EditEducation;

public class EditEducationCommandHandler : ICommandHandler<EditEducationCommand, List<EducationModel>>
{
    private readonly IUserService _userService;
    private readonly IRepository _repository;

    public EditEducationCommandHandler(IUserService userService, IRepository repository)
    {
        _userService = userService;
        _repository = repository;
    }

    public async Task<List<EducationModel>> Handle(EditEducationCommand request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();

        var existingEducations = _repository.GetAllReadOnly<Domain.Users.Education>().Where(e => e.SellerId == sellerId);
        var existingEducationsList = await _repository.ToListAsync(existingEducations);
        foreach(var education in existingEducationsList)
        {
            if (!request.Educations.Any(x => x.Id == education.Id))
            {
                education.IsDeleted = true;
                education.DeletedAt = DateTime.Now;
                await _repository.UpdateAsync(education);
            }
        }

        foreach (var education in request.Educations)
        {
            var existingEducation = await _repository.GetByIdAsync<Domain.Users.Education, int>(education.Id);
            if(existingEducation != null)
            {
                existingEducation.Institution = education.Institution;
                existingEducation.Degree = education.Degree;
                existingEducation.GraduationYear = int.Parse(education.EndYear);
                existingEducation.Major = education.Major;
                await _repository.UpdateAsync(existingEducation);
            }
            else
            {
                var newEducation = new Domain.Users.Education
                {
                    Institution = education.Institution,
                    Degree = education.Degree,
                    GraduationYear = int.Parse(education.EndYear),
                    Major = education.Major,
                    SellerId = sellerId
                };
                await _repository.AddAsync(newEducation);
            }

        }
        await _repository.SaveChangesAsync();

        var updatedEducations = _repository.GetAllReadOnly<Domain.Users.Education>().Where(e => e.SellerId == sellerId && !e.IsDeleted);
        var updatedEducationsList = await _repository.ToListAsync(updatedEducations);

        return updatedEducationsList.Select(e => new EducationModel
        {
            Id = e.Id,
            Institution = e.Institution,
            Degree = e.Degree,
            Major = e.Major,
            EndYear = e.GraduationYear.ToString()
        }).ToList();
    }
}
