using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Factories.Notification;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Orders.Models.Invoice;
using QuickHire.Application.Users.Models.Messaging;
using QuickHire.Domain.Messaging;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Infrastructure.Helpers;

namespace QuickHire.Application.Orders.PlaceOrder.SuccessfulPayment;

public class MarkOrderPaidCommandHandler : ICommandHandler<MarkOrderPaidCommand, Unit>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;
    private readonly INotificationService _notificationService;
    private readonly IPdfHelper _pdfHelper;
    private readonly ICloudinaryService _cloudinaryService;

    public MarkOrderPaidCommandHandler(IRepository repository, IUserService userService, INotificationService notificationService, IPdfHelper pdfHelper, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _userService = userService;
        _notificationService = notificationService;
        _pdfHelper = pdfHelper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Unit> Handle(MarkOrderPaidCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ordersQueryable = _repository.GetAllIncluding<Domain.Orders.Order>(x => x.Gig).Where(x => x.Id == request.OrderId);
            var order = await _repository.FirstOrDefaultAsync(ordersQueryable);
            if (order == null)
            {
                throw new NotFoundException(nameof(Domain.Orders.Order), request.OrderId);
            }

            order.Status = Domain.Orders.Enums.OrderStatus.InProgress;

            if (order.CustomeOfferId.HasValue)
            {
                var customOffer = await _repository.GetByIdAsync<Domain.CustomOffers.CustomOffer, int>(order.CustomeOfferId.Value);
                customOffer.Status = Domain.CustomOffers.Enums.CustomOfferStatus.Accepted;
                await _repository.UpdateAsync(customOffer);

                if(customOffer.ProjectBriefId.HasValue)
                {
                    var projectBrief = await _repository.GetByIdAsync<Domain.ProjectBriefs.ProjectBrief, int>(customOffer.ProjectBriefId.Value);
                    projectBrief.Status = Domain.ProjectBriefs.Enums.ProjectBriefStatus.OrderPlaced;
                    await _repository.UpdateAsync(projectBrief);
                }
            }

            await _repository.UpdateAsync(order);

            var buyerUserId = await _userService.GetUserIdByBuyerIdAsync(order.BuyerId);
            var sellerUserId = await _userService.GetUserIdBySellerIdAsync(order.SellerId);

            await _notificationService.MakeNotification(order.SellerId, NotificationRecipientType.Seller, Domain.Users.Enums.NotificationType.NewOrder, new Dictionary<string, string> { { "OrderNumber", order.OrderNumber } });
            await _notificationService.MakeNotification(order.BuyerId, NotificationRecipientType.Buyer, Domain.Users.Enums.NotificationType.OrderPlaced, new Dictionary<string, string> { { "OrderNumber", order.OrderNumber } });

            var newConversation = new Conversation
            {
                ParticipantAId = sellerUserId,
                ParticipantAMode = "seller",
                ParticipantBId = buyerUserId,
                ParticipantBMode = "buyer",
                CreatedAt = DateTime.Now,
                LastMessageAt = DateTime.Now,
                OrderId = order.Id
            };

            await _repository.AddAsync(newConversation);
            await _repository.SaveChangesAsync();

            order.ConversationId = newConversation.Id;
            await _repository.UpdateAsync(order);

            var subtotal = order.TotalPrice;
            var taxRate = 0.15m;
            var serviceFee = 5m;

            var tax = subtotal * taxRate;
            var totalAmount = subtotal + tax + serviceFee;

            var invoice = new Domain.Orders.Invoice
            {
                OrderId = order.Id,
                CreatedAt = DateTime.Now,
                InvoiceNumber = "INV-" + DateTime.UtcNow.ToString("yyyyMMddHHmmss"),
                BuyerId = order.BuyerId,
                SellerId = order.SellerId,
                Subtotal = subtotal,
                Tax = tax,
                ServiceFee = serviceFee,
                TotalAmount = totalAmount
            };

            var buyerForInvoice = await _userService.GetBuyerForInvoiceAsync(buyerUserId);

            var serviceFeeItem = new InvoiceItemModel
            {
                ItemName = "Service Fee",
                Quantity = 1,
                TotalAmount = serviceFee.ToString("F2")
            };

            var orderItem = new InvoiceItemModel
            {
                ItemName = order.Gig.Title,
                Quantity = 1,
                TotalAmount = tax.ToString("F2")
            };

            await _repository.AddAsync(invoice);
            await _repository.SaveChangesAsync();
            var invoiceModel = new InvoiceModel
            {
                InvoiceNumber = invoice.InvoiceNumber,
                BuyerName = buyerForInvoice.name,
                BuyerAddress = buyerForInvoice.address,
                BuyerCompanyName = buyerForInvoice.companyName,
                CreatedAt = invoice.CreatedAt.ToString("dd MMM, yyyy"),
                OrderNumber = order.OrderNumber,
                Items = new List<InvoiceItemModel> { serviceFeeItem, orderItem },
                TotalAmount = totalAmount.ToString("F2"),
                SubTotal = subtotal.ToString("F2"),
                Tax = tax.ToString("F2")
            };

            var pdfLink = _pdfHelper.GeneratePdfFromHtml(invoiceModel);
            if (pdfLink == null)
            {
                throw new BadRequestException("Failed to generate PDF.", "");
            }

            if (pdfLink != null)
            {
                var uploadResult = _cloudinaryService.UploadFile(pdfLink);
                if (uploadResult == null)
                {
                    throw new BadRequestException("Failed to upload PDF to cloud storage.", "");
                }
                invoice.SourceUrl = uploadResult;
            }
            await _repository.UpdateAsync(invoice);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
        catch (Exception ex)
        {
            throw new BadRequestException("Error while marking order as payed", ex.Message);
        }
    }
}
