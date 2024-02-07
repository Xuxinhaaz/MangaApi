using System.ComponentModel.DataAnnotations.Schema;

namespace MangaApi.Presentation.ViewModels.UsersViewModel.UsersProfileViewModel;

public class UsersProfileViewModel
{
    [NotMapped]
    public IFormFile? UserPhoto { get; set; }
    public string? UserName { get; set; }
    public string? UserBio { get; set; }
}