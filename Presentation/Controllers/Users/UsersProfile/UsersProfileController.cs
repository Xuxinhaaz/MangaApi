using AutoMapper;
using FluentValidation;
using MangaApi.Application.Authentication.TokenValidator;
using MangaApi.Domain.Repositories.UserRepo;
using MangaApi.Domain.Repositories.UserRepo.UsersProfileRepo;
using MangaApi.Presentation.ViewModels.UsersViewModel.UsersProfileViewModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace MangaApi.Presentation.Controllers.Users.UsersProfile;

[ApiController]
public class UsersProfileController : ControllerBase
{
    private readonly IUsersProfileRepository _usersProfileRepository;
    private readonly ITokenValidator _tokenValidator;
    private readonly IValidator<UsersProfileViewModel> _usersProfileViewModelValidator;
    private readonly IUserRepository _userRepository;
    public UsersProfileController(
        IUsersProfileRepository usersProfileRepository, 
        ITokenValidator tokenValidator, 
        IValidator<UsersProfileViewModel> usersProfileViewModelValidator, IUserRepository userRepository)
    {
        _usersProfileRepository = usersProfileRepository;
        _tokenValidator = tokenValidator;
        _usersProfileViewModelValidator = usersProfileViewModelValidator;
        _userRepository = userRepository;
    }
    
    [HttpGet("/api/v1/Users/Profiles")]
    public async Task<IActionResult> GetUsersProfile(
        [FromHeader] string Authorization, 
        [FromQuery] int pageNumber)
    {
        var responseAuth = await _tokenValidator.ValidateUsersJwt(Authorization);
        if (!responseAuth.IsValid)
        {
            return new UnauthorizedObjectResult(new
            {
                errors = new
                {
                    message = "Invalid authentication credentials."
                }
            });
        }

        return new OkObjectResult(new
        {
            usersProfiles = await _usersProfileRepository.GetAll(pageNumber)
        });
    }

    [HttpPost("/api/v1/Users/Profiles/{id}")]
    public async Task<IActionResult> PostUsersProfile( 
        [FromRoute] string id,
        [FromBody] UsersProfileViewModel model, 
        [FromHeader] string Authorization)
    {
        var responseAuth = await _tokenValidator.ValidateUsersJwt(Authorization);
        if (!responseAuth.IsValid)
        {
            return new UnauthorizedObjectResult(new
            {
                errors = new
                {
                    message = "Invalid authentication credentials."
                }
            });
        }
        
        var responseValidator = _usersProfileViewModelValidator.Validate(model);
        if (!responseValidator.IsValid)
        {
            var modelStateDictionary = new ModelStateDictionary();
            foreach (var item in responseValidator.Errors)
            {
                modelStateDictionary.AddModelError(item.PropertyName, item.ErrorMessage);
            }

            return new BadRequestObjectResult(new
            {
                errors = modelStateDictionary
            });
        }

        var newUsersProfile = await _usersProfileRepository.Generate(id, model);
        var usersProfileDto = await _usersProfileRepository.MapEntity(newUsersProfile);
        var userFound = await _userRepository.GetUserById(newUsersProfile.UsersId);
        
        await _usersProfileRepository.Add(newUsersProfile, userFound);

        return new CreatedAtActionResult(
            actionName: "PostUsersProfile",
            controllerName: "UsersProfile",
            routeValues: new { id, Authorization, model},
            value: new
            {
                userProfile = usersProfileDto
            }
            );
    }
}