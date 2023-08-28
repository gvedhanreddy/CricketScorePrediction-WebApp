using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CricketWebApp.Pages
{
    // creating the data
    /// <summary>
    /// Creating the index page with all Home page as cards
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        // Logger to log the in the console
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // public void OnGet()
        // {

        // }
    }
}