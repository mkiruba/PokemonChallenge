namespace PokemoneChallenge.Domain.Exceptions;

public class PokemonFailedException : PokemonBaseException
{
    public PokemonFailedException() : base($"Failed to retrieve Pokemon.")
    {
    }
}