using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Domain.Models.Manga;

namespace MangaApi.Domain.Repositories.MangaRepo;

public interface IMangaRepository
{
    Task<List<Mangas>> GetMangas();
    Task<Mangas> GetMangaById(string id);
    Task<List<Mangas>> FilterMangasByTag(string tag);
    Task<List<Mangas>> SearchManga(string search, int pageNumber);
    Mangas Generate(MangasViewModel model);
    Task AddManga(Mangas model);
    Task<bool> AnyMangaTitle(Mangas model);
    Task<bool> AnyManga(string id);
}