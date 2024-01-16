using System.ComponentModel.DataAnnotations;
using MangaApi.Domain.Models.Users.UsersProfile;

namespace MangaApi.Domain.Models.Users;

public class UserModel
{
    [Key]
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? UserEmail { get; set; }
    public string? UserPassword { get; set; }
    public UsersProfileModel? UsersProfileModel { get; set; }
    public List<string>? UserRoles{ get; set; }
}