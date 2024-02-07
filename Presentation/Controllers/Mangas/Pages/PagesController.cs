using FluentValidation;
using FluentValidation.Results;
using MangaApi.Application.Authentication.TokenValidator;
using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;
using MangaApi.Domain.Data;
using MangaApi.Domain.Repositories.MangaRepo;
using MangaApi.Domain.Repositories.MangaRepo.PagesRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Presentation.Controllers.Mangas.Pages;

public class PagesController : ControllerBase
{
    private readonly IValidator<CollectionPageViewModel> _collectionPageValidator;
    private readonly IValidator<CollectionPagesPhotosViewModel> _collectionPagePhotosValidator;
    private readonly IPageRepository _pageRepository;
    private readonly IMangaRepository _mangaRepository;
    private readonly ITokenValidator _tokenValidator;
    private readonly AppDbContext _context;

    public PagesController(
        IMangaRepository mangaRepository, 
        IPageRepository pageRepository,
        IValidator<CollectionPageViewModel> collectionPageValidator, 
        IValidator<CollectionPagesPhotosViewModel> collectionPagePhotosValidator, ITokenValidator tokenValidator, AppDbContext context)
    {
        _pageRepository = pageRepository;
        _collectionPageValidator = collectionPageValidator;
        _collectionPagePhotosValidator = collectionPagePhotosValidator;
        _tokenValidator = tokenValidator;
        _context = context;
        _mangaRepository = mangaRepository;
    }
    
    
    [HttpGet("/api/v1/Manga/Page/{id}")]
    public async Task<IActionResult> GetMangaPageById( [FromRoute] string id, [FromHeader] string Authorization)
    {
        if (string.IsNullOrEmpty(id))
            return new BadRequestObjectResult("Id must not be empty");
        
        var responseAuth = await _tokenValidator.ValidateUsersJwt(Authorization);
        if (!responseAuth.IsValid)
        {
            return new UnauthorizedObjectResult(new
            {
                erros = new
                {
                    message = "Invalid authentication credentials."
                }
            });
        }
        
        return new OkObjectResult(new
        {
            page = await _pageRepository.GetPageById(id)
        });
    }
    
    [HttpGet("/api/v1/Manga/Pages/{id}")]
    public async Task<IActionResult> GetMangaChapter( [FromRoute] string id, [FromHeader] string Authorization)
    {
        if (string.IsNullOrEmpty(id))
            return new BadRequestObjectResult("Id must not be empty");
        
        var responseAuth = await _tokenValidator.ValidateUsersJwt(Authorization);
        if (!responseAuth.IsValid)
        {
            return new UnauthorizedObjectResult(new
            {
                erros = new
                {
                    message = "Invalid authentication credentials."
                }
            });
        }
        
        bool anyChapter = await _pageRepository.AnyMangaCollectionPageByid(id);
        if (!anyChapter)
            return BadRequest("No chapter found.");
        
        return new OkObjectResult(new
        {
            pages = await _pageRepository.GetChapter(id)
        });
    }
    
    [HttpPost("/api/v1/Manga/Pages/{id}")]
    public async Task<IActionResult> PostMangaChapter(
        [FromRoute] string id, 
        [FromBody] CollectionPageViewModel model, 
        [FromHeader] string Authorization)
    {
        var responseAuth = await _tokenValidator.ValidateUsersJwt(Authorization);
        if (!responseAuth.IsValid)
        {
            return new UnauthorizedObjectResult(new
            {
                erros = new
                {
                    message = "Invalid authentication credentials."
                }
            });
        }
        
        ValidationResult validationResult = await _collectionPageValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (ValidationFailure validationFailure in validationResult.Errors)
            {
                modelStateDictionary.AddModelError(validationFailure.PropertyName, validationFailure.ErrorMessage);
            }

            return new BadRequestObjectResult(new
            {
                errors = modelStateDictionary
            });
        }
        
        var anyMangaCollectionPage = await _pageRepository.AnyMangaCollectionPageByid(id);
        var anyManga = await _mangaRepository.AnyManga(id);
        if (anyMangaCollectionPage || !anyManga) 
            return BadRequest("Manga page with this id already exists.");

        Domain.Models.Manga.Mangas fulManga = await _pageRepository.GeneratePages(model, id);

        return new CreatedAtActionResult(
            actionName: "PostChapterPages",
            controllerName: "Pages",
            routeValues: new { id, model, Authorization},
            value: new
            {
                Pages = fulManga.Chapters
            });
    }
    
    [HttpPost("/api/v1/Manga/Pages/Photos/{id}")]
    public async Task<IActionResult> MangaPagesPhotoPost(
        [FromRoute] string id, 
        [FromForm] CollectionPagesPhotosViewModel model, 
        [FromHeader] string Authorization)
    {
        if (string.IsNullOrEmpty(id))
            return new BadRequestObjectResult("Id must not be empty");
        
        var responseAuth = await _tokenValidator.ValidateUsersJwt(Authorization);
        if (!responseAuth.IsValid)
        {
            return new UnauthorizedObjectResult(new
            {
                erros = new
                {
                    message = "Invalid authentication credentials."
                }
            });
        }
        
        ValidationResult validationResult = await _collectionPagePhotosValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (ValidationFailure validationFailure in validationResult.Errors)
            {
                modelStateDictionary.AddModelError(validationFailure.PropertyName, validationFailure.ErrorMessage);
            }

            return new BadRequestObjectResult(new
            {
                errors = modelStateDictionary
            });
        }
        
        var fulManga = await _mangaRepository.GetMangaById(id);
        var anyMangaPages = await _pageRepository.AnyMangaCollectionPageByid(id);
        if (!anyMangaPages)
            return BadRequest("Manga must have a collection page.");
        
        await _pageRepository.GeneratePagesPhotos(model, id);
        await _pageRepository.Add(fulManga);

        return new CreatedResult();
    }

    [HttpDelete("/api/v1/Mangas/Pages/DeleteMax")]
    public async Task<IActionResult> DeleteMax()
    {
        var allPageModels = await _context.PageModels.ToListAsync();
        var allCollectionPages = await _context.CollectionPages
            .Include(m => m.PageModels)
            .ToListAsync();
        
        _context.PageModels.RemoveRange(allPageModels);
        _context.CollectionPages.RemoveRange(allCollectionPages);
        await _context.SaveChangesAsync();

        return new OkObjectResult(new
        {
            message = "OK"
        });
    }

}