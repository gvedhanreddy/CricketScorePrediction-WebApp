using System;
using CricketAnalysis.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CricketAnalysis.Data
{

    /// <summary>
    /// DataContext is the class that handles database access and querying using Entity Framework Core.
    /// </summary>
    public class DataContext:DbContext
    {
        // DataContext constructor that accepts DbContextOptions parameter is used to configure the database context instance.
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //DbSet is a collection of entities that can be queried using LINQ syntax and can be used to perform CRUD operations.
        public DbSet<Player> Player { get; set; }


    }
}

