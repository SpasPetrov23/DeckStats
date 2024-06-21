namespace DeckStats.Data.Models;

public class Deck
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
}