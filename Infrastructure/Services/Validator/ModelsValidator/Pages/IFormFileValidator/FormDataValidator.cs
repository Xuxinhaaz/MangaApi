using FluentValidation;

namespace MangaApi.Infrastructure.Services.Validator.ModelsValidator.Pages.IFormFileValidator;

public class FormDataValidator : AbstractValidator<IFormFile>
{
    public FormDataValidator()
    {
        RuleFor(x => x.Length).NotNull().WithMessage("you must send a file.")
            .LessThanOrEqualTo(100).WithMessage("File size is larger than allowed.");
        
    }
}