using FluentValidation;
using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;
using MangaApi.Domain.Data;
using MangaApi.Domain.Repositories.MangaRepo;
using MangaApi.Domain.Repositories.MangaRepo.PagesRepo;
using Microsoft.AspNetCore.Mvc;

namespace MangaApi.Presentation.Controllers.Mangas.Pages;

public class PagesController : ControllerBase
{
    
    private readonly Application.Services.Validators.IValidator _validator;
    private readonly IPageRepository _pageRepository;
    private readonly AppDbContext _context;
    private readonly IMangaRepository _mangaRepository;

    public PagesController(
        IMangaRepository mangaRepository, 
        Application.Services.Validators.IValidator validator, 
        IPageRepository pageRepository,
        AppDbContext context)
    {
        _validator = validator;
        _pageRepository = pageRepository;
        _context = context;
        _mangaRepository = mangaRepository;
    }
    
    
    [HttpGet("/api/v1/Manga/Page/{id}")]
    public async Task<IActionResult> MangaPageById( [FromRoute] string id )
    {
        if (string.IsNullOrEmpty(id))
            return new BadRequestObjectResult("Id must not be empty");
        
        return new OkObjectResult(new
        {
            page = await _pageRepository.GetPageById(id)
        });
    }
    
    [HttpGet("/api/v1/Manga/Pages/{id}")]
    public async Task<IActionResult> MangaPages( [FromRoute] string id )
    {
        if (string.IsNullOrEmpty(id))
            return new BadRequestObjectResult("Id must not be empty");
        
        bool anyMangaCollectionPageById = await _pageRepository.AnyMangaCollectionPageByid(id);
        if (!anyMangaCollectionPageById)
            return BadRequest("No mangas found with provided ID.");
        
        return new OkObjectResult(new
        {
            pages = await _pageRepository.GetPages(id)
        });
    }
    
    [HttpPost("/api/v1/Manga/Pages/{id}")]
    public async Task<IActionResult> MangaPagesPost([FromRoute] string id, [FromBody] CollectionPageViewModel model)
    {
        if (string.IsNullOrEmpty(id))
            return new BadRequestObjectResult("Id must not be empty");

        var responseValidator = await _validator.ValidateCollectionPagesViewModel(model);
        if (!responseValidator.IsValid)
            return BadRequest(responseValidator.Errors.ToList());
        
        var anyMangaCollectionPage = await _pageRepository.AnyMangaCollectionPageByid(id);
        var anyMangaPage = await _pageRepository.AnyMangaPageByid(id);
        var anyManga = await _mangaRepository.AnyManga(id);
        if (anyMangaCollectionPage || anyMangaPage || !anyManga) 
            return BadRequest("Manga page with this id already exists.");

        Domain.Models.Manga.Mangas fulManga = await _pageRepository.GeneratePages(model, id);
        
        return new OkObjectResult(new
        {
            pages = fulManga.CollectionPage
        });
    }
    
    [HttpPost("/api/v1/Manga/Pages/Photos/{id}")]
    public async Task<IActionResult> MangaPagesPost([FromRoute] string id, [FromForm] CollectionPagesPhotosViewModel model)
    {
        if (string.IsNullOrEmpty(id))
            return new BadRequestObjectResult("Id must not be empty");
        
        var fulManga = await _mangaRepository.GetMangaById(id);
        var anyMangaPages = await _pageRepository.AnyMangaCollectionPageByid(id);
        if (!anyMangaPages)
            return BadRequest("Manga must have a collection page.");
        
        await _pageRepository.GeneratePagesPhotos(model, id);
        
        return new OkObjectResult(new
        {
            pages = fulManga
        });
    }

}