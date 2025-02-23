using Dapper;
using Microsoft.Data.SqlClient;
using Models;
using R_Exam.Middlewares;
using R_Exam.Repositories;
using R_Exam.Repositories.Base;
using R_Exam.Services;
using R_Exam.Services.Base;
using System.Data;

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

var connectionString = "Server=DESKTOP-EINAMOG\\MS_SQL_SERVER;Database=r_exam;User Id=sa;Password=R24002004;TrustServerCertificate=True;Trusted_Connection=True;";

builder.Services.AddScoped<IQuestionService, QuestionService>();
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