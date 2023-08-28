using System;
using CricketAnalysis.Models;

namespace CricketAnalysis.Data
{
    /// <summary>
    /// Repository class for player for All CRUD and analysis operations
    /// </summary>
    public class PlayerRepository:IPlayerRepository
    {
        // Create an empty list of players
        private List<Player> players = new List<Player>();

        // Create an instance of DataContext
        private DataContext _context;

        // Constructor that accepts an instance of DataContext
        public PlayerRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a collection of all the players
        /// </summary>
        /// <returns>A collection of all the players</returns>
        public ICollection<Player> GetPlayers()
        {
            return _context.Player.ToList();
        }

        /// <summary>
        /// Returns the player with the specified id
        /// </summary>
        /// <param name="id">The id of the player to return</param>
        /// <returns>The player with the specified id</returns>
        public Player GetPlayerById(int id)
        {
            return _context.Player.Where(player => player.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Checks if a player with the specified id exists
        /// </summary>
        /// <param name="id">The id of the player to check for existence</param>
        /// <returns>True if the player with the specified id exists, false otherwise</returns>
        public bool PlayerExists(int id)
        {
            return _context.Player.Any(player => player.Id == id);
        }

        /// <summary>
        /// Updates the specified player in the database
        /// </summary>
        /// <param name="player">The player to update</param>
        /// <returns>True if the update was successful, false otherwise</returns>
        public bool UpdatePlayer(Player player)
        {
            _context.Update(player);
            return Save();
        }

        /// <summary>
        /// Adds the specified player to the database
        /// </summary>
        /// <param name="player">The player to add</param>
        /// <returns>True if the add was successful, false otherwise</returns>
        public bool CreatePlayer(Player player)
        {
            int nextId = _context.Player.Any() ? _context.Player.Max(p => p.Id) + 1 : 1;
            player.Id = nextId;
            _context.Add(player);
            return Save();
        }

        /// <summary>
        /// Deletes the specified player from the database
        /// </summary>
        /// <param name="player">The player to delete</param>
        /// <returns>True if the delete was successful, false otherwise</returns>
        public bool DeletePlayerById(int id)
        {
            Player player = _context.Player.Find(id);
            if (player == null)
            {
                return false;
            }
            _context.Player.Remove(player);
            return Save();
        }

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        /// <returns>True if the save was successful, false otherwise</returns>
        public bool Save()
        { 
                int saved = _context.SaveChanges();
                return saved == 1;
        }

         /// <summary>
        /// Returns the average runs scored by the player with the specified id
        /// </summary>
        /// <param name="id">The id of the player</param>
        /// <returns>The average runs scored by the player with the specified id</returns>
        public (string playerName, int averageRunsScored) GetAverageRunsScored(int Id)
        {
            var player = _context.Player
                .Where(p => p.Id == Id)
                .Select(p => new { p.PlayerName, p.Inns, p.Runs })
                .SingleOrDefault();

            // if (player == null || player.Inns == 0)
            if (player == null)

            {
                return ("", 0);
            }

            return (player.PlayerName, (int)player.Runs / player.Inns);
        }

        /// <summary>
        /// Average runs scored by player name
        /// </summary>
        /// <param name="playerName"></param>
        /// <returns>The average runs scored by the player with name</returns>
        public (string playerName, int averageRunsScored) GetAverageRunsScored(string playerName)
        {
            var player = _context.Player
                .Where(p => p.PlayerName == playerName && p.Inns != 0)
                .SingleOrDefault();

            if (player == null)
            {
                return ("",0);
            }

            return (player.PlayerName, (int)player.Runs / player.Inns);
            //return (double)player.Runs / player.Inns;
        }

        /// <summary>
        /// Returns the average wickets taken by the player with the specified id
        /// </summary>
        /// <param name="id">The id of the player</param>
        /// <returns>The average wickets taken by the player with the specified id</returns>returns>
        public (string playerName, int averageWicketsTaken) GetAverageWicketsTaken(int Id)
        {
            var player = _context.Player
                .Where(p => p.Id == Id)
                .Select(p => new { p.PlayerName, p.Inns, p.Wickets })
                .SingleOrDefault();

            //if (player == null || player.Inns == 0)
            if (player == null)
            {
                return ("", 0);
            }

            return (player.PlayerName, (int)player.Wickets / player.Inns);
        }

        ///// <summary>
        ///// Average runs scored by player name
        ///// </summary>
        ///// <param name="playerName"></param>
        ///// <returns>The average wickets taken by the player with name</returns>
        public (string playerName, int averageWicketsTaken) GetAverageWicketsTaken(string playerName)
        {
            var player = _context.Player
                .Where(p => p.PlayerName == playerName && p.Inns != 0)
                .SingleOrDefault();

            if (player == null)
            {
                return ("", 0);
            }

            return (player.PlayerName, (int)player.Wickets / player.Inns);
            //return (double)player.Wickets / player.Inns;
        }

        /// <summary>
        /// Returns a list of top 10 players sorted by the number of runs scored
        /// </summary>
        /// <returns>List<Player></returns>
        // public List<Player> GetTop10PlayersByRuns()
        //{
        //  return _context.Player.OrderByDescending(p => p.Runs).Take(10).ToList();
        //}

        public List<object> GetTop10PlayersByRuns()
        {
            var top10Players = _context.Player
                .OrderByDescending(p => p.Runs)
                .Take(10)
                .Select(p => new { p.PlayerName, p.MatchesPlayed, p.Runs })
                .ToList<object>();

            return top10Players;
        }

        /// <summary>
        /// Returns a collection of players with the most number of wickets taken
        /// </summary>
        /// <param name="count">The number of players to return</param>
        /// <returns>ICollection<Player></returns>
        public ICollection<Player> GetPlayersWithMostWickets(int count)
        {
            return _context.Player.OrderByDescending(p => p.Wickets).Take(count).ToList();
        }

        /// <summary>
        /// Returns a list of top 10 players who scored minimum 300 Runs sorted by their strike rate
        /// </summary>
        /// <returns>List<Player></returns>
        public List<Player> GetTopTenPlayersByStrikeRate()
        {
            return _context.Player.Where(p => p.Runs > 300).OrderByDescending(p => p.Strikerate).Take(10).ToList();
            // return _context.Player.OrderByDescending(p => p.Strikerate).Take(10).ToList();
        }

        /// <summary>
        /// Returns a list of top 10 players sorted by their highest score
        /// </summary>
        /// <returns>List<Player></returns>
        public List<Player> GetTopTenPlayersByHighestscore()
        {

            return _context.Player.OrderByDescending(p => p.HighestScore).Take(10).ToList();
        }

        // <summary>
        /// Returns a list of top 10 players who scored the most number of hundreds
        /// </summary>
        /// <returns>List<Player></returns>
        public List<Player> GetMostHundredsscored()
        {

            // return _context.Player.OrderByDescending(p => p.Hundred).Take(4).ToList();
            return _context.Player.Where(p => p.Hundred >= 1).ToList();
        }

        /// <summary>
        /// Returns a list of top 10 players who scored the most number of fifties
        /// </summary>
        /// <returns>List<Player></returns>
        public List<Player> GetMostFiftysscored()
        {

            return _context.Player.Where(p => p.Fifty >= 1).ToList();
        }

        /// <summary>
        /// Returns a list of top 10 players who hit the most number of sixes
        /// </summary>
        /// <returns>List<Player></returns>
        public List<Player> GetMostSixessscored()
        {

            return _context.Player.OrderByDescending(p => p.Sixes).Take(10).ToList();
        }

        /// <summary>
        /// Returns a list of top 10 players who hit the most number of fours
        /// </summary>
        /// <returns>List<Player></returns>
        public List<Player> GetMostFoursscored()
        {

            return _context.Player.OrderByDescending(p => p.Fours).Take(10).ToList();
        }

        /// <summary>
        /// Searches for players whose name contains the specified search query
        /// </summary>
        /// <param name="search">The search query to filter the list of players</param>
        /// <returns>The list of players whose name contains the specified search query</returns>
        public ICollection<Player> SearchPlayersByName(string search)
        {
            try
            {
                // Check if the search parameter is null or empty
                if (string.IsNullOrEmpty(search))
                {
                    throw new ArgumentException("The player name parameter is required.", nameof(search));
                }

                var players = _context.Player
                    .Where(p => p.PlayerName.Contains(search))
                    .ToList();

                return players;
            }
            catch (Exception ex)
            {
                // Log the exception and throw a custom exception with a user-friendly error message
                // Alternatively, you can customize the error message based on the specific exception type
                // For example, if the exception is caused by a database connection error, you can return a custom error message indicating that the database is unavailable
                Console.WriteLine($"An error occurred while searching for players by name: {ex}");
                throw new Exception("An error occurred while searching for players by name. Please try again later.");
            }
        }


        /// <summary>
        /// Retrieves a list of player names whose name contains the specified search query
        /// </summary>
        /// <param name="searchString">The search query to filter the list of player names</param>
        /// <returns>The list of player names whose name contains the specified search query</returns>
        public ICollection<string> GetPlayerNames(string searchString)
        {
            try
            {
                // Check if the searchString parameter is null or empty
                if (string.IsNullOrEmpty(searchString))
                {
                    throw new ArgumentException("The player name parameter is required.", nameof(searchString));
                }

                var playerNames = _context.Player
                    .Where(p => p.PlayerName.Contains(searchString))
                    .Select(p => p.PlayerName)
                    .ToList();

                return playerNames;
            }
            catch (Exception ex)
            {
                // Log the exception and throw a custom exception with a user-friendly error message
                // Alternatively, you can customize the error message based on the specific exception type
                // For example, if the exception is caused by a database connection error, you can return a custom error message indicating that the database is unavailable
                Console.WriteLine($"An error occurred while getting player names: {ex}");
                throw new Exception("An error occurred while getting player names. Please try again later.");
            }
        }


        /// <summary>
        /// Searches for players whose name contains the specified search query
        /// </summary>
        /// <param name="name">The search query to filter the list of players</param>
        /// <returns>The list of players whose name contains the specified search query</returns>
        public ICollection<Player> SearchPlayers(string name)
        {
            try
            {
                // Check if the name parameter is null or empty
                if (string.IsNullOrEmpty(name))
                {
                    throw new ArgumentException("The player name parameter is required.", nameof(name));
                }

                var players = _context.Player
                    .Where(p => p.PlayerName.Contains(name))
                    .ToList();

                return players;
            }
            catch (Exception ex)
            {
                // Log the exception and throw a custom exception with a user-friendly error message
                // Alternatively, you can customize the error message based on the specific exception type
                // For example, if the exception is caused by a database connection error, you can return a custom error message indicating that the database is unavailable
                Console.WriteLine($"An error occurred while searching for players by name: {ex}");
                throw new Exception("An error occurred while searching for players by name. Please try again later.");
            }
        }

        // /// <summary>
        // ///Calculate boundary percentage for each player
        // /// </summary>
        // /// <returns>Boundary Percentage</returns>
        // public List<PlayerBoundaryPercentage> CalculateBoundaryPercentage()
        // {
        //     // Calculate boundary percentage for each player
        //     List<PlayerBoundaryPercentage> playerBoundaryPercentages = new List<PlayerBoundaryPercentage>();

        //     // Search players and iterarte through each player available
        //     foreach (var player in players)
        //     {
        //         if (player.Runs.HasValue && player.Runs.Value > 0)
        //         {
        //             //calculate the boundary percentage for the each player
        //             double boundaryPercentage = ((double)(player.Fours + player.Sixes) / player.Runs.Value) * 100;
        //             PlayerBoundaryPercentage playerBoundaryPercentage = new PlayerBoundaryPercentage
        //             {
        //                 PlayerId = player.Id,
        //                 PlayerName = player.PlayerName,
        //                 BoundaryPercentage = boundaryPercentage
        //             };
        //             playerBoundaryPercentages.Add(playerBoundaryPercentage);
        //         }
        //     }

        //     return playerBoundaryPercentages;
        // }
        /// <summary>
        /// This Method searches by player name and Calculate boundary percentage for each player
        /// </summary>
        /// <param name="search"></param>
        /// <returns>boundarypercentage</returns>
        // Calculate boundary percentage for each player based on player name
        public List<PlayerBoundaryPercentage> CalculateBoundaryPercentagebyName(string search)
        {
            List<PlayerBoundaryPercentage> playerBoundaryPercentages = new List<PlayerBoundaryPercentage>();

            // Get Players from the Database
            List<Player> players = GetPlayers().ToList();
            //List<Player> players = _playerRepository.GetPlayers();

            // Filter players based on search parameter
            if (!string.IsNullOrEmpty(search))
            {
                players = players.Where(p => p.PlayerName.Contains(search)).ToList();
            }

            // Search players and iterarte through each player available
            foreach (var player in players)
            {
                if (player.Runs.HasValue && player.Runs.Value > 0)
                {
                    //calculate the boundary percentage for the each player
                    double boundaryPercentage = ((double)(player.Fours + player.Sixes) / player.Runs.Value) * 100;
                    PlayerBoundaryPercentage playerBoundaryPercentage = new PlayerBoundaryPercentage
                    {
                        PlayerId = player.Id,
                        PlayerName = player.PlayerName,
                        BoundaryPercentage = Math.Round(boundaryPercentage,0),
                    };
                    playerBoundaryPercentages.Add(playerBoundaryPercentage);
                }
            }

            return playerBoundaryPercentages;
        }

    }
}

