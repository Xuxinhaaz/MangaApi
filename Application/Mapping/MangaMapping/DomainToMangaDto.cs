using AutoMapper;
using MangaApi.Domain.Models.Manga;
using MangaApi.Presentation.Dtos.MangasDto;

namespace MangaApi.Application.Mapping.MangaMapping;

public class DomainToMangaDto : Profile
{
    public DomainToMangaDto()
    {
        CreateMap<Mangas, MangaDto>();
    }
}