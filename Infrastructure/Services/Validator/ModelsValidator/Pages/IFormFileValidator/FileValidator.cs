using FluentValidation;
using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;

namespace MangaApi.Infrastructure.Services.Validator.ModelsValidator.Pages.IFormFileValidator;

public class FileValidator : AbstractValidator<CollectionPagesPhotosViewModel>
{
    public FileValidator()
    {
        RuleForEach(x => x.MangasPhoto).SetValidator(new FormDataValidator());
    }
}