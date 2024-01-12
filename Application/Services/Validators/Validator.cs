using FluentValidation.Results;
using MangaApi.Application.Services.Validators.ModelsValidator.Mangas;
using MangaApi.Application.Services.Validators.ModelsValidator.Pages;
using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;

namespace MangaApi.Application.Services.Validators;

public class Validator : IValidator
{
    public async Task<ValidationResult> ValidateMangasViewModel(MangasViewModel  model)
    {
        var validator = new MangaViewModelValidator();

        return await validator.ValidateAsync(model);
    }

    public async Task<ValidationResult> ValidateCollectionPagesViewModel(CollectionPageViewModel model)
    {
        var validator = new CollectionViewModelValidator();

        return await validator.ValidateAsync(model);
    }
}