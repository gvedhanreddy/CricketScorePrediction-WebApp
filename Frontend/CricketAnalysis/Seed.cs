using System;
using CricketAnalysis.Data;
using CricketAnalysis.Models;

namespace CricketAnalysis
{
    /// <summary>
    /// Seed to database add initial data 
    /// </summary>
    public class Seed
    {
        private readonly DataContext dataContext;

        // Constructor that takes the DataContext object as a parameter
        public Seed(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        // Method that seeds the data in the database
        public void SeedDataContext()
        {
            //Check if the Player table in the database is empty
            if (!dataContext.Player.Any())
            {
                //Create a list of Player objects
                List<Player> players = new(){
                new Player {Id = 187, PlayerName="smiriti",Runs=3000},
                new Player {Id = 188, PlayerName="Hampreth",Runs=2000},
                };
                //Add the list of Player objects to the Player table in the database
                dataContext.Player.AddRange(players);
                //Save the changes made to the database
                //dataContext.SaveChanges();
            }
        }
    }
}

