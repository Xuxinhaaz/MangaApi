using MangaApi.Application.Authentication;
using MangaApi.Presentation.ViewModels.UsersViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MangaApi.Presentation.Controllers.Users;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UsersController(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpPost("/api/v1/GenerateToken")]
    public IActionResult GenerateToken( [FromBody] UsersViewModel model)
    {
        var strToken = _jwtTokenGenerator.Generate(model);

        return new OkObjectResult(new
        {
            token = strToken
        });
    }
}