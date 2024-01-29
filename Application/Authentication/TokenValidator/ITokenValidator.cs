using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace MangaApi.Application.Authentication.TokenValidator;

public interface ITokenValidator
{
    Task<TokenValidationResult> ValidateUsersJwt(string token);
}