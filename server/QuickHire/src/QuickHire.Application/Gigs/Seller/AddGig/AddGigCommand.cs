using MediatR;
using Microsoft.AspNetCore.Http;
using QuickHire.Application.Common.Interfaces.Abstractions;
using System.Windows.Input;

namespace QuickHire.Application.Gigs.Seller.AddGig;

public class AddGigCommand : ICommand<Unit>
{
    public string Title { get; set; } = string.Empty;
    public int SubSubCategoryId { get; set; } 
    public string Description { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    public string Plans { get; set; } = string.Empty;
    public string Metadata { get; set; } = string.Empty;
    public string Faqs { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public List<IFormFile> GalleryImages { get; set; } = new(); 
}

