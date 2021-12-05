using MediatR;

namespace PokemoneChallenge.Domain.Events;

public record PokemonRequested(string message) : INotification
{
}