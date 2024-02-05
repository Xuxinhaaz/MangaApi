using FluentValidation;
using FluentValidation.Results;
using MangaApi.Application.Authentication;
using MangaApi.Application.Authentication.TokenGenerator;
using MangaApi.Application.Authentication.TokenValidator;
using MangaApi.Domain.Repositories.UserRepo;
using MangaApi.Infrastructure.Services.Validator.ModelsValidator.Users;
using MangaApi.Presentation.ViewModels.UsersViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MangaApi.Presentation.Controllers.Users;

[ApiController]
public class UsersController : ControllerBase
{
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IValidator<UsersLogInViewModel> _usersLogInValidator;
    private readonly IValidator<UsersViewModel> _usersValidator;
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenValidator _tokenValidator;

    public UsersController(IValidator<UsersViewModel> usersValidator,
        IAuthenticationService authenticationService,
        IUserRepository userRepository,
        ITokenValidator tokenValidator,
        IValidator<UsersLogInViewModel> usersLogInValidator,
        IJwtTokenGenerator tokenGenerator)
    {
        _usersValidator = usersValidator;
        _authenticationService = authenticationService;
        _userRepository = userRepository;
        _tokenValidator = tokenValidator;
        _usersLogInValidator = usersLogInValidator;
        _tokenGenerator = tokenGenerator;
    }

    [HttpGet("/api/v1/GetUsers")]
    public async Task<IActionResult> GetUsers([FromHeader] string Authorization)
    {
        var responseAuth = await _tokenValidator.ValidateUsersJwt(Authorization);
        if (!responseAuth.IsValid)
        {
            return Unauthorized();
        }
        
        return new OkObjectResult(new
        {
            users = await _userRepository.GetUsers()
        });
    }

    [HttpPost("/api/v1/Register")]
    public async Task<IActionResult> Register( [FromBody] UsersViewModel model)
    {
        var responseAuth = _usersValidator.Validate(model);
        if (!responseAuth.IsValid)
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (ValidationFailure error in responseAuth.Errors)
            {
                modelStateDictionary.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return new BadRequestObjectResult(new {
                errors = modelStateDictionary
            });       
        }

        var user = await _authenticationService.Register(model);

        return new OkObjectResult(new
        {
            user
        });
    }
    
    [HttpPost("/api/v1/LogIn")]
    public async Task<IActionResult> LogIn( [FromBody] UsersLogInViewModel model)
    {
        var validationResult = _usersLogInValidator.Validate(model);
        if(!validationResult.IsValid)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var item in validationResult.Errors)
            {
                modelStateDictionary.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            return new BadRequestObjectResult(new {
                errors = modelStateDictionary
            });
        }

        var isLoggedIn = await _authenticationService.LogIn(model);
        if(!isLoggedIn)
        {
            return new NotFoundObjectResult(new {
                errors = "User not found!"
            });
        }

        var userFound = await _userRepository.FindUserInLogInProcess(model);
        var userFoundDto = _userRepository.MapEntity(userFound);

        var jwtToken = _tokenGenerator.GenerateInLogInProcess(userFoundDto);

        userFoundDto.Token = jwtToken;

        return new OkObjectResult(new {
            user = userFoundDto
        });
    }
}