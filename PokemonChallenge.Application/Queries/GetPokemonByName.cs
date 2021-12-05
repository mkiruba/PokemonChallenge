using MediatR;
using PokemoneChallenge.Domain.Entities;

namespace PokemonChallenge.Application.Queries;

public class GetPokemonByName : IRequest<Pokemon>
{
    public string Name { get; set; }
}
