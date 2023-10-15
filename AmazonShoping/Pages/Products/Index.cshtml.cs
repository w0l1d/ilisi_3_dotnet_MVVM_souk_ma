using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmazonShoping.Data;
using AmazonShoping.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AmazonShoping.Pages.Products {
    public class IndexModel : PageModel {
        private readonly AmazonShoping.Data.AmazonCLoneContextSQLite _context;


        public IndexModel(AmazonShoping.Data.AmazonCLoneContextSQLite context) {
            _context = context;
        }

        public IList<Product> Product { get; set; } = default!;
        [BindProperty(SupportsGet = true)] public string? SearchString { get; set; }
        [BindProperty(SupportsGet = true)] public long? FilterCategoryId { get; set; }

        public async Task OnGetAsync() {
            IQueryable<Product> productsQuery = _context.Product;

            if (!string.IsNullOrEmpty(SearchString)) {
                Console.WriteLine("SearchString is not null and is: " + SearchString);
                productsQuery = productsQuery.Where(p => p.Title.ToUpper().Contains(SearchString.ToUpper()));
                HttpContext.Session.SetString("SearchString", SearchString ?? "No Searching");
            }

            if (FilterCategoryId.HasValue) {
                Console.WriteLine("FilterCategoryId is not null and is: " + FilterCategoryId);
                productsQuery = productsQuery.Where(p => p.CategoryId == FilterCategoryId);
            }

            Product = await productsQuery.ToListAsync();

            ViewData["Categories"] = new SelectList(_context.Category, "Id", "Name");
        }
    }
}