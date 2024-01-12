using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using MangaApi.Application.Repositories.MangaRepo;
using MangaApi.Application.Repositories.MangaRepo.PagesRepo;
using MangaApi.Application.Services.Validators;
using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;
using MangaApi.Domain.Data;
using MangaApi.Domain.Models.Mangas;
using MangaApi.Domain.Models.Tags;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PageModel = MangaApi.Domain.Models.Mangas.PageModel;

namespace MangaApi.Controllers.Mangas;

[ApiController]
public class MangaController : IMangaController
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
            return new BadRequestObjectResult("Id must not be null/empty!");
        
        return new OkObjectResult(new
        {
            manga = await _mangaRepository.GetMangaById(id)
            
        });
    }

    [HttpGet("/api/v1/Mangas/Tag")]
    public async Task<IActionResult> MangaTagsFilterByOneTag( [FromHeader] string authorization, [FromQuery] string tag)
    {
        if (string.IsNullOrEmpty(tag))
            return new BadRequestObjectResult("tag must not be null/empty!");

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

    [HttpGet("/api/v1/manga/page/{id}")]
    public Task<IActionResult> MangaPageById(string authorization, string id, int pageNumber)
    {
        throw new NotImplementedException();
    }

    [HttpGet("/api/v1/manga/pages/{id}")]
    public Task<IActionResult> MangaPages(string authorization, string id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("/api/v1/Mangas")]
    public async Task<IActionResult> MangaPost( [FromHeader] string authorization, [FromBody] MangasViewModel model)
    {
        Dictionary<string, dynamic> errors = new();
        
        ValidationResult response = await _validator.ValidateMangasViewModel(model);
        if (!response.IsValid)
            return new BadRequestObjectResult(errors["Erros"] = response.Errors.ToList());
        
        var newManga = _mangaRepository.Generate(model);
        await _mangaRepository.AddManga(newManga);
        
        return new OkObjectResult(newManga);
    }

    [HttpPost("/api/v1/Manga/pages/{id}")]
    public async Task<IActionResult> MangaPagesPost([FromHeader] string authorization, [FromRoute] string id, [FromBody] CollectionPageViewModel model)
    {
        Dictionary<string, dynamic> errors = new();
        var responseValidation = await _validator.ValidateCollectionPagesViewModel(model);
        if (!responseValidation.IsValid)
            return new BadRequestObjectResult(errors["Errors"] = responseValidation.Errors.ToList());
        
        var fulManga = await _pageRepository.GeneratePages(model, id);
        
        return new OkObjectResult(new
        {
            fulManga,
            fulManga.CollectionPage?.PageModels
        });
    }



    [HttpPost("/api/v1/Mangas/Search")]
    public Task<IActionResult> MangaSearch(string authorization, string search)
    {
        throw new NotImplementedException();
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