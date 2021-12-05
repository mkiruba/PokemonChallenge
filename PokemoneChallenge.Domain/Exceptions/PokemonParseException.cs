namespace PokemoneChallenge.Domain.Exceptions;

public class PokemonParseException : PokemonBaseException
{
    public PokemonParseException() : base($"Failed to parse Pokemon response.")
    {
    }
}