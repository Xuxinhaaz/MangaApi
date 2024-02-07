namespace MangaApi.Presentation.Dtos.UserDto.UsersProfileDto;

public class UsersProfileDto
{
    public string? UsersId { get; set; }
    public string? UserName { get; set; }
    public string? UserBio { get; set; }
    public List<string>? UserHasReadMangas { get; set; }
}