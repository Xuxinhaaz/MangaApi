using System.Text;
using MangaApi.Application;
using MangaApi.Application.Mapping.MangaMapping;
using MangaApi.Application.Mapping.UserMapping;
using MangaApi.Application.Mapping.UserMapping.UsersProfileMapping;
using MangaApi.Domain.Data;
using MangaApi.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = true,
        ValidIssuer = "MangaApi",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddAplication();
builder.Services.AddInfrastructure();

builder.Services.AddAutoMapper(typeof(DomainToUsersProfileDto));
builder.Services.AddAutoMapper(typeof(DomainToMangaDto));
builder.Services.AddAutoMapper(typeof(DomainToUserDto));

builder.Services.AddLogging(x => x.AddConsole());

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.MapControllers();

app.Run();