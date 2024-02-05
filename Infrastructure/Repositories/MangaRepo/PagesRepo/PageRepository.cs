using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;
using MangaApi.Domain.Data;
using MangaApi.Domain.Models.Manga;
using MangaApi.Domain.Repositories.MangaRepo.PagesRepo;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Infrastructure.Repositories.MangaRepo.PagesRepo;

public class PageRepository : IPageRepository
{
    private readonly AppDbContext _context;
    private ILogger<PageRepository> _logger;
    
    public PageRepository(AppDbContext context, ILogger<PageRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CollectionPage> GetChapter(string id)
    {
        var collectionPageFound = await _context.CollectionPages
            .Include(m => m.PageModels)
            .FirstAsync(m => m.CollectionId == id);

        return collectionPageFound;
    }

    public async Task<PageModel> GetPageById(string id)
    {
        return await _context.PageModels.FirstAsync(x => x.CollectionId == id);
    }
    
    public async Task<Mangas> GeneratePages(CollectionPageViewModel model, string id)
    {
        Mangas mangasFound = await _context.Mangas
            .Include(m => m.Chapters)
            .FirstAsync(x => x.Id == id);
    
        var CollectionID = Guid.NewGuid().ToString();
    
        var pageModels = model.Models.Select(x => new PageModel()
            {
                CollectionId = CollectionID,
                PageName = x.PageName
            })
            .ToList();
        var collectionPage =  new CollectionPage()
        {
            MangaId = id,
            chapterSummary = model.ChapterSummary, 
            ChapterName = model.ChapterName,
            CollectionId = CollectionID,
            PageModels = pageModels
        };
    
        mangasFound.Chapters.Add(collectionPage);
        
        _logger.LogInformation("Page Models...");
        
        await _context.CollectionPages.AddAsync(collectionPage);
        
        _logger.LogInformation("Adding collection pages...");
        
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Saved.");
    
        return mangasFound;
    }
    
    public async Task GeneratePagesPhotos(CollectionPagesPhotosViewModel model, string id)
    {
        Mangas mangasFound = await _context.Mangas.FirstAsync(x => x.Id == id);
        
        _logger.LogInformation("Manga Founded...");
        
        if (string.IsNullOrWhiteSpace(mangasFound.Title))
        {
            _logger.LogError("Title of the manga is null or empty.");
        }
        
        var filePathDirectory = Path.Combine("Storage", "MangaPages", mangasFound.Title);
    
        try
        {
            if (!Directory.Exists(filePathDirectory))
            {
                _logger.LogInformation("Creating directory: {Directory}", filePathDirectory);
                Directory.CreateDirectory(filePathDirectory);
            }
            else
            {
                _logger.LogInformation("Directory already exists: {Directory}", filePathDirectory);
            }
    
            foreach (var item in model.MangasPhoto)
            {
                var filePath = Path.Combine(filePathDirectory, item.FileName);
    
                using (Stream photoStream = new FileStream(filePath, FileMode.Create))
                {
                    _logger.LogInformation("Copying to: {FilePath}", filePath);
                    item.CopyTo(photoStream);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating directory or copying files.");
        }
    }
    
    public async Task<bool> AnyMangaCollectionPageByid(string id)
    {
        var anyMangaCollectionPageById = await _context.CollectionPages
            .AnyAsync(x => x.CollectionId == id);
    
        return anyMangaCollectionPageById;
    }

    public async Task Add(Mangas model)
    {
        await _context.CollectionPages.AddRangeAsync(model.Chapters);
        foreach (var item in model.Chapters)
        {
            await _context.PageModels.AddRangeAsync(item.PageModels);
        }
    }
}