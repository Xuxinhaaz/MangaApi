using FluentValidation;
using MangaApi.Application.Authentication;
using MangaApi.Application.Authentication.TokenGenerator;
using MangaApi.Application.Authentication.TokenValidator;
using MangaApi.Domain.Models.Users.UsersProfile;
using MangaApi.Domain.Repositories.MangaRepo;
using MangaApi.Domain.Repositories.MangaRepo.PagesRepo;
using MangaApi.Domain.Repositories.UserRepo;
using MangaApi.Domain.Repositories.UserRepo.UsersProfileRepo;
using MangaApi.Infrastructure.Repositories.MangaRepo;
using MangaApi.Infrastructure.Repositories.MangaRepo.PagesRepo;
using MangaApi.Infrastructure.Repositories.UsersRepo;
using MangaApi.Infrastructure.Repositories.UsersRepo.UsersProfileRepo;
using MangaApi.Infrastructure.Services.Authentication;

namespace MangaApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(this IServiceCollection Services)
    {
        Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        Services.AddScoped<ITokenValidator, TokenValidator>();
        
        Services.AddScoped<IMangaRepository, MangaRepository>();
        Services.AddScoped<IPageRepository, PageRepository>();
        Services.AddScoped<IUserRepository, UsersRepository>();
        Services.AddScoped<IUsersProfileRepository, UsersProfileRepository>();
        
        return Services;
    }
}