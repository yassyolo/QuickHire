using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using System.Windows.Input;

namespace QuickHire.Application.Users.Authentication.UpdateProfile;

public record UpdateProfileCommand(string? FullName, string? Email, string? Username, int? CountryId, string? City, string? Street, string? ZipCode) : ICommand<Unit>;

