using AutoMapper;
using MangaApi.Domain.Data;
using MangaApi.Domain.Models.Users;
using MangaApi.Domain.Models.Users.UsersProfile;
using MangaApi.Domain.Repositories.UserRepo.UsersProfileRepo;
using MangaApi.Presentation.Dtos.UserDto.UsersProfileDto;
using MangaApi.Presentation.ViewModels.UsersViewModel.UsersProfileViewModel;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Infrastructure.Repositories.UsersRepo.UsersProfileRepo;

public class UsersProfileRepository : IUsersProfileRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UsersProfileRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UsersProfileModel>> GetAll(int pageNumber)
    {
        return await _context.UsersProfiles
            .Skip(pageNumber * 10)
            .Take(10)
            .ToListAsync();
    }

    public async Task<UsersProfileModel> GetById(string id)
    {
        return await _context.UsersProfiles.FirstOrDefaultAsync(x => x.UsersId == id);
    }

    public async Task<UsersProfileModel> Generate(string id, UsersProfileViewModel model)
    {
        var newUsersProfile = new UsersProfileModel
        {
            UsersProfileModelId = Guid.NewGuid().ToString(),
            UserName = model.UserName,
            UsersId = id,
            UserBio = model.UserBio
        };

        return newUsersProfile;
    }

    public async Task<bool> Any()
    {
        return await _context.UsersProfiles.AnyAsync();
    }

    public async Task<UsersProfileDto> MapEntity(UsersProfileModel model)
    {
        return _mapper.Map<UsersProfileDto>(model);
    }

    public async Task<List<UsersProfileDto>> MapEntities(List<UsersProfileModel> model)
    {
        return _mapper.Map<List<UsersProfileDto>>(model);
    }

    public async Task Add(UsersProfileModel model, UserModel userModel)
    {
        userModel.UsersProfileModel = model;
        await _context.UsersProfiles.AddAsync(model);
        await _context.SaveChangesAsync();
    }
}