using System;
using System.Collections.Generic;
using System.Text;
using Highscore.Website.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Highscore.Website.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Game> Game { get; set; }
        public DbSet<Score> Score { get; set; }
        public DbSet<Player> Player { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
