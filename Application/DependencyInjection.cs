using FluentValidation;
using MangaApi.Domain.Repositories.MangaRepo;
using MangaApi.Domain.Repositories.MangaRepo.PagesRepo;
using MangaApi.Infrastructure.Repositories.MangaRepo;
using MangaApi.Infrastructure.Repositories.MangaRepo.PagesRepo;
using MangaApi.Infrastructure.Services.Validator;
using IValidator = MangaApi.Application.Common.Services.Validators.IValidator;

namespace MangaApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection Services)
    {
        Services.AddScoped<IMangaRepository, MangaRepository>();
        Services.AddScoped<IPageRepository, PageRepository>();
        Services.AddScoped<IValidator, Infrastructure.Services.Validator.Validator>();

        return Services;
    }
}