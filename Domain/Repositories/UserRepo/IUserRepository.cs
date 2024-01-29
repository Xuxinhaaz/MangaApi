using MangaApi.Domain.Models.Users;
using MangaApi.Presentation.Dtos.UserDto;
using MangaApi.Presentation.ViewModels.UsersViewModel;

namespace MangaApi.Domain.Repositories.UserRepo;

public interface IUserRepository
{
    Task<List<UserModel>> GetUsers();
    Task<UserModel> GetUserById(string id);
    UserModel Generate(UsersViewModel model);
    Task<bool> LogInProcess(UsersLogInViewModel model);
    Task<bool> AnyUserName(UsersViewModel model);
    Task<bool> AnyUser(string id);
    UserDto MapEntity(UserModel model);
    List<UserDto> MapEntity(List<UserModel> model);
    Task AddToDb(UserModel model);
}
