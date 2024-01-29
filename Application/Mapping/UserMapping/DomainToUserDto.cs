using AutoMapper;
using MangaApi.Domain.Models.Users;
using MangaApi.Presentation.Dtos.UserDto;

namespace MangaApi.Application.Mapping.UserMapping;

public class DomainToUserDto : Profile
{
    public DomainToUserDto()
    {
        CreateMap<UserModel, UserDto>();
    }
}