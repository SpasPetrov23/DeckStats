using System.ComponentModel.DataAnnotations;
using DeckStats.Data.Enums;

namespace DeckStats.Data.Models;

public class Deck
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Wins { get; set; }
    public int Losses { get; set; }
    public decimal WinRatio { get; set; }
    public decimal Points { get; set; }
    public TierType Tier { get; set; }
    public TierType SubjectiveTier { get; set; }
    public string ImageUrl { get; set; }
    public int MostPlayedByPlayerId { get; set; }
    
    public Player MostPlayedByPlayer { get; set; }
    public ICollection<MatchResult> MatchResults { get; set; }
}
