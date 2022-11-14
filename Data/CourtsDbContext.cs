using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MTLcourts.Models;

namespace MTLcourts.Data
{
    public class CourtsDbContext : IdentityDbContext
    {
         public CourtsDbContext(DbContextOptions<CourtsDbContext> options) : base(options) {}
           public DbSet<Courts> Court { get; set; }

           public DbSet<Ratings> Ratings { get; set; }

            public DbSet<Comments> Comments { get; set; }

            public DbSet<Checkedin> Checkedin { get; set; }

            public DbSet<Intentions> Intentions { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(@"Data source=mtlcourtsdb.db");
        // }
    }
}