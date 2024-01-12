using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaApi.Domain.Models.Tags;

public class TagsModel
{
    [Key]
    public string? TagsId { get; set; }
    [ForeignKey("Id")]
    public string? MangaId { get; set; }
    public string? Tag { get; set; }
    
}