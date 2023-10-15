using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AmazonShoping.Data;
using AmazonShoping.Models;

namespace AmazonShoping.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly AmazonShoping.Data.AmazonCLoneContextSQLite _context;

        public EditModel(AmazonShoping.Data.AmazonCLoneContextSQLite context)
        {
            _context = context;
        }

        [BindProperty] public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            // Fetch categories and set them in the ViewData
            ViewData["Categories"] = new SelectList(_context.Category, "Id", "Name");
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product =  await _context.Product.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            Product = product;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("**************");
                foreach (var modelStateValue in ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                Console.WriteLine("**************");
            

                // Print Product content to console
                Console.WriteLine("Product Information:");
                Console.WriteLine($"Id: {Product.Id}");
                Console.WriteLine($"Title: {Product.Title}");
                Console.WriteLine($"Description: {Product.Description}");
                Console.WriteLine($"Price: {Product.Price}");
                Console.WriteLine($"Quantity: {Product.Quantity}");
                Console.WriteLine($"ImageURL: {Product.ImageURL}");
                Console.WriteLine($"Category: {Product.Category}");
                Console.WriteLine($"Category ID: {Product.CategoryId}");
                Console.WriteLine($"Image: {Product.Image == null}");
                
                // Fetch categories and set them in the ViewData
                ViewData["Categories"] = new SelectList(_context.Category, "Id", "Name");
                return Page();
            }
            

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(long id)
        {
          return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
