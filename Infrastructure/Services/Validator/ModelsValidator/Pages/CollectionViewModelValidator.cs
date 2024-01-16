using FluentValidation;
using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;

namespace MangaApi.Infrastructure.Services.Validator.ModelsValidator.Pages;

public class CollectionViewModelValidator : AbstractValidator<CollectionPageViewModel>
{
    public CollectionViewModelValidator()
    {
        RuleForEach(x => x.Models).ChildRules(x =>
        {
            x.RuleFor(x => x.PageName)
                .NotEmpty().WithMessage("Page name is required.")
                .NotNull().WithMessage("Page name cannot be null.")
                .MinimumLength(8).WithMessage("Page name must be at least 8 characters.")
                .MaximumLength(80).WithMessage("Page name cannot exceed 80 characters.");
            
        });
    }
    
}