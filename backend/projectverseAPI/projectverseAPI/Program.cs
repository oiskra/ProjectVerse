using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using projectverseAPI.Data;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;
using projectverseAPI.Services;
using projectverseAPI.Validators.Authentication;
using projectverseAPI.Validators.Collaboration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectVerseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) 
);
builder.Services.AddControllers();
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
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        var jwtConfig = builder.Configuration.GetSection("JwtConfig");

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
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin",
        policy => {
            policy.RequireAuthenticatedUser();
            policy.RequireRole(UserRoles.Admin);
        });
});

ValidatorOptions.Global.LanguageManager.Enabled = false;

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
