using MangaApi.Application.ViewModels.MangasViewModel;
using MangaApi.Application.ViewModels.MangasViewModel.PagesViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MangaApi.Controllers.Mangas;

public interface IMangaController
{
    [HttpGet("/Mangas")]
    Task<IActionResult> MangasGet( [FromHeader] string authorization );

    Task<IActionResult> TagsGet();

    [HttpGet("/Manga/{id}")]
    Task<IActionResult> MangaGetById( [FromRoute] string id);

    [HttpGet("/Mangas/Tag")]
    Task<IActionResult> MangaTagsFilterByOneTag( [FromHeader] string authorization, [FromQuery] string tag );

    [HttpGet("/Manga/Tag")] 
    Task<IActionResult> MangasTagGet( [FromHeader] string authorization );

    [HttpGet("/manga/page/{id}")]
    Task<IActionResult> MangaPageById( [FromRoute] string id, [FromQuery] int pageNumber); 
    
    [HttpGet("/manga/pages/{id}")]
    Task<IActionResult> MangaPages( [FromHeader] string authorization, [FromRoute] string id);

    [HttpPost("/Manga")]
    Task<IActionResult> MangaPost( [FromHeader] string authorization, MangasViewModel model );
    
    [HttpPost("/Manga/pages/{id}")]
    Task<IActionResult> MangaPagesPost( [FromHeader] string authorization, [FromRoute] string id, [FromBody] CollectionPageViewModel model);

    [HttpPost("/Mangas/Search")]
    Task<IActionResult> MangaSearch( [FromHeader] string authorization, [FromQuery] string search);

    Task<IActionResult> MangaDeleteMax();
}