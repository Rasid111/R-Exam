using Dapper;
using Microsoft.Data.SqlClient;
using Models;
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

var connectionString = "";

builder.Services.AddScoped<IQuestionService>((serviceProvider) => new QuestionService(new QuestionDapperRepository(connectionString)));
builder.Services.AddScoped<IQuestionRepository>((serviceProvider) => new QuestionJSONRepository());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(allowMVCOrigin);

app.MapControllers()
    .RequireCors(allowMVCOrigin);


app.Run();