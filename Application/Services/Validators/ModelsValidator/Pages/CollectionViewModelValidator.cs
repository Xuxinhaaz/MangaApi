using FluentValidation;
using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;

namespace MangaApi.Application.Services.Validators.ModelsValidator.Pages;

public class CollectionViewModelValidator : AbstractValidator<CollectionPageViewModel>
{
    public CollectionViewModelValidator()
    {
        RuleForEach(x => x.Models).ChildRules(x =>
        {
            x.RuleFor(x => x.PageNumber)
                .NotEmpty().WithMessage("Page number is required.")
                .NotNull().WithMessage("Page number cannot be null.");
    
            x.RuleFor(x => x.PageName)
                .NotEmpty().WithMessage("Page name is required.")
                .NotNull().WithMessage("Page name cannot be null.")
                .MinimumLength(8).WithMessage("Page name must be at least 8 characters.")
                .MaximumLength(80).WithMessage("Page name cannot exceed 80 characters.");
    
            x.RuleFor(x => x.MangaUrl) 
                .NotEmpty().WithMessage("Manga URL is required.")
                .NotNull().WithMessage("Manga URL cannot be null.")
                .MinimumLength(8).WithMessage("Manga URL must be at least 8 characters.")
                .MaximumLength(80).WithMessage("Manga URL cannot exceed 80 characters.");
        });
    }
    
}