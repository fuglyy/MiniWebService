using MiniWebService.Middlewares;
using MiniWebService.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMovieRepository, InMemoryMovieRepository>();


var app = builder.Build();

app.UseMiddleware<RequestIdMiddleware>();
app.UseMiddleware<TimingAndLogMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();


