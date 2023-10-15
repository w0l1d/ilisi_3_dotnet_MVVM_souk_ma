using AmazonShoping.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AmazonShoping.Pages.Products; 

public class AdminModel : PageModel {
    
    private readonly AmazonShoping.Data.AmazonCLoneContextSQLite _context;

    public AdminModel(AmazonShoping.Data.AmazonCLoneContextSQLite context)
    {
        _context = context;
    }

    public IList<Product> Product { get;set; } = default!;

    public async Task OnGetAsync()
    {
        if (_context.Product != null)
        {
            Product = await _context.Product.ToListAsync();
        }
    }
}