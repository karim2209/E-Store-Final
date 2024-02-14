using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ElectronicsStore.Data;

namespace ElectronicsStore.Models
{
    public class CreateModel : PageModel
    {
        private readonly ElectronicsStore.Data.ApplicationDbContext _context;

        public CreateModel(ElectronicsStore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName");
            return Page();
        }

        [BindProperty]
        public Electronic Electronic { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Electronics.Add(Electronic);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
