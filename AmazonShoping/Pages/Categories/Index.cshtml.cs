using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmazonShoping.Data;
using AmazonShoping.Models;

namespace AmazonShoping.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly AmazonShoping.Data.SoukMVVMContext _context;

        public IndexModel(AmazonShoping.Data.SoukMVVMContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Category != null)
            {
                Category = await _context.Category.ToListAsync();
            }
        }
    }
}
