namespace PokemoneChallenge.Domain.Exceptions;

public class PokemonBaseException : Exception
{
    public PokemonBaseException(string message) : base(message)
    {
    }
}