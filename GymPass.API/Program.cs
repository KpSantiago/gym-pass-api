using System.Reflection;
using GymPass.API.Middlewares;
using GymPass.Application.CQRs;
using GymPass.Domain.Authorization;
using GymPass.Domain.Repositories;
using GymPass.Infrastructure.Authentication;
using GymPass.Infrastructure.DB;
using GymPass.Infrastructure.Repositories;
using GymPass.Infrastructure.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GymPassContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
);

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ICheckInsRepository, CheckInsRepository>();
builder.Services.AddScoped<IGymsRepository, GymsRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo() { Version = "v1", Title = "Gym-Pass API 2.0" });
    
    s.TagActionsBy(api => new List<string>()
    {
        api.GroupName ?? "[Warning]: Without name"
    });
   
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { In = ParameterLocation.Header, Description = "Por favor, insira 'Bearer' seguido de um espaço e do token JWT", Name = "Authorization", Type = SecuritySchemeType.ApiKey, Scheme = "Bearer" });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] {} } });
    
    s.DocInclusionPredicate((_, api) => true);
    
   });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Middlewares/
app.UseGlobalExceptionHandler();

app.Run();