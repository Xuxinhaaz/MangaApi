using MangaApi.Domain.Models.Mangas;
using MangaApi.Domain.Models.Tags;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Domain.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        
    }

    public DbSet<Mangas> Mangas { get; set; }
    public DbSet<CollectionPage> CollectionPages { get; set; }
    public DbSet<PageModel> PageModels { get; set; }
    public DbSet<TagsModel> Tags { get; set; }
}