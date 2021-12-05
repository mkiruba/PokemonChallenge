using MediatR;
using PokemoneChallenge.Domain.Entities;

namespace PokemonChallenge.Application.Queries;

public class GetTranslatedPokemonByName : IRequest<Pokemon>
{
    public string Name { get; set; }
}
