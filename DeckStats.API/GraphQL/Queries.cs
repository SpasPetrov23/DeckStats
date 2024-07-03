using DeckStats.API.Handlers.DeckHandlers;
using DeckStats.API.Handlers.DeckHandlers.Dtos;
using MediatR;

namespace DeckStats.API.GraphQL;

public class Queries
{
    public async Task<List<DeckDto>> GetDecks([Service] ISender mediator)
    {
        GetDecks.Query query = new() { };
        GetDecks.Result result = await mediator.Send(query);

        return result.Decks;
    }
}
