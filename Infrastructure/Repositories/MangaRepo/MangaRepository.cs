using AutoMapper;
using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Domain.Data;
using MangaApi.Domain.Models.Manga;
using MangaApi.Domain.Models.Tags;
using MangaApi.Domain.Repositories.MangaRepo;
using MangaApi.Presentation.Dtos.MangasDto;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Infrastructure.Repositories.MangaRepo;

public class MangaRepository : IMangaRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    
    public MangaRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Mangas>> GetMangas()
    {
        return await _context.Mangas
            .Include(m=> m.TagsModel)
            .Include(m => m.Chapters)
            .ToListAsync();
    }

    public async Task<Mangas> GetMangaById(string id)
    {
        return await _context.Mangas
            .Include(m => m.TagsModel)
            .Include(m => m.Chapters)
            .FirstAsync(x => x.Id == id);
    }

    public async Task<List<Mangas>> FilterMangasByTag(string tag, int pageNumber)
    {
        List<Mangas> mangasFounded = await _context.Mangas
            .Skip(pageNumber * 5)
            .Take(5)
            .Where(model => model.TagsModel.Any(tagsModel => tagsModel.Tag == tag))
            .Include(m => m.TagsModel)
            .ToListAsync();

        return mangasFounded;
    }

    public async Task<List<Mangas>> GetMangasByListOfTags(List<string> tags, int pageNumber)
    {
        var mangasFound = await _context.Mangas
            .Skip(pageNumber * 5)
            .Take(5)
            .Include(m => m.TagsModel)
            .Where(m => tags.All(t => m.TagsModel.Any(mt => mt.Tag.ToLower() == t.ToLower())))
            .ToListAsync();

        return mangasFound;
    }

    public async Task<List<Mangas>> SearchManga(string search, int pageNumber)
    {
        var listOfMangaFound = await _context.Mangas
            .Skip(pageNumber * 5)
            .Take(5)
            .Include(m => m.TagsModel)
            .Where(x => x.Title.Contains(search))
            .ToListAsync();

        return listOfMangaFound;
    }

    public Mangas Generate(MangasViewModel model)
    {
        var filePath = Path.Combine("Storage", "MangasPhoto", model.MangaPhoto.FileName);
        
        using Stream fileStream = new FileStream(filePath, FileMode.Create);
        
        model.MangaPhoto.CopyTo(fileStream);
            
        var newManga = new Mangas()
        {
            Chapters = new List<CollectionPage>(),
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
        await _context.CollectionPages.AddRangeAsync(model.Chapters);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AnyMangaTitle(MangasViewModel model)
    {
        var anyManga = await _context.Mangas.AnyAsync(x => x.Title == model.Title);

        return anyManga;
    }

    public async Task<bool> AnyManga(string id)
    {
        var anyManga = await _context.Mangas.AnyAsync(x => x.Id == id);

        return anyManga;
    }

    public MangaDto MapEntity(Mangas model)
    {
        var mangaDto = _mapper.Map<MangaDto>(model);

        return mangaDto;
    }

    public List<MangaDto> MapEntities(List<Mangas> models)
    {
        var listOfDTOs = _mapper.Map<List<MangaDto>>(models);

        return listOfDTOs;
    }
}