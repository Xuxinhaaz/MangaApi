using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Domain.Models.Mangas;

namespace MangaApi.Application.Repositories.MangaRepo;

public interface IMangaRepository
{
    Task<List<Mangas>> GetMangas();
    Task<Mangas> GetMangaById(string id);
    Task<List<Mangas>> FilterMangasByTag(string tag);
    public Mangas Generate(MangasViewModel model);
    Task AddManga(Mangas model);
}