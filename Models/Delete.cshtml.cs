using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ElectronicsStore.Data;

namespace ElectronicsStore.Models
{
    public class DeleteModel : PageModel
    {
        private readonly ElectronicsStore.Data.ApplicationDbContext _context;

        public DeleteModel(ElectronicsStore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Electronic Electronic { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronic = await _context.Electronics.FirstOrDefaultAsync(m => m.Id == id);

            if (electronic == null)
            {
                return NotFound();
            }
            else
            {
                Electronic = electronic;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var electronic = await _context.Electronics.FindAsync(id);
            if (electronic != null)
            {
                Electronic = electronic;
                _context.Electronics.Remove(Electronic);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
