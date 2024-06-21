using DeckStats.Data.Models;

namespace DeckStats.API.GraphQL;

public partial class Query
{
    public Deck GetDeck()
    {
        Deck result = new()
        {
            Name = "Chaos",
            Wins = 3,
            Losses = 2
        };
            
        return result;
    }
}