using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Domain.Models.Manga;
using MangaApi.Presentation.Dtos.MangasDto;

namespace MangaApi.Domain.Repositories.MangaRepo;

public interface IMangaRepository
{
    Task<List<Mangas>> GetMangas();
    Task<Mangas> GetMangaById(string id);
    Task<List<Mangas>> FilterMangasByTag(string tag, int pageNumber);
    Task<List<Mangas>> GetMangasByListOfTags(List<string> tags, int pageNumber);
    Task<List<Mangas>> SearchManga(string search, int pageNumber);
    Mangas Generate(MangasViewModel model);
    Task AddManga(Mangas model);
    Task<bool> AnyMangaTitle(MangasViewModel model);
    Task<bool> AnyManga(string id);
    MangaDto MapEntity(Mangas model);
    List<MangaDto> MapEntities(List<Mangas> models);
}