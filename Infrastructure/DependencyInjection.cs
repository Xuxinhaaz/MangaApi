using FluentValidation;
using MangaApi.Application.Authentication;
using MangaApi.Application.Authentication.TokenGenerator;
using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;
using MangaApi.Infrastructure.Services.Authentication;
using MangaApi.Infrastructure.Services.Validator.ModelsValidator.Mangas;
using MangaApi.Infrastructure.Services.Validator.ModelsValidator.Pages;
using MangaApi.Infrastructure.Services.Validator.ModelsValidator.Pages.IFormFileValidator;
using MangaApi.Infrastructure.Services.Validator.ModelsValidator.Users;
using MangaApi.Presentation.ViewModels.UsersViewModel;

namespace MangaApi.Infrastructure;

public static class DependencyInjectionInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection Services)
    {
        Services.AddScoped<IValidator<MangasViewModel>, MangaViewModelValidator>();
        Services.AddScoped<IValidator<CollectionPageViewModel>, CollectionViewModelValidator>();
        Services.AddScoped<IValidator<CollectionPagesPhotosViewModel>, FileValidator>();
        Services.AddScoped<IValidator<UsersViewModel>, UsersViewModelValidator>();
        
        Services.AddScoped<IAuthenticationService, AuthenticationService>();
     
        return Services;
    }
}