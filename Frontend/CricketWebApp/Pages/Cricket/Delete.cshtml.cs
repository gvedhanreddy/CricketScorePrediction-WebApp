using CricketAnalysis.Models; // Import the necessary model(s)
using Microsoft.AspNetCore.Mvc; // Import the necessary namespace(s)
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace CricketWebApp.Pages.Cricket
{
    /// <summary>
    /// Delete player from the database
    /// </summary>
    public class DeleteModel : PageModel // Define the DeleteModel class that inherits from PageModel
    {
        HttpClient client; // Declare a private field to hold an instance of HttpClient

        public DeleteModel() // Constructor
        {
            client = new HttpClient(); // Initialize the HttpClient instance
        }

        [BindProperty]
        public Player Player { get; set; } // Define a public property for the Player object to be deleted

        // Define an async method that handles GET requests to the page and retrieves the Player object to be deleted
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) // Check if the ID is null
            {
                return NotFound(); // Return a "not found" error
            }

            Player = await client.GetFromJsonAsync<Player>("https://localhost:7043/Player/" + id); // Retrieve the Player object from the API

            if (Player == null) // Check if the Player object is null
            {
                return NotFound(); // Return a "not found" error
            }

            return Page(); // Return the Delete page
        }

        // Define an async method that handles POST requests to the page and deletes the Player object
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) // Check if the ID is null
            {
                return NotFound(); // Return a "not found" error
            }

            Player = await client.GetFromJsonAsync<Player>("https://localhost:7043/Player/" + id); // Retrieve the Player object from the API

            if (Player != null) // Check if the Player object is not null
            {
                var res = await client.DeleteAsync("https://localhost:7043/Player/" + id); // Delete the Player object from the API
            }

            return RedirectToPage("./Index"); // Redirect the user to the Index page
        }
    }
}
