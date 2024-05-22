using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using WebApi.Domain.Repository;
using WebApi.Domain.Service;
using WebApi.Persistence;
using WebApi.Security.Handlers.Implementations;
using WebApi.Security.Handlers.Interfaces;
using WebApi.Security.Middleware;
using WebApi.Security.Settings;
using WebApi.Service;
using WebApi.Shared.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Backendc API",
        Description = "Backend RESTful API",
        TermsOfService = new Uri("https://google.com"),
        Contact = new OpenApiContact
        {
            Name = "ddddd.studio",
            Url = new Uri("https://google.studio")
        },
        License = new OpenApiLicense
        {
            Name = "google Resources License",
            Url = new Uri("https://google.com/license")
        }
    });
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme, Id="bearerAuth"
                }
            },
            Array.Empty<string>()
        }
    });


});

builder.Services.AddDbContext<AppDbContext>(options =>  options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection")));
builder.Services.AddScoped<IAlbumRepository,AlbumRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<ISongService, SongService>();
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddHostedService<DatabaseSeedingService>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(
    typeof(WebApi.Mapping.ModelToResourceProfile),
    typeof(WebApi.Mapping.ResourceToModelProfile)
);


var app = builder.Build();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseMiddleware<JwtMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
