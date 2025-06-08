using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Profile;
using System.Windows.Input;

namespace QuickHire.Application.Users.Seller.Profile.EditSkill;

public record EditSkillCommand(List<SkillModel> Skills) : ICommand<List<SkillModel>>;
