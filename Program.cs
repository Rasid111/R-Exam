using Dapper;
using Microsoft.Data.SqlClient;
using R_Exam.Models;
using R_Exam.Repositories;
using R_Exam.Repositories.Base;
using R_Exam.Services;
using R_Exam.Services.Base;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var connectionString = "connectionString";

builder.Services.AddScoped<IQuestionService>((serviceProvider) => new QuestionService(new QuestionDapperRepository(connectionString)));
builder.Services.AddScoped<IQuestionRepository>((serviceProvider) => new QuestionJSONRepository());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();


app.Run();