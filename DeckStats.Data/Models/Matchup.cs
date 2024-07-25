namespace DeckStats.Data.Models;

public class Matchup
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    
    public ICollection<MatchResult> MatchResults { get; set; }
}
