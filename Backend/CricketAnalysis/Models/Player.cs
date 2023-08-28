using System;
namespace CricketAnalysis.Models;

/// <summary>
/// This class represents a cricket player and their statistics
/// </summary>
public class Player
{

    // The name of the player
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // The number of matches played by the player
    public string PlayerName { get; set; }

    // The number of innings played by the player
    public int MatchesPlayed { get; set; }

    // The number of innings played by the player
    public int Inns { get; set; }

    // The number of times the player was not out
    public int NO { get; set; }

    // The total number of runs scored by the player
    public int? Runs { get; set; }

    // The highest score made by the player in a single innings
    public int HighestScore { get; set; }

    // The strike rate of the player
    public double Strikerate { get; set; }

    // The number of centuries (100 runs or more) scored by the player
    public int Hundred { get; set; }

    // The number of half-centuries (50-99 runs) scored by the player
    public int Fifty { get; set; }

    // The number of fours (balls hit to the boundary) scored by the player
    public int Fours { get; set; }

    // The number of sixes (balls hit over the boundary) scored by the player
    public int Sixes { get; set; }

    // The total number of wickets taken by the player
    public int Wickets { get; set; }

}
    
  

