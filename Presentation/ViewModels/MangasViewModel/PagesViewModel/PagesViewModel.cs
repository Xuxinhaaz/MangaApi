namespace MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;

public class CollectionPagesPhotosViewModel
{
    public List<IFormFile>? MangasPhoto { get; set; }

}

public class CollectionPageViewModel
{
    public List<PagesViewModel>? Models { get; set; }
}

public class PagesViewModel
{
    public string? PageName { get; set; }
}