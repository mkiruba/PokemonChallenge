using MediatR;

namespace PokemoneChallenge.Domain.Events;

public record TranslationRequested(string message) : INotification
{
}