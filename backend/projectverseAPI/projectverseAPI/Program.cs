using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using projectverseAPI.Data;
using projectverseAPI.Interfaces;
using projectverseAPI.Mapping;
using projectverseAPI.Models;
using projectverseAPI.Services;
using projectverseAPI.Validators;
using projectverseAPI.Validators.Authentication;
using projectverseAPI.Validators.Collaboration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectVerseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(options =>
    options.Filters.Add(typeof(ValidateModelStateAttribute)));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("https://localhost:5173")
            .SetIsOriginAllowed(_ => true);
        });
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true);

builder.Services
    .AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;

        options.User.RequireUniqueEmail = true;        
    })
    .AddEntityFrameworkStores<ProjectVerseContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(options =>
    {
        var jwtConfig = builder.Configuration.GetSection("JwtConfig");

        options.IncludeErrorDetails = true;
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ValidIssuer = jwtConfig["validIssuer"],
            ValidAudience = jwtConfig["validAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["secret"]))
        };

        options.Events = new JwtBearerEvents()
        {
            OnChallenge = ctx =>
            {
                Console.WriteLine("On Challenge");
                Console.WriteLine(ctx.AuthenticateFailure?.Message ?? "no msg");
                return Task.CompletedTask;
            },

            OnMessageReceived = msg =>
            {
                var token = msg?.Request.Headers.Authorization.ToString();
                string path = msg?.Request.Path ?? "";
                if (!string.IsNullOrEmpty(token))

                {
                    Console.WriteLine("Access token");
                    Console.WriteLine($"URL: {path}");
                    Console.WriteLine($"Token: {token}\r\n");
                }
                else
                {
                    Console.WriteLine("Access token");
                    Console.WriteLine("URL: " + path);
                    Console.WriteLine("Token: No access token provided\r\n");
                }
                return Task.CompletedTask;
            }
        ,

            OnTokenValidated = ctx =>
            {
                Console.WriteLine();
                Console.WriteLine("Claims from the access token");
                if (ctx?.Principal != null)
                {
                    foreach (var claim in ctx.Principal.Claims)
                    {
                        Console.WriteLine($"{claim.Type} - {claim.Value}");
                    }
                }
                Console.WriteLine();
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Test",
        policy => {
            policy.RequireAuthenticatedUser();
            policy.RequireRole(UserRoles.User);
        });
});

ValidatorOptions.Global.LanguageManager.Enabled = false;
ValidatorOptions.Global.PropertyNameResolver = (type, member, expression) =>
{
    if (member != null)
    {
        return char.ToLower(member.Name[0]) + member.Name[1..];
    }
    return null;
};

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCollaborationDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCollaborationDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterDTOValidator>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddScoped<ICollaborationService, CollaborationService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
