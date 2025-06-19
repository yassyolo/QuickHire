using Microsoft.AspNetCore.Http;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Orders.Models.Details;
using QuickHire.Application.Users.Models.Messaging;

namespace QuickHire.Application.Orders.SendWork;
public record SendWorkCommand(int Id, int Type, string Description, IFormFile Image) : ICommand<SendWorkReturnModel>;