using MangaApi.Presentation.ViewModels.UsersViewModel;

namespace MangaApi.Application.Authentication.TokenGenerator;

public interface IJwtTokenGenerator
{
    string Generate(UsersViewModel model);
}