using Mapster;
using QuickHire.Application.Admin.Models.FAQ;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;

namespace QuickHire.Application.Shared.FAQ.GetFAQ;

public class GetFAQQueryHandler : IQueryHandler<GetFAQQuery, List<FAQResponseModel>>
{
    private readonly IRepository _repository;

    public GetFAQQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<FAQResponseModel>> Handle(GetFAQQuery request, CancellationToken cancellationToken)
    {
        var faqs = _repository.GetAllReadOnly<Domain.Categories.FAQ>();

        if (request.GigId != null)
        {
            faqs = faqs!.Where(x => x.GigId == request.GigId);
        }

        if (request.MainCategoryId != null)
        {
            faqs = faqs!.Where(x => x.MainCategoryId == request.MainCategoryId);
        }

        var faqsList = await _repository.ToListAsync(faqs);
        return faqsList.Adapt<List<FAQResponseModel>>();
    }
}

