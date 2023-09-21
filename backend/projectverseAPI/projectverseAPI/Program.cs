using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectverseAPI;
using projectverseAPI.Data;
using projectverseAPI.Validators;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectVerseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers(options =>
    options.Filters.Add(typeof(ValidateModelStateAttribute)));

builder.Services.AddCorsExtension();

builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true);

builder.Services
    .AddIdentityExtension()
    .AddAuthenticationExtension(builder.Configuration)
    .AddAuthorizationExtension();

builder.Services.AddFluentValidationExtension();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();

builder.Services.RegisterServices();

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
