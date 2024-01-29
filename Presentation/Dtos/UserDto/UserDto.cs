namespace MangaApi.Presentation.Dtos.UserDto;

public class UserDto
{
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public List<string>? UserRoles{ get; set; }
    public string? Token { get; set; }
}