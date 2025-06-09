using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Seller.Profile.EditSkill;

public class EditSkillCommandHandler : ICommandHandler<EditSkillCommand, List<SkillModel>>
{
    private readonly IUserService _userService;
    private readonly IRepository _repository;

    public EditSkillCommandHandler(IUserService userService, IRepository repository)
    {
        _userService = userService;
        _repository = repository;
    }

    public async Task<List<SkillModel>> Handle(EditSkillCommand request, CancellationToken cancellationToken)
    {
        var sellerId = await _userService.GetSellerIdByUserIdAsync();
        var existingSkillsQueryable = _repository.GetAllReadOnly<Domain.Users.Skill>().Where(x => x.SellerId == sellerId);
        var existingSkills = await _repository.ToListAsync<Domain.Users.Skill>(existingSkillsQueryable);

        var incomingSkills = request.Skills;

        var incomingIds = incomingSkills.Select(x => x.Id).ToList();
        var existingIds = existingSkills.Select(x => x.Id).ToList();

        foreach (var existingSkill in existingSkills)
        {
            var match = incomingSkills.FirstOrDefault(x => x.Id == existingSkill.Id);
            if (match != null && existingSkill.Name != match.Name)
            {
                existingSkill.Name = match.Name;
                await _repository.UpdateAsync(existingSkill);
            }
        }

        var newSkills = incomingSkills.Where(x => x.Id < 0);
        foreach (var skill in newSkills)
        {
            var newSkill = new Domain.Users.Skill()
            {
                Name = skill.Name,
                SellerId = sellerId
            };
            await _repository.AddAsync(newSkill);
        }

        var deletedSkills = existingSkills.Where(x => !incomingIds.Contains(x.Id));
        foreach (var skill in deletedSkills)
        {
            skill.IsDeleted = true;
            skill.DeletedAt = DateTime.Now;
            await _repository.UpdateAsync(skill);
        }

        await _repository.SaveChangesAsync();

        var updatedSkillsQueryable = _repository.GetAllReadOnly<Domain.Users.Skill>().Where(x => x.SellerId == sellerId && !x.IsDeleted);
        var updatedSkills = await _repository.ToListAsync<Domain.Users.Skill>(updatedSkillsQueryable);

        return updatedSkills.Select(x => new SkillModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}

