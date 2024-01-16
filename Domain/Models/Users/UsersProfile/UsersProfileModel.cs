using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaApi.Domain.Models.Users.UsersProfile;

public class UsersProfileModel
{
    [Key]
    public string? UsersProfileModelId { get; set; }
    [ForeignKey("UserId")]
    public string? UsersId { get; set; }
    [NotMapped]
    public IFormFile? UserPhoto { get; set; }
    public string? UserName { get; set; }
    public string? UserBio { get; set; }
    public List<string>? UserHasReadMangas { get; set; }
}