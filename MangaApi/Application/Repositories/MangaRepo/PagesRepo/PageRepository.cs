using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;
using MangaApi.Domain.Data;
using MangaApi.Domain.Models.Mangas;
using MangaApi.Domain.Models.Tags;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Application.Repositories.MangaRepo.PagesRepo;

public class PageRepository : IPageRepository
{
    private readonly AppDbContext _context;

    public PageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CollectionPage> GetPages(string id)
    {
        return await _context.CollectionPages.FirstAsync(x => x.MangaId == id);
    }

    public async Task<PageModel> GetPageById(string id)
    {
        return await _context.PageModels.FirstAsync(x => x.CollectionId == id);
    }

    public async Task<Mangas> GeneratePages(CollectionPageViewModel model, string id)
    {
        Mangas mangasFound = await _context.Mangas
                                        .Include(mangas => mangas.CollectionPage)
                                        .FirstAsync(x => x.Id == id);

        var CollectionID = Guid.NewGuid().ToString();
            
        mangasFound.CollectionPage = new CollectionPage()
        {
            MangaId = id,
            CollectionId = CollectionID,
            PageModels = model.Models.Select(x => new PageModel()
            {
                CollectionId = CollectionID,
                PageNumber = x.PageNumber,
                PageName = x.PageName,
                MangaUrl = x.MangaUrl
            })  
            .ToList()
        };

        await _context.CollectionPages.AddAsync(mangasFound.CollectionPage);
        await _context.SaveChangesAsync();

        return mangasFound;
    }
}