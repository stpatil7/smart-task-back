using DataAccessLayer.DbContexts;
using Domain.Dto;
using Domain.Dto.Validations;
using Domain.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Libraries.Interfaces;
using Libraries.Repositories;
using Libraries.Services;
using Microservices.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// -------------------- Add Services --------------------

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Memory Cache
builder.Services.AddMemoryCache();

// Authentication & Authorization
builder.Services.AddAuthorization();
builder.Services.Configure<JwtSettingsDto>(configuration.GetSection("JwtSettings"));

// Email
builder.Services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailTemplateRenderer, EmailTemplateRenderer>();

// Repositories & Services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddFluentValidationAutoValidation();

// Validation Services
builder.Services.AddValidatorsFromAssemblyContaining<UserRegistrationsValidation>();

// -------------------- Build App --------------------

var app = builder.Build();

// -------------------- Middleware --------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
