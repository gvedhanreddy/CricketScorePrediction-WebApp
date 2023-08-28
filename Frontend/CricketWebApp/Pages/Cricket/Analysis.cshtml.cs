using CricketAnalysis.Data;
using CricketAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CricketWebApp.Pages.Cricket
{
    /// <summary>
    /// Various Analysis of the players like search by player name and other Analysis
    /// </summary>
    public class AnalysisModel : PageModel
    {
        private readonly IPlayerRepository _playerRepository;

        public AnalysisModel(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        // Bind properties for player ID, player name, and average runs scored/wickets taken
        [BindProperty]
        public int PlayerId { get; set; }

        [BindProperty]
        public string PlayerName { get; set; }

        [BindProperty]
        public double AverageRunsScored { get; set; }

        [BindProperty]
        public double AverageWicketsTaken { get; set; }

        // List of all players in the database
        public List<Player> Players { get; set; }
        //public void OnGet()
        //{
        //}

        // This method is called when the page is loaded with a GET request
        public IActionResult OnGet()
        {
            // Get all players from the database and store them in the Players property
            Players = (List<Player>)_playerRepository.GetPlayers();
             // Return the page
            return Page();
        }

        // This method is called when the page is submitted with a POST request
        public IActionResult OnPost()
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return Page();
            }

          // If a player name is entered, get their average runs scored and wickets taken
        if (!string.IsNullOrEmpty(PlayerName))
        {
            AverageRunsScored = _playerRepository.GetAverageRunsScored(PlayerName);
            AverageWicketsTaken = _playerRepository.GetAverageWicketsTaken(PlayerName);
        }
        // If a player ID is entered, get the player name, average runs scored, and average wickets taken
        else if (PlayerId > 0)
        {
            (PlayerName, AverageRunsScored) = _playerRepository.GetAverageRunsScored(PlayerId);
            (PlayerName, AverageWicketsTaken) = _playerRepository.GetAverageWicketsTaken(PlayerId);
        }

        // Get all players from the database and store them in the Players property
        Players = (List<Player>)_playerRepository.GetPlayers();

        // Return the page
        return Page();
    }
}
}




Regenerate response