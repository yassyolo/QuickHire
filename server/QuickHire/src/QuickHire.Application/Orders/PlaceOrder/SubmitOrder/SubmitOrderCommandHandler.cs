using Microsoft.Extensions.Configuration;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Orders.Models.Form;
using QuickHire.Application.Orders.PlaceOrder.SubmitOrder;
using QuickHire.Domain.CustomOffers;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Orders.Enums;
using QuickHire.Domain.Shared.Exceptions;
using Stripe;

public class SubmitOrderCommandHandler : ICommandHandler<SubmitOrderCommand, PaymentIntentResponse>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly string _stripeSecretKey;

    public SubmitOrderCommandHandler(IRepository repository, IUserService userService, IConfiguration configuration)
    {
        _repository = repository;
        _userService = userService;
        _configuration = configuration;
        _stripeSecretKey = _configuration.GetSection("StripeOptions")["SecretKey"];
        StripeConfiguration.ApiKey = _stripeSecretKey;
    }

    public async Task<PaymentIntentResponse> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var sellerId = 0;
            if (request.GigId.HasValue)
            {
                var gig = await _repository.GetByIdAsync<Gig, int>(request.GigId.Value);
                if (gig == null)
                {
                    throw new NotFoundException(nameof(Gig), request.GigId.Value);
                }
                sellerId = gig.SellerId;
            }
            else if (request.CustomOfferId.HasValue)
            {
                var customOffer = await _repository.GetByIdAsync<CustomOffer, int>(request.CustomOfferId.Value);
                if (customOffer == null)
                {
                    throw new NotFoundException(nameof(CustomOffer), request.CustomOfferId.Value);
                }
                sellerId = customOffer.SellerId;
            }

            var buyerId = await _userService.GetBuyerIdByUserIdAsync();
            var totalPrice = await CalculatePriceAsync(request.PaymentPlanId);

            var order = new Order
            {
                OrderNumber = $"OR{DateTime.UtcNow:yyyyMMddHHmmss}",
                GigId = request.GigId,
                CustomeOfferId = request.CustomOfferId,
                SelectedPaymentPlanId = request.PaymentPlanId,
                BuyerId = buyerId,
                SellerId = sellerId,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.PendingPayment,
                TotalPrice = totalPrice,
            };

            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();

            foreach (var answer in request.Requirements)
            {
                var gigRequirementAnswer = new GigRequirementAnswer
                {
                    GigRequirementId = await _repository.GetByIdAsync<GigRequirement, int>(answer.RequirementId) is GigRequirement requirement ? requirement.Id : throw new ArgumentException("Gig requirement not found."),
                    Answer = answer.Answer,
                    OrderId = order.Id
                };
                await _repository.AddAsync(gigRequirementAnswer);
            }

            await _repository.SaveChangesAsync();

            var paymentIntentOptions = new PaymentIntentCreateOptions
            {
                Amount = (long)(totalPrice * 100),
                Currency = "usd",
                Metadata = new Dictionary<string, string>
            {
                { "orderId", order.Id.ToString() },
                { "buyerId", buyerId.ToString() },
                { "sellerId", sellerId.ToString() }
            },
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true
                }
            };

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.CreateAsync(paymentIntentOptions);

            return new PaymentIntentResponse
            {
                ClientSecret = paymentIntent.ClientSecret,
                OrderId = order.Id,
            };
        }
        catch (Exception ex)
        {
            throw new BadRequestException("Error while submitting order", ex.Message);
        }
    }

    private async Task<decimal> CalculatePriceAsync(int paymentPlanId)
    {
        var paymentPlan = await _repository.GetByIdAsync<PaymentPlan, int>(paymentPlanId);
        if (paymentPlan == null) throw new ArgumentException("Payment plan not found.");
        var taxRate = 0.15;
        var serviceFee = 5.00m;
        return paymentPlan.Price + (paymentPlan.Price * (decimal)taxRate) + serviceFee;
    }
}
