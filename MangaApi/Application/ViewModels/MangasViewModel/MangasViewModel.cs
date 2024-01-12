using MangaApi.Domain.Models.Mangas;

namespace MangaApi.Application.ViewModels.MangasViewModel;

public class MangasViewModel
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Group { get; set; }
    public string? Translation { get; set; }
    public string? Description { get; set; }
    public List<string> Tags { get; set; }
}