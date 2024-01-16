using MangaApi.Domain.Models.Manga;
using MangaApi.Domain.Models.Tags;
using MangaApi.Domain.Models.Users;
using MangaApi.Domain.Models.Users.UsersProfile;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Domain.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<UsersProfileModel> UsersProfiles { get; set; }
    public DbSet<Mangas> Mangas { get; set; }
    public DbSet<CollectionPage> CollectionPages { get; set; }
    public DbSet<PageModel> PageModels { get; set; }
    public DbSet<TagsModel> Tags { get; set; }
}