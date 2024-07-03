using DeckStats.API.Handlers.UserHandlers;
using DeckStats.API.Handlers.UserHandlers.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeckStats.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<string> Login([FromBody] UserLoginDto dto)
    {
        Login.Mutation mutation = new()
        {
            Email = dto.Email,
            Password = dto.Password
        };

        Login.Result result = await _mediator.Send(mutation);

        return result.Token;
    }
}
