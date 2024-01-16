using FluentValidation.Results;
using MangaApi.Application.Services.Validators;
using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Domain.Data;
using MangaApi.Domain.Repositories.MangaRepo;
using MangaApi.Domain.Repositories.MangaRepo.PagesRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Presentation.Controllers.Mangas;

[ApiController]
public class MangaController
{
    private readonly IValidator _validator;
    private readonly IMangaRepository _mangaRepository;
    private readonly IPageRepository _pageRepository;
    private readonly AppDbContext _context;

    public MangaController(
        IMangaRepository mangaRepository, 
        IValidator validator, 
        IPageRepository pageRepository,
        AppDbContext context)
    {
        _mangaRepository = mangaRepository;
        _validator = validator;
        _pageRepository = pageRepository;
        _context = context;
    }

    [HttpGet("/api/v1/Mangas")]
    public async Task<IActionResult> MangasGet([FromHeader] string authorization)
    {
        return new OkObjectResult(new
        {
            Mangas = await _mangaRepository.GetMangas()
        });
    }
    
    [HttpGet("/api/v1/Tags")]
    public async Task<IActionResult> TagsGet()
    {
        return new OkObjectResult(new
        {
            Tags = await _context.Tags.ToListAsync()
        });
    }

    [HttpGet("/api/v1/Mangas/{id}")]
    public async Task<IActionResult> MangaGetById(  [FromRoute] string id)
    {
        if (string.IsNullOrEmpty(id))
            return new BadRequestObjectResult("Id must not be empty");
        
        return new OkObjectResult(new
        {
            manga = await _mangaRepository.GetMangaById(id)
            
        });
    }

    [HttpGet("/api/v1/Mangas/Tag")]
    public async Task<IActionResult> MangaTagsFilterByOneTag( [FromHeader] string authorization, [FromQuery] string tag)
    {
        if (string.IsNullOrEmpty(tag))
            return new BadRequestObjectResult("tag must not be empty!");

        return new OkObjectResult(new
        {
            mangas = await _mangaRepository.FilterMangasByTag(tag)
        });
    }

    [HttpGet("/api/v1/Manga/Tag")] 
    public Task<IActionResult> MangasTagGet(string authorization)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("/api/v1/Mangas")]
    public async Task<IActionResult> MangaPost( [FromHeader] string authorization, [FromForm] MangasViewModel model)
    {
        Dictionary<string, dynamic> errors = new();
        
        ValidationResult response = await _validator.ValidateMangasViewModel(model);
        if (!response.IsValid)
            return new BadRequestObjectResult(errors["Erros"] = response.Errors.ToList());
        
        var newManga = _mangaRepository.Generate(model);
        await _mangaRepository.AddManga(newManga);
        
        return new OkObjectResult(newManga);
    }


    [HttpPost("/api/v1/Mangas/Search")]
    public async Task<IActionResult> MangaSearch( 
        [FromHeader] string authorization, 
        [FromQuery] string search, 
        [FromQuery] int pageNumber
        )
    {
        if (string.IsNullOrEmpty(search))
            return new BadRequestObjectResult("search param must not be empty.");

        return new OkObjectResult(new
        {
            mangas = await _mangaRepository.SearchManga(search, pageNumber)
        });
    }

    [HttpDelete("/api/v1/Mangas/DeleteMax")]
    public async Task<IActionResult> MangaDeleteMax()
    {

        var mangas = await _context.Mangas.Include(m => m.TagsModel).ToListAsync();
        _context.Mangas.RemoveRange(mangas);
        foreach (var item in mangas)
        {
            _context.Tags.RemoveRange(item.TagsModel);
        }
        await _context.SaveChangesAsync();
        
        return new OkResult();
    }
}