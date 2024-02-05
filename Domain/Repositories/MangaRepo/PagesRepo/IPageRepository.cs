using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;
using MangaApi.Domain.Models.Manga;

namespace MangaApi.Domain.Repositories.MangaRepo.PagesRepo;

public interface IPageRepository
{
    Task<CollectionPage> GetChapter(string id);
    Task<PageModel> GetPageById(string id);
    Task<Mangas> GeneratePages(CollectionPageViewModel model, string id);
    Task GeneratePagesPhotos(CollectionPagesPhotosViewModel model, string id);
    Task<bool> AnyMangaCollectionPageByid(string id);
    Task Add(Mangas model);
}