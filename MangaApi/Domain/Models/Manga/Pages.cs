using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MangaApi.Domain.Models.Mangas;

public class CollectionPage
{
    [Key]
    public string? CollectionId { get; set; }
    [ForeignKey("Id")]
    public string? MangaId { get; set; }
    public List<PageModel>? PageModels = [];
}
public class PageModel
{
    [Key]
    public int? PageNumber { get; set; }
    [ForeignKey("CollectionId")]
    public string? CollectionId { get; set; }
    public string? PageName { get; set; }
    public string? MangaUrl { get; set; }
}