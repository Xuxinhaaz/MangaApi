using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaApi.Domain.Models.Manga;

public class CollectionPage
{
    [Key]
    public string? CollectionId { get; set; }
    [ForeignKey("Id")]
    public string? MangaId { get; set; }
    public string? ChapterName { get; set; }
    public string? chapterSummary { get; set; }
    public List<PageModel>? PageModels { get; set; }
}
public class PageModel
{
    [Key]
    public int? PageNumber { get; set; }
    [ForeignKey("CollectionId")]
    public string? CollectionId { get; set; }
    public string? PageName { get; set; }
    [NotMapped]
    public IFormFile? MangaPage { get; set; }
}