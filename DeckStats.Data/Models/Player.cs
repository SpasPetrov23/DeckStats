namespace DeckStats.Data.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AvatarUrl { get; set; }
    public int MostPlayedDeckId { get; set; }
    
    public Deck MostPlayedDeck { get; set; }
    public ICollection<MatchResult> MatchResults { get; set; }
}
