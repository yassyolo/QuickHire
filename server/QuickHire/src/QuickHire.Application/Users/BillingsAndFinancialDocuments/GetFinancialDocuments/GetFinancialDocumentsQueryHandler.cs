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
        /*var userId = request.Buyer ? await _userService.GetBuyerIdByUserIdAsync() : await _userService.GetSellerIdByUserIdAsync();

        var invoicesQueryable = _repository.GetAllReadOnly<Domain.Orders.Invoice>().Where(x => request.Buyer ? x.BuyerId == userId : x.SellerId == userId);
        invoicesQueryable = _repository.GetAllIncluding<Domain.Orders.Invoice>(x => x.Order.Gig);

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
            invoicesQueryable = invoicesQueryable.Where(x => x.InvoiceNumber.ToLower().Contains(keyword) ||
                x.Order.OrderNumber.ToLower().Contains(keyword) ||
                x.Order.Gig.Title.ToLower().Contains(keyword));
        }

        var invoicesList = await _repository.ToListAsync<Domain.Orders.Invoice>(invoicesQueryable);
        invoicesList = invoicesList.OrderByDescending(x => x.CreatedAt).ToList();

        return invoicesList.Adapt<IEnumerable<FinancialDocumentRowModel>>();*/

        return new List<FinancialDocumentRowModel>
        {
            new FinancialDocumentRowModel
            {
                Id = 1,
                Date = "01 Jan 2023",
                DocumentNumber = "INV-001",
                Service = "Gig Service",
                OrderNumber = "ORD-001",
                Total = "$100.00",
                PdfLink = "https://example.com/invoice1.pdf"
            },
            new FinancialDocumentRowModel
            {
                Id = 2,
                Date = "15 Jan 2023",
                DocumentNumber = "INV-002",
                Service = "Gig Service",
                OrderNumber = "ORD-002",
                Total = "$150.00",
                PdfLink = "https://example.com/invoice2.pdf"
            }
        };

    }
}
