using FluentValidation.Results;
using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;

namespace MangaApi.Application.Common.Services.Validators;

public interface IValidator
{
    Task<ValidationResult> ValidateMangasViewModel(MangasViewModel model);
    Task<ValidationResult> ValidateCollectionPagesViewModel(CollectionPageViewModel model);
}