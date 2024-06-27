using DeckStats.API.GraphQL.Types;
using DeckStats.Data;
using DeckStats.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DeckStats.API.GraphQL;

public partial class Query
{
    public async Task<List<DeckDto>> GetDecks([Service] DeckStatsDbContext dbContext)
    {
        var result = await dbContext.Decks.Select(x => new DeckDto
        {
            Name = x.Name,
            Wins = x.Wins,
            Losses = x.Losses,
        }).ToListAsync();
            
        return result;
    }
}