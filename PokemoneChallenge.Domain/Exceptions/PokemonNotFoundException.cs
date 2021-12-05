namespace PokemoneChallenge.Domain.Exceptions;

public class PokemonNotFoundException : PokemonBaseException
{
    public string Name { get; }
    public PokemonNotFoundException(string name) : base($"Pokemon {name} not found.") => Name = name;
}