using FluentValidation.Results;
using MangaApi.Application.Services.Validators;
using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;
using MangaApi.Infrastructure.Services.Validator.ModelsValidator.Mangas;
using MangaApi.Infrastructure.Services.Validator.ModelsValidator.Pages;

namespace MangaApi.Infrastructure.Services.Validator;

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