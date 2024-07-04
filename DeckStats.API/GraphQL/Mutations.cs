using AppAny.HotChocolate.FluentValidation;
using DeckStats.API.GraphQL.Inputs;
using DeckStats.API.Handlers.UserHandlers;
using DeckStats.Data.Models;
using MediatR;

namespace DeckStats.API.GraphQL;

public class Mutations
{
    public async Task<User> Register([Service] ISender mediator, [UseFluentValidation] RegisterInput input)
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
