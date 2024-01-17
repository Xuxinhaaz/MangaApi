using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MangaApi.Application.Authentication;
using MangaApi.Presentation.ViewModels.UsersViewModel;
using Microsoft.IdentityModel.Tokens;

namespace MangaApi.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(UsersViewModel model)
    {
        var JWTKey = _configuration["JWT:Key"];
        
        var handler = new JwtSecurityTokenHandler();

        var claims = new ClaimsIdentity(new []
        {
            new Claim(ClaimTypes.Name, model.UserName),
            new Claim(ClaimTypes.Email, model.UserEmail),
        });
        
        foreach (var roles in model.UserRoles)
        {
            claims.AddClaim(new Claim(ClaimTypes.Role, roles));
        }

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = claims,
            Issuer = "MangaApi",
            Expires = DateTime.UtcNow.AddMinutes(20),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JWTKey)),
                SecurityAlgorithms.HmacSha256Signature)
        };

         var createdToken = handler.CreateToken(tokenDescriptor);

         var strToken = handler.WriteToken(createdToken);

         return strToken;
    }
}