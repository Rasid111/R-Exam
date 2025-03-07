using R_Exam.Infrastructre.Services;
using R_Exam.Presentation.Middlewares;
using FluentValidation;
using R_Exam.Domain.Repositories;
using R_Exam.Infrastructre.Repositories;
using R_Exam.Infrastructre.Validators;
using R_Exam.Domain.Models;
using R_Exam.Application.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var allowMVCOrigin = "_allowMVCOrigin";

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: allowMVCOrigin, policy =>
            {
                policy.WithOrigins("http://localhost:5240")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });
        });

        #pragma warning disable CA2208 // Правильно создавайте экземпляры исключений аргументов
        string connectionString = builder.Configuration.GetConnectionString("R-ExamDb") ?? throw new ArgumentNullException(nameof(connectionString), "Internal Error");
        #pragma warning restore CA2208 // Правильно создавайте экземпляры исключений аргументов

        builder.Services.AddScoped<IQuestionService, QuestionService>();
        builder.Services.AddScoped<IValidator<Question>, QuestionValidator>();
        builder.Services.AddScoped<ILogService, LogService>();
        builder.Services.AddScoped<IQuestionRepository>((serviceProvider) => new QuestionDapperRepository(connectionString));
        builder.Services.AddScoped<ILogRepository>((serviceProvider) => new LogDapperRepository(connectionString));

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors(allowMVCOrigin);

        app.MapControllers()
            .RequireCors(allowMVCOrigin);

        app.UseMiddleware<LoggingMiddleware>();
        app.UseMiddleware<ExceptionHandlerMiddleware>();

        app.Run();
    }
}