using CricketAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace CricketWebApp.Pages.Cricket
{
    /// <summary>
    /// Edit player stats from the database
    /// </summary>
    public class EditModel : PageModel
    {

        HttpClient client;
        // Constructor for the EditModel class to initialize the HttpClient object
        public EditModel()
        {
            client = new HttpClient();

        }
        // Model binding property for the Player object
        [BindProperty]
        public Player Player { get; set; }
        //public void OnGet()
        //{
        //}
        // Get the player data to edit
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
             // Send GET request to the API to get player by id
            Player = client.GetFromJsonAsync<Player>("https://localhost:7043/Player/" + id).Result;


            if (Player == null)
            {
                return NotFound();
            }
            return Page();
        }

        // Update the edited player data
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Send PUT request to the API to update player by id with the edited data
                var res = await client.PutAsJsonAsync<Player>("https://localhost:7043/Player/" + id, Player);

                if (res.IsSuccessStatusCode)
                {
                    ViewData["message"] = "Changes Saved Sucessfully";
                }
                else
                {
                    ViewData["message"] = "Changes not Saved";
                }

            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return RedirectToPage("./Index");
        }


    }
}

