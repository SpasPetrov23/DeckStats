using DeckStats.Data.Enums;

namespace DeckStats.Data.Models;

public class MatchResult
{
    public int Id { get; set; }
    public MatchResultType Result { get; set; }
    public int CurrentWins { get; set; }
    public int CurrentLosses { get; set; }
    public int CurrentRatio { get; set; }
    public int CurrentPoints { get; set; }
    public int PointsChange { get; set; }
    public int DeckId { get; set; }
    public int PlayerId { get; set; }
    public int MatchupId { get; set; }
    
    public Deck Deck { get; set; }
    public Player Player { get; set; }
    public Matchup Matchup { get; set; }
}
