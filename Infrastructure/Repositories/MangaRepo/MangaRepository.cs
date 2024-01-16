using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Domain.Data;
using MangaApi.Domain.Models.Manga;
using MangaApi.Domain.Models.Tags;
using MangaApi.Domain.Repositories.MangaRepo;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Infrastructure.Repositories.MangaRepo;

public class MangaRepository : IMangaRepository
{
    private readonly AppDbContext _context;
    
    public MangaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Mangas>> GetMangas()
    {
        return await _context.Mangas
            .Include(m=> m.TagsModel)
            .Include(m => m.CollectionPage)
            .ToListAsync();
    }

    public async Task<Mangas> GetMangaById(string id)
    {
        return await _context.Mangas
            .Include(m => m.TagsModel)
            .Include(m => m.CollectionPage)
            .FirstAsync(x => x.Id == id);
    }

    public async Task<List<Mangas>> FilterMangasByTag(string tag)
    {
        List<Mangas> mangasFounded = await _context.Mangas
            .Where(model => model.TagsModel.Any(tagsModel => tagsModel.Tag == tag))
            .ToListAsync();

        return mangasFounded;
    }

    public async Task<List<Mangas>> SearchManga(string search, int pageNumber)
    {
        var listOfMangaFound = await _context.Mangas
            .Skip(pageNumber * 5)
            .Take(5)
            .Where(x => x.Title.ToLower() == search.ToLower())
            .ToListAsync();

        return listOfMangaFound;
    }

    public Mangas Generate(MangasViewModel model)
    {
        var filePath = Path.Combine("Storage", model.MangaPhoto.FileName);
        
        using Stream fileStream = new FileStream(filePath, FileMode.Create);
        
        model.MangaPhoto.CopyTo(fileStream);
            
        var newManga = new Mangas()
        {
            Popularity = 0,
            Id = Guid.NewGuid().ToString(),
            Title = model.Title,
            Author = model.Author,
            Description = model.Description,
            Group = model.Group,
            Translation = model.Translation
            
        };
        
        newManga.TagsModel = model.Tags.Select(tag => new TagsModel()
        {
            MangaId = newManga.Id,
            TagsId = Guid.NewGuid().ToString(),
            Tag = tag
        }).ToList();
        
        return newManga;
    }

    public async Task AddManga(Mangas model)
    {
        await _context.Mangas.AddAsync(model);
        await _context.Tags.AddRangeAsync(model.TagsModel);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AnyMangaTitle(Mangas model)
    {
        var anyManga = await _context.Mangas.AnyAsync(x => x.Title == model.Title);

        return anyManga;
    }

    public async Task<bool> AnyManga(string id)
    {
        var anyManga = await _context.Mangas.AnyAsync(x => x.Id == id);

        return anyManga;
    }
}