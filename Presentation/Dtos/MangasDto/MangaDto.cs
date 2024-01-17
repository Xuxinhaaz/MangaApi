using MangaApi.Domain.Models.Tags;

namespace MangaApi.Presentation.Dtos.MangasDto;

public class MangaDto
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public List<TagsModel>? TagsModel { get; set; }
    public string? Author { get; set; }
    public string? Group { get; set; }
    public string? Translation { get; set; }
    public string? Description { get; set; }
    public int? Popularity { get; set; }
}