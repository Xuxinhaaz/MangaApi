using FluentValidation;
using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Infrastructure.Services.Validator.ModelsValidator.Pages.IFormFileValidator;

namespace MangaApi.Infrastructure.Services.Validator.ModelsValidator.Mangas;

public class MangaViewModelValidator : AbstractValidator<MangasViewModel>
{
    public MangaViewModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .NotNull().WithMessage("Title cannot be null.")
            .MinimumLength(5).WithMessage("Title must be at least 5 characters.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Author is required.")
            .NotNull().WithMessage("Author cannot be null.")
            .MinimumLength(5).WithMessage("Author must be at least 5 characters.")
            .MaximumLength(30).WithMessage("Author cannot exceed 30 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .NotNull().WithMessage("Description cannot be null.")
            .MinimumLength(50).WithMessage("Description must be at least 50 characters.")
            .MaximumLength(400).WithMessage("Description cannot exceed 400 characters.");

        RuleFor(x => x.Group)
            .NotEmpty().WithMessage("Group is required.")
            .NotNull().WithMessage("Group cannot be null.")
            .MinimumLength(5).WithMessage("Group must be at least 5 characters.")
            .MaximumLength(50).WithMessage("Group cannot exceed 50 characters.");

        RuleFor(x => x.Translation)
            .NotEmpty().WithMessage("Translation is required.")
            .NotNull().WithMessage("Translation cannot be null.")
            .MinimumLength(5).WithMessage("Translation must be at least 5 characters.")
            .MaximumLength(20).WithMessage("Translation cannot exceed 20 characters.");

    }    
}