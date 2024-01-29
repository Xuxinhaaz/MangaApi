using FluentValidation;
using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;
using MangaApi.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Infrastructure.Services.Validator.ModelsValidator.Pages;

public class CollectionViewModelValidator : AbstractValidator<CollectionPageViewModel>
{
    private readonly AppDbContext _context;
    public CollectionViewModelValidator(AppDbContext context)
    {
        _context = context;
        
        RuleForEach(x => x.Models).ChildRules(x =>
        {
            x.RuleFor(x => x.PageName)
                .NotEmpty().WithMessage("Page name is required.")
                .NotNull().WithMessage("Page name cannot be null.")
                .MinimumLength(8).WithMessage("Page name must be at least 8 characters.")
                .MaximumLength(80).WithMessage("Page name cannot exceed 80 characters.")
                .Must(BeUniquePage).WithMessage("Page name must be unique within the collection.");
                
        });
    }

    private bool BeUniquePage(string pageName)
    {
        return !_context.PageModels.Any(pm => pm.PageName == pageName);
    }
    
}