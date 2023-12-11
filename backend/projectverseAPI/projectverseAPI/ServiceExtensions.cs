using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using projectverseAPI.Constants;
using projectverseAPI.Data;
using projectverseAPI.Handlers;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;
using projectverseAPI.Services;
using projectverseAPI.Validators.Authentication;
using projectverseAPI.Validators.Collaboration;
using projectverseAPI.Validators.Post;
using projectverseAPI.Validators.Project;
using projectverseAPI.Validators.User;
using System.Text;

namespace projectverseAPI
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddFluentValidationExtension(this IServiceCollection services)
        {
            ValidatorOptions.Global.LanguageManager.Enabled = false;
            ValidatorOptions.Global.PropertyNameResolver = (type, member, expression) =>
            {
                if (member != null)
                {
                    return char.ToLower(member.Name[0]) + member.Name[1..];
                }
                return null;
            };

            services
                .AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<CreateCollaborationDTOValidator>()
                .AddValidatorsFromAssemblyContaining<CreateCollaborationPositionDTOValidator>()
                .AddValidatorsFromAssemblyContaining<UpdateCollaborationDTOValidator>()
                .AddValidatorsFromAssemblyContaining<UserRegisterDTOValidator>()
                .AddValidatorsFromAssemblyContaining<UserLoginDTOValidator>()
                .AddValidatorsFromAssemblyContaining<RefreshRequestDTOValidator>()
                .AddValidatorsFromAssemblyContaining<CreateProjectRequestDTOValidator>()
                .AddValidatorsFromAssemblyContaining<UpdateProjectRequestDTOValidator>()
                .AddValidatorsFromAssemblyContaining<CreatePostCommentRequestDTOValidator>()
                .AddValidatorsFromAssemblyContaining<UpdatePostCommentRequestDTOValidator>()
                .AddValidatorsFromAssemblyContaining<UpdateUserRequestDTOValidator>();

            return services;
        }

        public static IServiceCollection AddAuthenticationExtension(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                var jwtConfig = configurationManager.GetSection("JwtConfig");

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

            return services;
        }

        public static IServiceCollection AddCorsExtension(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:5173")
                        .SetIsOriginAllowed(_ => true);
                    });
            });

            return services;
        }

        public static IServiceCollection AddIdentityExtension(this IServiceCollection services)
        {
            services
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

            return services;
        }

        public static IServiceCollection AddAuthorizationExtension(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Test",
                    policy => {
                        policy.RequireAuthenticatedUser();
                        policy.RequireRole(UserRoles.User);
                    });

                options.AddPolicy(PolicyNameConstants.SameAuthorPolicy, policy =>
                    policy.Requirements.Add(new SameAuthorRequirement()));

                options.AddPolicy(PolicyNameConstants.UpdateUserPolicy, policy =>
                    policy.Requirements.Add(new UpdateUserRequirement()));
            });

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services
                .AddScoped<ICollaborationApplicantsService, CollaborationApplicantsService>()
                .AddScoped<ICollaborationPositionService, CollaborationPositionService>()
                .AddScoped<ICollaborationService, CollaborationService>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IProjectService, ProjectService>()
                .AddScoped<IPostService, PostService>()
                .AddScoped<ICommentService, CommentService>()
                .AddScoped<IUserService, UserService>()
                .AddTransient<ITokenService, TokenService>()
                .AddTransient<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<IAuthorizationHandler, SameAuthorAuthorizationHandler>()
                .AddSingleton<IAuthorizationHandler, UpdateUserAuthorizationHandler>();

            return services;
        }

        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(cfg =>
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

            return services;
        }
    }
}
