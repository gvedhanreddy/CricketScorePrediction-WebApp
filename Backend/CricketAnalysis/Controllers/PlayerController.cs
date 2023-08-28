// Import necessary libraries
using System;
using CricketAnalysis.Data;
using CricketAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Define namespace
namespace CricketAnalysis.Controllers
{
    // Define the PlayerController class
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {

        // Declare private members for logger and player repository
        private readonly ILogger<PlayerController> _logger;
        private readonly IPlayerRepository _playerRepository;


        // Constructor to initialize logger and player repository
        public PlayerController(ILogger<PlayerController> logger, IPlayerRepository playerRepository)
        {
            _logger = logger;
            _playerRepository = playerRepository;
        }

        /// <summary>
        /// HTTP GET endpoint to retrieve all players
        /// </summary>
        /// <returns>All Players</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Player>))]
        public IActionResult GetPlayers()
        {
            try
            {
                var players = _playerRepository.GetPlayers();
                return Ok(players);
            }
            catch (Exception ex)
            {
                // Log the exception and throw a custom exception with a user-friendly error message
                // Alternatively, you can customize the error message based on the specific exception type
                // For example, if the exception is caused by a database connection error, you can return a custom error message indicating that the database is unavailable
                _logger.LogError($"An error occurred while getting players: {ex}");
                throw new Exception("An error occurred while getting players. Please try again later.");
            }
        }

        /// <summary>
        /// HTTP GET endpoint to retrieve a single player by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Player by id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Player))]
        [ProducesResponseType(404)]
        public IActionResult GetPlayerById(int id)
        {
            try
            {
                // Check if the id parameter is valid
                if (id <= 0)
                {
                    throw new ArgumentException("The id parameter must be greater than 0.", nameof(id));
                }

                Player player = _playerRepository.GetPlayerById(id);

                if (player == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(player);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and throw a custom exception with a user-friendly error message
                // Alternatively, you can customize the error message based on the specific exception type
                // For example, if the exception is caused by a database connection error, you can return a custom error message indicating that the database is unavailable
                Console.WriteLine($"An error occurred while getting player by id: {ex}");
                throw new Exception("An error occurred while getting player by id. Please try again later.");
            }
        }


        /// <summary>
        /// HTTP POST endpoint to add a new player
        /// </summary>
        /// <param name="player"></param>
        /// <returns>player</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AddPlayer([FromBody] Player player)
        {
            try
            {
                // Check if the player object is valid
                if (player == null)
                {
                    throw new ArgumentNullException(nameof(player), "The player object cannot be null.");
                }

                bool isAdded = _playerRepository.CreatePlayer(player);
                return isAdded ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                // Log the exception and throw a custom exception with a user-friendly error message
                // Alternatively, you can customize the error message based on the specific exception type
                Console.WriteLine($"An error occurred while adding player: {ex}");
                throw new Exception("An error occurred while adding player. Please try again later.");
            }
        }


        /// <summary>
        /// HTTP PUT endpoint to edit a player details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="player"></param>
        /// <returns>player</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult EditPlayer(int id, [FromBody] Player player)
        {
            try
            {
                // Check if the player object is valid
                if (player == null)
                {
                    throw new ArgumentNullException(nameof(player), "The player object cannot be null.");
                }

                // Validate the player object's properties
                if (string.IsNullOrWhiteSpace(player.PlayerName))
                {
                    throw new ArgumentException("The player name cannot be null or empty.", nameof(player.PlayerName));
                }

                bool isUpdated = _playerRepository.UpdatePlayer(player);

                if (!isUpdated)
                {
                    return NotFound($"No matching player found with id {id}.");
                }

                return Ok("Player details successfully updated");
            }
            catch (Exception ex)
            {
                // Log the exception and throw a custom exception with a user-friendly error message
                // Alternatively, you can customize the error message based on the specific exception type
                Console.WriteLine($"An error occurred while updating player: {ex}");
                throw new Exception("An error occurred while updating player. Please try again later.");
            }
        }


        /// <summary>
        /// HTTP DELETE endpoint to delete a player inforamtion by ID
        /// </summary>
        /// <param name="player"></param>
        /// <returns>player</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePlayerById(int id)
        {
            try
            {
                bool deleted = _playerRepository.DeletePlayerById(id);

                if (!deleted)
                {
                    return NotFound("No matching player found");
                }

                return Ok("Player deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting player with ID {id}: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the player");
            }
        }


        /// <summary>
        /// HTTP GET endpoint to retrieve the average runs scored by a player id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>averageRunsScored</returns>
        [HttpGet("{id}/average-run-scored")]
        public ActionResult<double> GetAverageRunsScored(int id)
        {
            (string playerName, double averageRunsScored) = _playerRepository.GetAverageRunsScored(id);

            if (playerName == "")
            {
                return NotFound();
            }

            return Ok($"{playerName} has an average of {averageRunsScored}");
        }

        /// <summary>
        /// Average runs scored by player name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The average runs scored by the player with name</returns>
        [HttpGet("average-run-scored/{name}")]
        public ActionResult<double> GetAverageRunsScored(string name)
        {
            var averageRunsScored = _playerRepository.GetAverageRunsScored(name);

            if (averageRunsScored == 0)
            {
                return NotFound();
            }

            return Ok(averageRunsScored);
        }

        /// <summary>
        /// HTTP GET endpoint to retrieve the average wickets taken per match by a player
        /// </summary>
        /// <param name="id"></param>
        /// <returns>averageWicketsTaken</returns>
        [HttpGet("{id}/average-wickets-taken-per-match")]
        public ActionResult<double> GetAverageWicketsTaken(int id)
        {
            (string playerName, double averageWicketsTaken) = _playerRepository.GetAverageWicketsTaken(id);

            if (playerName == "")
            {
                return NotFound();
            }

            return Ok($"{playerName} has an average of {averageWicketsTaken}");
        }


        /// <summary>
        /// Average runs scored by player name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The average runs scored by the player with name</returns>
        [HttpGet("average-wickets-Taken/{name}")]
        public ActionResult<double> GetAverageWicketsTaken(string name)
        {
            var averageWicketstaken = _playerRepository.GetAverageWicketsTaken(name);

            if (averageWicketstaken == 0)
            {
                return NotFound();
            }

            return Ok(averageWicketstaken);
        }


        /// <summary>
        /// HTTP GET endpoint to retrieve the top 10 run scorers
        /// </summary>
        /// <returns>top10RunScorers</returns>
        [HttpGet("top-10-run-scorers")]
        public IActionResult GetTop10RunScorers()
        {
            var top10RunScorers = _playerRepository.GetPlayers()
                .OrderByDescending(p => p.Runs)
                .Take(10)
                .ToList();

            return Ok(top10RunScorers);
        }

        /// <summary>
        /// Returns the list of top 10 players by wicket takers
        /// </summary>
        /// <returns>top10-wicket-takers</returns>
        [HttpGet("top-10-wicket-takers")]
        [ProducesResponseType(200, Type = typeof(List<Player>))]
        public IActionResult GetTopWicketTakers()
        {
            _logger.Log(LogLevel.Information, "Get top wicket takers");
            return Ok(_playerRepository.GetPlayersWithMostWickets(10));
        }

        /// <summary>
        /// Returns the list of top 10 players by strike-rate
        /// </summary>
        /// <returns>topStrikeRate</returns>
        [HttpGet("top-10-players-by-strike-rate")]
        [ProducesResponseType(200, Type = typeof(List<Player>))]
        public IActionResult GetTopTenPlayersByStrikeRate()
        {
            _logger.Log(LogLevel.Information, "Get top 10 players by strike rate");

            var topStrikeRate = _playerRepository.GetTopTenPlayersByStrikeRate();

            return Ok(topStrikeRate);
        }

        /// <summary>
        /// Returns the list of top 10 players by Highest Score
        /// </summary>
        /// <returns>HighestScored</returns>
        [HttpGet("top-10-players-by-Highestindividualscore")]
        [ProducesResponseType(200, Type = typeof(List<Player>))]
        public IActionResult GetTopTenPlayersByHighestscore()
        {
            _logger.Log(LogLevel.Information, "Get top 10 players by Highest indivual score");

            var HighestScored = _playerRepository.GetTopTenPlayersByHighestscore();

            return Ok(HighestScored);
        }

        /// <summary>
        /// Returns the list of players who scored most hundreds
        /// </summary>
        /// <returns>MostHundred</returns>
        [HttpGet("players-who-scored-MostHundreds")]
        [ProducesResponseType(200, Type = typeof(List<Player>))]
        public IActionResult GetMostHundredsscored()
        {
            _logger.Log(LogLevel.Information, "Get players who scored MostHundreds");

            var MostHundred = _playerRepository.GetMostHundredsscored();

            return Ok(MostHundred);
        }

        /// <summary>
        /// Returns the list of players who scored most fifties
        /// </summary>
        /// <returns>MostFifty</returns>
        [HttpGet("players-who-scored-MostFifty")]
        [ProducesResponseType(200, Type = typeof(List<Player>))]
        public IActionResult GetMostFiftysscored()
        {
            _logger.Log(LogLevel.Information, "Get players who scored fifty");

            var MostFifty = _playerRepository.GetMostFiftysscored();

            return Ok(MostFifty);
        }

        /// <summary>
        /// Returns the list of top 10 players who hit most sixes
        /// </summary>
        /// <returns>MostSixes</returns>
        [HttpGet("top-10-players-who-scored-MostSixes")]
        [ProducesResponseType(200, Type = typeof(List<Player>))]
        public IActionResult GetMostSixessscored()
        {
            _logger.Log(LogLevel.Information, "Get players who hit Most Sixes");

            var MostSixes = _playerRepository.GetMostSixessscored();

            return Ok(MostSixes);
        }

        /// <summary>
        /// Returns the list of top 10 players who scored most fours
        /// </summary>
        /// <returns>MostFours</returns>
        [HttpGet("top-10-players-who-scored-MostFours")]
        [ProducesResponseType(200, Type = typeof(List<Player>))]
        public IActionResult GetMostFoursscored()
        {
            _logger.Log(LogLevel.Information, "Get players who hit Most Fours");

            var MostFours = _playerRepository.GetMostFoursscored();

            return Ok(MostFours);
        }

        /// <summary>
        /// Searches for players by name
        /// </summary>
        /// <param name="search">The search query</param>
        /// <returns>The list of players matching the search query</returns>
        //[HttpGet("players-search")]
        [HttpGet("players-search")]
        public IActionResult SearchPlayersByName(string search)
        {
            try
            {
                // Check if the search parameter is null or empty
                if (string.IsNullOrEmpty(search))
                {
                    return BadRequest("The player name parameter is required.");
                }

                var players = _playerRepository.SearchPlayersByName(search);

                return Ok(players);
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 Internal Server Error status code
                // Alternatively, you can customize the error message and status code based on the specific exception type
                // For example, if the exception is caused by a database connection error, you can return a 503 Service Unavailable status code instead
                Console.WriteLine($"An error occurred while processing the SearchPlayersByName request: {ex}");
                return StatusCode(500, "An error occurred while processing the SearchPlayersByName request.");
            }
        }

        /// <summary>
        /// Retrieves a list of player names matching the specified search query
        /// </summary>
        /// <param name="search">The search query to filter the list of player names</param>
        /// <returns>A list of player names matching the specified search query</returns>
        //[HttpGet("playerNames")]
        [HttpGet("playerNames")]
        public IActionResult GetPlayerNames([FromQuery] string search)
        {
            try
            {
                // Check if the search parameter is null or empty
                if (string.IsNullOrEmpty(search))
                {
                    return BadRequest("The player name parameter is required.");
                }

                var playerNames = _playerRepository.GetPlayerNames(search);

                return Ok(playerNames);
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 Internal Server Error status code
                // Alternatively, you can customize the error message and status code based on the specific exception type
                // For example, if the exception is caused by a database connection error, you can return a 503 Service Unavailable status code instead
                Console.WriteLine($"An error occurred while processing the GetPlayerNames request: {ex}");
                return StatusCode(500, "An error occurred while processing the GetPlayerNames request.");
            }
        }


        /// <summary>
        /// Searches for players whose name contains the specified search query
        /// </summary>
        /// <param name="name">The search query to filter the list of players</param>
        /// <returns>The list of players whose name contains the specified search query</returns>
        // GET api/player/search/{name}
        [HttpGet("search/{name}")]
        public ActionResult<IEnumerable<Player>> Search(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest("The player name parameter is required.");
                }

                var players = _playerRepository.SearchPlayers(name);

               
                if (players == null || players.Count == 0)
                {
                    return NotFound();
                }

                return Ok(players);
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 Internal Server Error status code
                // Alternatively, you can customize the error message and status code based on the specific exception type
                // For example, if the exception is caused by a database connection error, you can return a 503 Service Unavailable status code instead
                Console.WriteLine($"An error occurred while processing the search request: {ex}");
                return StatusCode(500, "An error occurred while processing the search request.");
            }
        }

    }
}

