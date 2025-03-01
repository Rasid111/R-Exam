using R_Exam.WebApi.Infrastructre.Services;
using R_Exam.WebApi.Presentation.Middlewares;
using FluentValidation;
using R_Exam.WebApi.Core.Services.Base;
using R_Exam.WebApi.Core.Repositories.Base;
using R_Exam.WebApi.Infrastructre.Repositories;
using System.Runtime.Loader;
using System.Reflection;
using R_Exam.WebApi.Core.Models;
using R_Exam.WebApi.Infrastructre.Validators;

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

string connectionString = builder.Configuration.GetConnectionString("R-ExamDb") ?? throw new ArgumentNullException(nameof(connectionString), "Internal Error");

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