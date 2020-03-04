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
    public class DetailsModel : PageModel
    {
        private readonly Highscore.Website.Data.ApplicationDbContext _context;

        public DetailsModel(Highscore.Website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Data.Entities.Player Player { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Player = await _context.Player.FirstOrDefaultAsync(m => m.Id == id);

            if (Player == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
