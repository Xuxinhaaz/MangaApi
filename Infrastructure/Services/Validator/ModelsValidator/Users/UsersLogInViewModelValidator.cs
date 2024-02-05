using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MangaApi.Presentation.ViewModels.UsersViewModel;

namespace MangaApi.Infrastructure.Services.Validator.ModelsValidator.Users
{
    public class UsersLogInViewModelValidator : AbstractValidator<UsersLogInViewModel>
    {
        public UsersLogInViewModelValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Your password cannot be empty.")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
        
            RuleFor(x => x.Email).NotEmpty().WithMessage("Your Email cannot be empty.")
                .MinimumLength(6).WithMessage("Your Email must be at least 6 characters.")
                .MaximumLength(30).WithMessage("Your Email must be at most 30 characters.")
                .EmailAddress().WithMessage("Your Email must be valid.");
        }
    }
}