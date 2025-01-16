var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapGet("/question", () =>
{
    return new {
        Question = "1 + 1",
        Answer1 = "1",
        Answer2 = "2",
        Answer3 = "3",
        Answer4 = "4"
    };
});

app.Run();