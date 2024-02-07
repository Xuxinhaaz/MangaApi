using AutoMapper;
using MangaApi.Domain.Models.Users.UsersProfile;
using MangaApi.Presentation.Dtos.UserDto.UsersProfileDto;

namespace MangaApi.Application.Mapping.UserMapping.UsersProfileMapping;

public class DomainToUsersProfileDto : Profile
{
    public DomainToUsersProfileDto()
    {
        CreateMap<UsersProfileModel, UsersProfileDto>();
    }
}