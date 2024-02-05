using System.Data;
using FluentValidation;
using MangaApi.Presentation.ViewModels.UsersViewModel;

namespace MangaApi.Infrastructure.Services.Validator.ModelsValidator.Users;

public class UsersViewModelValidator : AbstractValidator<UsersViewModel>
{
    public UsersViewModelValidator()
    {
        RuleFor(x => x.Password).NotEmpty().WithMessage("Your password cannot be empty.")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");

        RuleFor(x => x.UserEmail).NotEmpty().WithMessage("Your Email cannot be empty.")
            .MinimumLength(6).WithMessage("Your Email must be at least 6 characters.")
            .MaximumLength(30).WithMessage("Your Email must be at most 30 characters.")
            .EmailAddress().WithMessage("Your Email must be valid.");

        RuleFor(x => x.UserName).NotEmpty().WithMessage("Your name must not be empty.")
            .MinimumLength(4).WithMessage("Your name must be at least 6 characters")
            .MaximumLength(20).WithMessage("Your name must be at most 20 characters.");
    }
}