using CricketAnalysis.Models;

namespace CricketAnalysis.Data
{
    /// <summary>
    /// Interface for the PlayerRepository
    /// </summary>
    public interface IPlayerRepository
    {
        // Method to retrieve all players from the database
        ICollection<Player> GetPlayers();

        // Method to retrieve a single player from the database using their ID
        Player GetPlayerById(int id);

        // Method to check if a player with the given ID exists in the database
        bool PlayerExists(int id);

        // Method to create a new player in the database
        bool CreatePlayer(Player player);

        // Method to update an existing player in the database
        bool UpdatePlayer(Player player);

        // Method to delete an existing player from the database
        //bool DeletePlayer(Player player);
        bool DeletePlayerById(int id);

        // Method to save changes made to the database
        bool Save();

         // Method to retrieve the average runs scored by a player with the given ID
        (string playerName, int averageRunsScored) GetAverageRunsScored(int Id);

        (string playerName, int averageRunsScored) GetAverageRunsScored(string playerName);

        //double GetAverageRunsScored(string playerName);

        // Method to retrieve the average wickets taken by a player with the given ID
        (string playerName, int averageWicketsTaken) GetAverageWicketsTaken(int Id);
        (string playerName, int averageWicketsTaken) GetAverageWicketsTaken(string playerName);

        // Method to retrieve the top 10 players with the most runs scored
        // List<Player> GetTop10PlayersByRuns();
        List<object> GetTop10PlayersByRuns();

        // Method to retrieve a specified number of players with the most wickets taken
        ICollection<Player> GetPlayersWithMostWickets(int count);

        // Method to retrieve the top 10 players with the highest strike rate
        List<Player> GetTopTenPlayersByStrikeRate();

        // Method to retrieve the top 10 players with the highestscore
        List<Player> GetTopTenPlayersByHighestscore();

        // Method to retrieve the top 10 players with the most hundreds scored
        List<Player> GetMostHundredsscored();

        // Method to retrieve the top 10 players with the most fifties scored
        List<Player> GetMostFiftysscored();

        // Method to retrieve the top 10 players with the most sixes scored
        List<Player> GetMostSixessscored();

        // Method to retrieve the top 10 players with the most fours scored
        List<Player> GetMostFoursscored();

        // Method to search the player using their name
        ICollection<Player> SearchPlayersByName(string search);

        // Method to search the players using their part of the name
        ICollection<string> GetPlayerNames(string searchString);

        // Method to search the players using their name
        ICollection<Player> SearchPlayers(string name);

        //Method to calculate the Boundary Percentage by Name
        List<PlayerBoundaryPercentage> CalculateBoundaryPercentagebyName(string name);

    }
}