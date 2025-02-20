namespace DeckStats.API.Handlers.DeckHandlers.Dtos;

public class DeckDto
{
    public string Name { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public decimal WinRatio { get; set; }
    public decimal Points { get; set; }
    public string Tier { get; set; }
    public string SubjectiveTier { get; set; }
    public string ImageUrl { get; set; }
    public int MostPlayedByPlayerId { get; set; }
}
