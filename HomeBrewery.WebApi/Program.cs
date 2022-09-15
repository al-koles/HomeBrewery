using System.Reflection;
using System.Text;
using FluentValidation;
using HomeBrewery.Application;
using HomeBrewery.Application.Common.Mappings;
using HomeBrewery.Application.Common.Settings;
using HomeBrewery.Application.Interfaces;
using HomeBrewery.Domain;
using HomeBrewery.Persistence;
using HomeBrewery.WebApi.Extensions;
using MDAreas.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddAutoMapper(conf =>
{
    conf.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    conf.AddProfile(new AssemblyMappingProfile(typeof(IHomeBreweryDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);

builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(policy => 
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddIdentity<HBUser, HBRole>(config =>
    {
        config.SignIn.RequireConfirmedAccount = false;
        config.SignIn.RequireConfirmedEmail = false;
        config.SignIn.RequireConfirmedPhoneNumber = false;
        config.Password.RequiredLength = 4;
        config.Password.RequireDigit = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<HomeBreweryDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers()
    .AddNewtonsoftJson(opt =>
        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var jwtSection = configuration.GetSection("JWT");
builder.Services.Configure<JwtBearerTokenSettings>(jwtSection);
var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>();

builder.Services.AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtBearerTokenSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtBearerTokenSettings.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtBearerTokenSettings.SecretKey)),
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);

    config.AddSecurityDefinition("AuthToken", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer",
        Name = "Authorization",
        Description = "Authorization token"
    });

    config.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "AuthToken"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

app.UseCustomExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MigrateDbContextIfNecessary<HomeBreweryDbContext>();

app.Run();
