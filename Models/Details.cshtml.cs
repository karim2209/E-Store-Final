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
    public class DetailsModel : PageModel
    {
        private readonly ElectronicsStore.Data.ApplicationDbContext _context;

        public DetailsModel(ElectronicsStore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
