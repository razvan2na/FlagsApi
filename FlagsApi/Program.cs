using FlagsApi.Constants;
using FlagsApi.Data;
using FlagsApi.Models;
using FlagsApi.Repositories;
using FlagsApi.Repositories.Implementations;
using FlagsApi.Services;
using FlagsApi.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

var _corsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Data
builder.Services.AddDbContext<ApplicationContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDb")));
builder.Services.AddDbContext<UserStoreContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserStoreDb")));

// Authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:7257";
        options.Audience = "flagsApi";
        options.RequireHttpsMetadata = false;
    });

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.Viewer, policy => policy.RequireClaim("scope", "flagsApi.read"));
    options.AddPolicy(Policies.Admin, policy => policy.RequireClaim(ClaimTypes.Role, Roles.Admin));
});

// CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: _corsPolicy, builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Mapper
builder.Services.AddAutoMapper(typeof(Program));

// Repositories
builder.Services.AddTransient<IRepositoryBase<Country>, CountryRepository>();
builder.Services.AddTransient<IRepositoryBase<User>, UserRepository>();
builder.Services.AddTransient<IRepositoryBase<UserCountry>, UserCountryRepository>();

// Services
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "FlagsAPI", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(_corsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
