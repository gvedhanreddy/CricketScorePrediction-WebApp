using CricketAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CricketWebApp.Pages.Cricket
{
    /// <summary>
    /// Create/ add new player to the database
    /// </summary>
    public class CreateModel : PageModel
    {
        HttpClient client;
        // Constructor for the CreateModel class to initialize the HttpClient object
        public CreateModel()
        {
            client = new HttpClient();

        }
        //public void OnGet()
        //{
        //}

        // GET method to display the page
        public IActionResult OnGet()
        {
            return Page();
        }

        // Model binding property for the Player object
        [BindProperty]
        public Player Player { get; set; }

        // POST method to handle the form submission
        public async Task<IActionResult> OnPostAsync()
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return Page();
            }
             // Send a POST request to the Player API to add the new Player object to the database
            var res = await client.PostAsJsonAsync<Player>("https://localhost:7043/Player", Player);
            // Redirect to the Index page after the POST request is completed
            return RedirectToPage("./Index");
        }
    }
}

