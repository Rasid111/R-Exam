using R_Exam.Models;
using R_Exam.Repositories;
using R_Exam.Repositories.Base;
using R_Exam.Services;
using R_Exam.Services.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddSingleton<IQuestionRepository, QuestionJSONRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();