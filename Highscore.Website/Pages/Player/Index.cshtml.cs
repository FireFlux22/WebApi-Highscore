using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Highscore.Website.Data;
using Highscore.Website.Data.Entities;

namespace Highscore.Website.Pages.Player
{
    public class IndexModel : PageModel
    {
        private readonly Highscore.Website.Data.ApplicationDbContext _context;

        public IndexModel(Highscore.Website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.Entities.Player> Player { get;set; }

        public async Task OnGetAsync()
        {
            Player = await _context.Player.ToListAsync();
        }
    }
}
