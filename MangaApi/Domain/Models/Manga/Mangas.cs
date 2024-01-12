using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using MangaApi.Domain.Models.Tags;

namespace MangaApi.Domain.Models.Mangas;

public class Mangas
{
    [Key]
    public string? Id { get; set; }
    public string? Title { get; set; }
    public List<TagsModel>? TagsModel { get; set; }
    public string? Author { get; set; }
    public string? Group { get; set; }
    public string? Translation { get; set; }
    public CollectionPage? CollectionPage { get; set; }
    public string? Description { get; set; }
    public int? Popularity { get; set; }
}