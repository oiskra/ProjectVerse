using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.Interfaces;
using projectverseAPI.Services;
using projectverseAPI.Validators.Collaboration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectVerseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) 
);
builder.Services.AddControllers();

ValidatorOptions.Global.LanguageManager.Enabled = false;

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCollaborationDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCollaborationDTOValidator>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICollaborationService, CollaborationService>();
 
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
