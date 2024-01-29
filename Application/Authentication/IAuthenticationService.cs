using MangaApi.Presentation.Dtos.UserDto;
using MangaApi.Presentation.ViewModels.UsersViewModel;

namespace MangaApi.Application.Authentication;

public interface IAuthenticationService
{
    Task<UserDto> Register(UsersViewModel model);
    Task<UserDto> LogIn(UsersViewModel model);
}