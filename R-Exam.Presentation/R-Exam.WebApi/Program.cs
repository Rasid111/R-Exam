using R_Exam.Infrastructre.Services;
using R_Exam.Presentation.Middlewares;
using FluentValidation;
using R_Exam.Domain.Repositories;
using R_Exam.Infrastructre.Repositories;
using R_Exam.Infrastructre.Validators;
using R_Exam.Domain.Models;
using R_Exam.Application.Services;
using R_Exam.Infrastructre.EntityFramework;
using R_Exam.Application.Mappers;
using R_Exam.Application.Dtos.Question;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

internal class Program
{
    private async static Task Main(string[] args)
    {
        var allowMVCOrigin = "_allowMVCOrigin";

        var builder = WebApplication.CreateBuilder(args);

        #pragma warning disable CA2208 // Правильно создавайте экземпляры исключений аргументов
        string connectionString = builder.Configuration.GetConnectionString("R-ExamDb") ?? throw new ArgumentNullException(nameof(connectionString), "Internal Error");
        string accountsConnectionString = builder.Configuration.GetConnectionString("R-ExamAccountsDb") ?? throw new ArgumentNullException(nameof(connectionString), "Internal Error");
        #pragma warning restore CA2208 // Правильно создавайте экземпляры исключений аргументов

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        builder.Services.AddDbContext<AccountDbContext>(options =>
            options.UseSqlServer(accountsConnectionString));

        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => { })
            .AddEntityFrameworkStores<AccountDbContext>();

        builder.Services.AddAuthentication(defaultScheme: CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(authenticationScheme: CookieAuthenticationDefaults.AuthenticationScheme, options => { options.Cookie.SameSite = SameSiteMode.None; });

        builder.Services.AddAuthorization();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: allowMVCOrigin, policy =>
            {
                policy.WithOrigins("http://localhost:5240")
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Services.AddScoped<IValidator<Question>, QuestionValidator>();
        builder.Services.AddScoped<ILogService, LogService>();
        builder.Services.AddScoped<IQuestionRepository>((serviceProvider) => new QuestionDapperRepository(connectionString));
        builder.Services.AddScoped<ILogRepository>((serviceProvider) => new LogDapperRepository(connectionString));

        builder.Services.AddMediatR(configuration => {
            configuration.RegisterServicesFromAssembly(typeof(QuestionCreateRequestDto).Assembly);
        });
        builder.Services.AddAutoMapper(typeof(Question_QuestionDto_Mapper).Assembly);

        var app = builder.Build();

        var serviceScope = app.Services.CreateScope();
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        await roleManager.CreateAsync(new IdentityRole { Name = "User" });

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors(allowMVCOrigin);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers()
            .RequireCors(allowMVCOrigin);

        app.UseMiddleware<LoggingMiddleware>();
        app.UseMiddleware<ExceptionHandlerMiddleware>();

        app.Run();
    }
}