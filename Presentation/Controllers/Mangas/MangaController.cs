using FluentValidation;
using FluentValidation.Results;
using MangaApi.Application.Authentication.TokenValidator;
using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Domain.Data;
using MangaApi.Domain.Repositories.MangaRepo;
using MangaApi.Domain.Repositories.MangaRepo.PagesRepo;
using MangaApi.Presentation.Dtos.MangasDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MangaApi.Presentation.Controllers.Mangas;

[ApiController]
public class MangaController
{
    private readonly ITokenValidator _tokenValidator;
    private readonly IValidator<MangasViewModel> _mangaValidator;
    private readonly IMangaRepository _mangaRepository;
    private readonly IPageRepository _pageRepository;
    private readonly AppDbContext _context;

    public MangaController(
        IMangaRepository mangaRepository,
        IValidator<MangasViewModel> validator,
        IPageRepository pageRepository,
        AppDbContext context, IValidator<MangasViewModel> mangaValidator, ITokenValidator tokenValidator)
    {
        _mangaRepository = mangaRepository;
        _pageRepository = pageRepository;
        _context = context;
        _mangaValidator = mangaValidator;
        _tokenValidator = tokenValidator;
    }

    [HttpGet("/api/v1/Mangas")]
    public async Task<IActionResult> MangasGet([FromHeader] string authorization)
    {
        var Mangas = await _mangaRepository.GetMangas();
        var MangasDtos = _mangaRepository.MapEntities(Mangas);

        return new OkObjectResult(new
        {
            Mangas = MangasDtos
        });
    }

    [HttpGet("/api/v1/Mangas/{id}")]
    public async Task<IActionResult> MangaGetById([FromRoute] string id, [FromHeader] string Authorization)
    {
        if (string.IsNullOrEmpty(id))
            return new BadRequestObjectResult(new
            {
                errors = "id cannot be null."
            });

        var responseAuth = await _tokenValidator.ValidateUsersJwt(Authorization);
        if (!responseAuth.IsValid)
        {
            return new UnauthorizedResult();
        }

        var anyManga = await _mangaRepository.AnyManga(id);
        if (!anyManga)
            return new BadRequestObjectResult(new
            {
                errors = "Manga with provided ID does not exist."
            });

        var manga = await _mangaRepository.GetMangaById(id);
        var mangaDto = _mangaRepository.MapEntity(manga);

        return new OkObjectResult(new
        {
            manga = mangaDto
        });
    }

    [HttpGet("/api/v1/Mangas/Tag")]
    public async Task<IActionResult> MangaTagsFilterByOneTag([FromQuery] string tag, [FromQuery] int pageNumber)
    {
        if (string.IsNullOrEmpty(tag))
            return new BadRequestObjectResult("tag must not be empty!");

        var mangasFiltered = await _mangaRepository.FilterMangasByTag(tag, pageNumber);
        if (mangasFiltered.Count == 0)
            return new OkObjectResult(new
            {
                mangas = Array.Empty<MangaDto>()
            });

        var mangasDto = _mangaRepository.MapEntities(mangasFiltered);

        return new OkObjectResult(new
        {
            mangas = mangasDto
        });

    }

    [HttpPost("/api/v1/Mangas/Tags")]
    public async Task<IActionResult> MangasTagGet([FromBody] List<string> tags, [FromQuery] int pageNumber)
    {
        var mangaFound = await _mangaRepository.GetMangasByListOfTags(tags, pageNumber);
        var mangaDto = _mangaRepository.MapEntities(mangaFound);

        return new OkObjectResult(new
        {
            mangas = mangaDto
        });
    }

    [HttpPost("/api/v1/Mangas")]
    public async Task<IActionResult> MangaPost([FromForm] MangasViewModel model)
    {
        ValidationResult validationResult = await _mangaValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (ValidationFailure validationFailure in validationResult.Errors)
            {
                modelStateDictionary.AddModelError(validationFailure.PropertyName, validationFailure.ErrorMessage);
            }

            return new BadRequestObjectResult(new
            {
                erros = modelStateDictionary
            });
        }

        var anyMangaTitle = await _mangaRepository.AnyMangaTitle(model);
        if (anyMangaTitle)
            return new BadRequestObjectResult("Mangas with provided title already exists!");

        var newManga = _mangaRepository.Generate(model);

        var mangaDto = _mangaRepository.MapEntity(newManga);

        await _mangaRepository.AddManga(newManga);

        return new OkObjectResult(new
        {
            manga = mangaDto
        });
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

        var mangasFound = await _mangaRepository.SearchManga(search, pageNumber);
        var mangasDto = _mangaRepository.MapEntities(mangasFound);

        return new OkObjectResult(new
        {
            mangas = mangasDto
        });
    }

    [HttpDelete("/api/v1/Mangas/DeleteMax")]
    public async Task<IActionResult> MangaDeleteMax()
    {

        var mangas = await _mangaRepository.GetMangas();
        _context.Mangas.RemoveRange(mangas);
        foreach (var item in mangas)
        {
            _context.Tags.RemoveRange(item.TagsModel);
        }
        await _context.SaveChangesAsync();

        return new OkResult();
    }
}