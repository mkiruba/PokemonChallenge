using MediatR;
using PokemonChallenge.Application.Services;
using PokemonChallenge.Infrastructure.Queries.Handlers;
using PokemonChallenge.Infrastructure.Services;
using PokemoneChallenge.Domain.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection
builder.Services.AddHttpClient<IPokemonService, PokemonService>(client => 
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
});
builder.Services.AddHttpClient<IYodaService, YodaService>(client =>
{
    client.BaseAddress = new Uri("https://api.funtranslations.com/");
});
builder.Services.AddHttpClient<IShakespeareService, ShakespeareService>(client =>
{
    client.BaseAddress = new Uri("https://api.funtranslations.com/");
});
builder.Services.AddScoped<IPokemonFactory, PokemonFactory>();

builder.Services.AddMediatR(typeof(PokemonQueryHandler).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
