using PokemonReviewApp.Data;
using WEBAPIDEMO.Interfaces;
using WEBAPIDEMO.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddTransient<Seed>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDBContext<DataContext>;

var app = builder.Build();

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

internal class Seed
{
}