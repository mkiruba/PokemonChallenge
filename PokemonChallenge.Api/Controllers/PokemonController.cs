using MediatR;
using Microsoft.AspNetCore.Mvc;
using PokemonChallenge.Application.Queries;
using PokemoneChallenge.Domain.Entities;
using PokemoneChallenge.Domain.Exceptions;

namespace PokemonChallenge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly ILogger<PokemonController> _logger;
    private readonly IMediator _mediator;
    private const string errorMessage = "Failed to get Pokemon";
    public PokemonController(ILogger<PokemonController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("{name}", Name = "GetPokemon")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pokemon))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(string name)
    {
        try
        {
            var getPokemonByName = new GetPokemonByName { Name = name };
            var pokemon = await _mediator.Send(getPokemonByName);
            if (pokemon == null)
            { 
                return NotFound();
            }
            return Ok(pokemon);
        }
        catch (PokemonBaseException ex)
        {
            _logger.LogError(message: errorMessage, exception: ex, args: name);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(message: errorMessage, exception: ex, args: name);
            return StatusCode(500, errorMessage);
        }
    }
}
