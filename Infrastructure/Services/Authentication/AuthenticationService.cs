using FluentValidation;
using MangaApi.Application.Authentication.TokenGenerator;
using MangaApi.Domain.Repositories.UserRepo;
using MangaApi.Infrastructure.Services.Validator.ModelsValidator.Users;
using MangaApi.Presentation.Dtos.UserDto;
using MangaApi.Presentation.ViewModels.UsersViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Client;
using IAuthenticationService = MangaApi.Application.Authentication.IAuthenticationService;

namespace MangaApi.Infrastructure.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository
        )
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }


    public async Task<UserDto> Register(UsersViewModel model)
    {
        var newManga = _userRepository.Generate(model);

        model.UserRoles = newManga.UserRoles;
        var strToken = _jwtTokenGenerator.Generate(model);

        var mangaDto = _userRepository.MapEntity(newManga);
        mangaDto.Token = strToken;
        
        await _userRepository.AddToDb(newManga);

        return mangaDto;
    }

    public async Task<bool> LogIn(UsersLogInViewModel model)
    {
        var isLoggedIn = await _userRepository.LogInProcess(model);
        
        return isLoggedIn;
    }
}