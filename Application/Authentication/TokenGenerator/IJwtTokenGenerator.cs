using MangaApi.Presentation.Dtos.UserDto;
using MangaApi.Presentation.ViewModels.UsersViewModel;

namespace MangaApi.Application.Authentication.TokenGenerator;

public interface IJwtTokenGenerator
{
    string Generate(UsersViewModel model);
    string GenerateInLogInProcess(UserDto? model);
}