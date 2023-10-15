using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmazonShoping.Data;
using AmazonShoping.Models;

namespace AmazonShoping.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly AmazonShoping.Data.AmazonCLoneContextSQLite _context;

        public DetailsModel(AmazonShoping.Data.AmazonCLoneContextSQLite context)
        {
            _context = context;
        }

      public Product Product { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }
    }
}
