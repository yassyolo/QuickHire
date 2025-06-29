using Microsoft.Extensions.Logging;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Orders.Models.Details;
using QuickHire.Application.Users.Models.Messaging;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Orders.SendWork;

public class SendWorkCommandHandler : ICommandHandler<SendWorkCommand, SendWorkReturnModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly INotificationService _notificationService;

    public SendWorkCommandHandler(IRepository repository, IUserService userService, ICloudinaryService cloudinaryService, INotificationService notificationService)
    {
        _repository = repository;
        _userService = userService;
        _cloudinaryService = cloudinaryService;
        _notificationService = notificationService;
    }

    public async Task<SendWorkReturnModel> Handle(SendWorkCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync<QuickHire.Domain.Orders.Order, int>(request.Id);
        if (order == null)
        {
            throw new NotFoundException(nameof(QuickHire.Domain.Orders.Order), request.Id);
        }

        var returnModel = new SendWorkReturnModel();
        if (request.Type == 1)
        {       
            var revisionQueryable = _repository.GetAllIncluding<QuickHire.Domain.Orders.Revision>().Where(x => x.OrderId == request.Id );
            var revisions = await _repository.ToListAsync<QuickHire.Domain.Orders.Revision>(revisionQueryable);
            var revsionsCoutn = revisions.Count();
            var newRevision = new QuickHire.Domain.Orders.Revision
            {
                Description = request.Description,
                OrderId = request.Id,
                CreatedAt = DateTime.UtcNow,
                Status = QuickHire.Domain.Orders.Enums.RevisionStatus.Pending,
            };

            if (request.Image != null)
            {                
                    var uploadResult = _cloudinaryService.UploadFile(request.Image);
                    if (uploadResult == null)
                    {
                        throw new Exception("Failed to upload image to cloud storage.");
                    }
                    newRevision.AttachmentUrls.Add(uploadResult);
            }

            await _repository.AddAsync(newRevision);
            await _repository.SaveChangesAsync();

            returnModel.Revision = new RevisionPayloadModel
            {
                Attachment = newRevision.AttachmentUrls.FirstOrDefault() ?? "",
                Description = newRevision.Description,
                RevisionNumber = revsionsCoutn + 1,
                RevisionId = newRevision.Id,
            };

            await _notificationService.MakeNotification(order.BuyerId, NotificationRecipientType.Buyer, Domain.Users.Enums.NotificationType.OrderStatusUpdate, new Dictionary<string, string> { { "OrderNumber", order.OrderNumber } });

        }
        else if(request.Type == 2)
        {
            var delivery = new QuickHire.Domain.Orders.Delivery
            {
                Description = request.Description,
                OrderId = request.Id,
                CreatedAt = DateTime.UtcNow,
            };
            if (request.Image != null)
            {
                    var uploadResult = _cloudinaryService.UploadFile(request.Image);
                    if (uploadResult == null)
                    {
                        throw new Exception("Failed to upload image to cloud storage.");
                    }
                    delivery.AttachmentUrls.Add(uploadResult);
            }

            await _repository.AddAsync(delivery);
            await _repository.SaveChangesAsync();

            order.Status = QuickHire.Domain.Orders.Enums.OrderStatus.Delivered;
            if(order.CustomeOfferId.HasValue && order.CustomOffer.ProjectBriefId.HasValue)
            {
                order.CustomOffer.ProjectBrief.Status = Domain.ProjectBriefs.Enums.ProjectBriefStatus.Delivered;
            }

            await _repository.UpdateAsync(order);

            returnModel.Delivery = new DeliveryPayloadModel
            {
                Attachment = delivery.AttachmentUrls.FirstOrDefault() ?? "",
                Description = delivery.Description,
                SourceFileUrl = delivery.SourceFileUrl
            };

            await _notificationService.MakeNotification(order.BuyerId, NotificationRecipientType.Buyer, Domain.Users.Enums.NotificationType.OrderDelivered, new Dictionary<string, string> { { "OrderNumber", order.OrderNumber } });

        }
        return returnModel;
    }
}
