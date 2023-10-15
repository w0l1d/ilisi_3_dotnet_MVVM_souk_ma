using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AmazonShoping.Data;
using AmazonShoping.Models;

namespace AmazonShoping.Pages.Products {
    public class CreateModel : PageModel {
        private readonly AmazonShoping.Data.AmazonCLoneContextSQLite _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(AmazonShoping.Data.AmazonCLoneContextSQLite context,
            IWebHostEnvironment webHostEnvironment) {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet() {
            // Fetch categories and set them in the ViewData
            ViewData["Categories"] = new SelectList(_context.Category, "Id", "Name");
            return Page();
        }

        [BindProperty] public Product Product { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid || _context.Product == null || Product == null) {
                // Fetch categories and set them in the ViewData
                ViewData["Categories"] = new SelectList(_context.Category, "Id", "Name");
                return Page();
            }

            if (Product.Image != null) {
                var fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(Product.Image.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "products_images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create)) {
                    await Product.Image.CopyToAsync(stream);
                }

                Product.ImageURL = fileName;
            }

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


           

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}