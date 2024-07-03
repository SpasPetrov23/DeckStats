using DeckStats.API.GraphQL.Inputs;
using DeckStats.API.Handlers.UserHandlers;
using DeckStats.Data.Models;
using MediatR;

namespace DeckStats.API.GraphQL;

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
