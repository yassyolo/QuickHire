using Mapster;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.BillingsAndFinancialDocuments;

namespace QuickHire.Application.Users.BillingsAndFinancialDocuments.GetFinancialDocuments;

public class GetFinancialDocumentsQueryHandler : IQueryHandler<GetFinancialDocumentsQuery, IEnumerable<FinancialDocumentRowModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetFinancialDocumentsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IEnumerable<FinancialDocumentRowModel>> Handle(GetFinancialDocumentsQuery request, CancellationToken cancellationToken)
    {
        if (request.OrderId.HasValue)
        {
            var invoiceForOrderQueryable = _repository.GetAllIncluding<Domain.Orders.Invoice>(x => x.Order.Gig!).Where(x => x.OrderId == request.OrderId.Value);
            var invoiceForOrderList = await _repository.ToListAsync<Domain.Orders.Invoice>(invoiceForOrderQueryable);
            return invoiceForOrderList.Adapt<IEnumerable<FinancialDocumentRowModel>>();
        }

        var userId = request.Buyer ? await _userService.GetBuyerIdByUserIdAsync() : await _userService.GetSellerIdByUserIdAsync();

        var invoicesQueryable = _repository.GetAllIncluding<Domain.Orders.Invoice>(x => x.Order.Gig!).Where(x => request.Buyer ? x.BuyerId == userId : x.SellerId == userId);

        if (DateTime.TryParse(request.FromDate, out var fromDate))
        {
            invoicesQueryable = invoicesQueryable.Where(x => x.CreatedAt >= fromDate);
        }

        if (DateTime.TryParse(request.ToDate, out var toDate))
        {
            invoicesQueryable = invoicesQueryable.Where(x => x.CreatedAt <= toDate);
        }

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            var keyword = request.Keyword.ToLower();
            invoicesQueryable = invoicesQueryable.Where(x => x.InvoiceNumber.ToLower().Contains(keyword) || x.Order.Gig!.Title.ToLower().Contains(keyword));
        }

        var invoicesList = await _repository.ToListAsync<Domain.Orders.Invoice>(invoicesQueryable);
        invoicesList = invoicesList.OrderByDescending(x => x.CreatedAt).ToList();

        return invoicesList.Adapt<IEnumerable<FinancialDocumentRowModel>>();        
    }
}
