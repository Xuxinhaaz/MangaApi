using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;
using MangaApi.Domain.Models.Mangas;

namespace MangaApi.Domain.Repositories.MangaRepo.PagesRepo;

public interface IPageRepository
{
    Task<CollectionPage> GetPages(string id);
    Task<PageModel> GetPageById(string id);
    Task<Mangas> GeneratePages(CollectionPageViewModel model, string id);
}