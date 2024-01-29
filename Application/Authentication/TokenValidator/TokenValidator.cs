using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MangaApi.Application.Authentication.TokenValidator;

public class TokenValidator : ITokenValidator
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<TokenValidator> _logger;

    public TokenValidator(IConfiguration configuration, ILogger<TokenValidator> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<TokenValidationResult> ValidateUsersJwt(string token)
    {
        try
        {
            var JWT = token.Replace("Bearer ", "");
    
            var validationParameters = new TokenValidationParameters()
            {
                ValidIssuer = "MangaApi",
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Key"]))
                
            };
            
            var validationResult = await new JwtSecurityTokenHandler().ValidateTokenAsync(JWT, validationParameters);
            
            return validationResult;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Token validation failed: {ex.Message}");
            return new TokenValidationResult { IsValid = false };
        }
    }
}