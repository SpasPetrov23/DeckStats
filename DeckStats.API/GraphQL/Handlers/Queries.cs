using DeckStats.API.GraphQL.Handlers.DeckHandlers;
using DeckStats.API.GraphQL.Handlers.DeckHandlers.Dtos;
using MediatR;

namespace DeckStats.API.GraphQL.Handlers;

public class Queries
{
    public async Task<List<DeckDto>> GetDecks([Service] ISender mediator)
    {
        GetDecks.Query query = new() { };
        GetDecks.Result result = await mediator.Send(query);

        return result.Decks;
    }
}