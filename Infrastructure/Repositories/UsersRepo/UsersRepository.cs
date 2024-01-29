using AutoMapper;
using MangaApi.Domain.Data;
using MangaApi.Domain.Models.Manga;
using MangaApi.Domain.Models.Users;
using MangaApi.Domain.Models.Users.UsersProfile;
using MangaApi.Domain.Repositories.UserRepo;
using MangaApi.Presentation.Dtos.UserDto;
using MangaApi.Presentation.ViewModels.UsersViewModel;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Infrastructure.Repositories.UsersRepo;

public class UsersRepository : IUserRepository
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public UsersRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<List<UserModel>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<UserModel> GetUserById(string id)
    {
        var userFound = await _context.Users.FirstAsync(x => x.UserId == id);

        return userFound;
    }

    public UserModel Generate(UsersViewModel model)
    {
        var userRoles = new List<string>() { "User", "Admin" };
        
        return new UserModel
        {
            UserEmail = model.UserEmail,
            UserId = Guid.NewGuid().ToString(),
            UserName = model.UserName,
            UserRoles = userRoles,
            UserPassword = BCrypt.Net.BCrypt.HashPassword(model.Password)
        };
    }

    public async Task<bool> LogInProcess(UsersLogInViewModel model)
    {
        var anyEmail = await _context.Users
            .AnyAsync(x => x.UserEmail == model.Email);

        var anyPassword = await _context.Users
            .AnyAsync(x => VerifyPassword(model.Password, x.UserPassword));
        
        return anyEmail && anyPassword;
    }
    
    private static bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
    }

    public async Task<bool> AnyUserName(UsersViewModel model)
    {
        return await _context.Users.AnyAsync(x => x.UserName.ToLower() == model.UserName.ToLower());
    }

    public async Task<bool> AnyUser(string id)
    {
        return await _context.Users.AnyAsync(x => x.UserId == id);
    }

    public UserDto MapEntity(UserModel model)
    {
        return _mapper.Map<UserDto>(model);
    }

    public List<UserDto> MapEntity(List<UserModel> model)
    {
        return _mapper.Map<List<UserDto>>(model);
    }

    public async Task AddToDb(UserModel model)
    {
        await _context.Users.AddAsync(model);
        await _context.SaveChangesAsync();
    }
    
}