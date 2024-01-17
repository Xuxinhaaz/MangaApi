using MangaApi.Presentation.ViewModels.UsersViewModel;

namespace MangaApi.Application.Authentication;

public interface IJwtTokenGenerator
{
    string Generate(UsersViewModel model);
}