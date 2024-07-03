using DeckStats.API.GraphQL.Handlers.UserHandlers;
using DeckStats.API.GraphQL.Handlers.UserHandlers.Inputs;
using DeckStats.Data.Models;
using MediatR;

namespace DeckStats.API.GraphQL.Handlers;

public class Mutations
{
    //TODO: Fluent Validation
    public async Task<User> Register([Service] ISender mediator, RegisterInput input)
    {
        Register.Mutation mutation = new()
        {
            Username = input.Username,
            Email = input.Email,
            Password = input.Password
        };
        Register.Result result = await mediator.Send(mutation);

        return result.User;
    }
}
