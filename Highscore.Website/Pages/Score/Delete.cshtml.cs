﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Highscore.Website.Data;
using Highscore.Website.Data.Entities;

namespace Highscore.Website.Pages.Score
{
    public class DeleteModel : PageModel
    {
        private readonly Highscore.Website.Data.ApplicationDbContext _context;

        public DeleteModel(Highscore.Website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Entities.Score Score { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Score = await _context.Score
                .Include(s => s.Player).FirstOrDefaultAsync(m => m.Id == id);

            if (Score == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Score = await _context.Score.FindAsync(id);

            if (Score != null)
            {
                _context.Score.Remove(Score);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
