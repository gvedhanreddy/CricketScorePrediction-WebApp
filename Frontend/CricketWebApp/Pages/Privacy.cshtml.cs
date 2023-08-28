using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CricketWebApp.Pages
{
    // creating the privacy data
    /// <summary>
    /// Add privacy page model
    /// </summary>
    public class PrivacyModel : PageModel
    {
        //logger to log into console
        private readonly ILogger<PrivacyModel> _logger;
        //privacy logger to log in console
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        // public void OnGet()
        // {
        // }
    }
}