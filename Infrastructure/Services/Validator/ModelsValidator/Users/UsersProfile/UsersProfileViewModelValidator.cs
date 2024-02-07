using FluentValidation;
using MangaApi.Domain.Models.Users.UsersProfile;
using MangaApi.Infrastructure.Services.Validator.ModelsValidator.Pages.IFormFileValidator;
using MangaApi.Presentation.ViewModels.UsersViewModel.UsersProfileViewModel;

namespace MangaApi.Infrastructure.Services.Validator.ModelsValidator.Users.UsersProfile;

public class UsersProfileViewModelValidator: AbstractValidator<UsersProfileViewModel>
{
    public UsersProfileViewModelValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Your name must not be empty.")
            .MinimumLength(4).WithMessage("Your name must be at least 6 characters")
            .MaximumLength(20).WithMessage("Your name must be at most 20 characters.");

        RuleFor(x => x.UserBio).NotEmpty().WithMessage("Your bio must not be empty.")
            .MinimumLength(1).WithMessage("Your bio must have at least 1 character.")
            .MaximumLength(100).WithMessage("Your bio must have at most 100 characters.");

        RuleFor(x => x.UserPhoto).SetValidator(new FormDataValidator());
    }
}