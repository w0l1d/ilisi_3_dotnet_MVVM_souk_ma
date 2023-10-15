using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmazonShoping.Data;
using AmazonShoping.Models;

namespace AmazonShoping.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly AmazonShoping.Data.AmazonCLoneContextSQLite _context;

        public CreateModel(AmazonShoping.Data.AmazonCLoneContextSQLite context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Categories"] = new SelectList(_context.Category, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Category == null || Category == null)
            {
                return Page();
            }

            _context.Category.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
