using MangaApi.Domain.Models.Mangas;

namespace MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;

public class CollectionPageViewModel
{
    public List<PagesViewModel> Models { get; set; }
}

public class PagesViewModel
{
    public int? PageNumber { get; set; }
    public string? PageName { get; set; }
    public string? MangaUrl { get; set; }
}