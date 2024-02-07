using MangaApi.Domain.Models.Users;
using MangaApi.Domain.Models.Users.UsersProfile;
using MangaApi.Presentation.Dtos.UserDto.UsersProfileDto;
using MangaApi.Presentation.ViewModels.UsersViewModel.UsersProfileViewModel;

namespace MangaApi.Domain.Repositories.UserRepo.UsersProfileRepo;

public interface IUsersProfileRepository
{
    Task<List<UsersProfileModel>> GetAll(int pageNumber);
    Task<UsersProfileModel> GetById(string id);
    Task<UsersProfileModel> Generate(string id, UsersProfileViewModel model);
    Task<bool> Any();
    Task<UsersProfileDto> MapEntity(UsersProfileModel model);
    Task<List<UsersProfileDto>> MapEntities(List<UsersProfileModel> model);
    Task Add(UsersProfileModel model, UserModel userModel);
}