namespace MangaApi.Presentation.ViewModels.UsersViewModel;

public class UsersViewModel
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? UserEmail { get; set; }
    public List<string>? UserRoles{ get; set; }
}