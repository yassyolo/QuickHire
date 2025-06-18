using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Orders.Models.Details;

namespace QuickHire.Application.Orders.SendWork;

public class SendWorkCommandHandler : ICommandHandler<SendWorkCommand, SendWorkReturnModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;
    private readonly ICloudinaryService _cloudinaryService;

    public SendWorkCommandHandler(IRepository repository, IUserService userService, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _userService = userService;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<SendWorkReturnModel> Handle(SendWorkCommand request, CancellationToken cancellationToken)
    {
        return new SendWorkReturnModel
        {
            
        };
    }
}
