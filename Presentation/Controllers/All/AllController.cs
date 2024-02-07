using MangaApi.Domain.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Presentation.Controllers.All;

[ApiController]
public class AllController : ControllerBase
{
    private readonly AppDbContext _context;

    public AllController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("/all")]
    public async Task<IActionResult> GetAll()
    {
        return new OkObjectResult(new
        {
            mangas = await _context.Mangas.ToListAsync(),
            tags = await _context.Tags.ToListAsync(),
            pages = new
            {
                chapters = await _context.CollectionPages.ToListAsync(),
                pages = await _context.PageModels.ToListAsync()
            },
            users = await _context.Users.ToListAsync(),
            usersProfile = await _context.UsersProfiles.ToListAsync()
        });
    }
    
}