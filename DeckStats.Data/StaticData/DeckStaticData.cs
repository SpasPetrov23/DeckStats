using DeckStats.Data.Enums;
using DeckStats.Data.Models;

namespace DeckStats.Data.StaticData;

public class DeckStaticData
{
    public static List<Deck> GetDeckStaticData()
    {
        Random rng = new();
        string[] deckNames =
        {
            "Chaos",
            "Dark Magician",
            "Dino True King",
            "Ice Barrier",
            "Insect",
            "Volcanic",
            "Beast Control",
            "Exodia",
            "Crystal Beasts",
            "Elemental HERO"
        };

        List<Deck> decks = new();
        
        for (int i = 1; i < 10; i++)
        {
            var rngWins = rng.Next(0, 15);
            var rngLosses = rng.Next(0, 15);
            var totalGames = rngWins + rngLosses;
            
            decks.Add(new()
            {
                Id = i,
                Name = deckNames[rng.Next(deckNames.Length)],
                Wins = rngWins,
                Losses = rngLosses,
                WinRatio = rngWins / totalGames,
                Points = (rngWins - rngLosses) * 250 / totalGames,
                Tier = TierType.Unranked,
                SubjectiveTier = TierType.Unranked,
                ImageUrl = ""
            });
        }

        return decks;
    }
}
