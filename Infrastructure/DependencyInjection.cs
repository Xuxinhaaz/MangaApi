using FluentValidation;
using MangaApi.Application.Authentication;
using MangaApi.Domain.Repositories.MangaRepo;
using MangaApi.Domain.Repositories.MangaRepo.PagesRepo;
using MangaApi.Infrastructure.Authentication;
using MangaApi.Infrastructure.Repositories.MangaRepo;
using MangaApi.Infrastructure.Repositories.MangaRepo.PagesRepo;
using MangaApi.Infrastructure.Services.Validator;
using IValidator = MangaApi.Application.Common.Services.Validators.IValidator;

namespace MangaApi.Application;

public static class DependencyInjectionInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection Services)
    {
        Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        
        return Services;
    }
}