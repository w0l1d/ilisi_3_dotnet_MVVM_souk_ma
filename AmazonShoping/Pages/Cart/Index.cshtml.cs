using System.Text.Json;
using AmazonShoping.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AmazonShoping.Pages.Cart;

public class IndexModel : PageModel
{
    private readonly AmazonShoping.Data.AmazonCLoneContextSQLite _context;


    public IndexModel(AmazonShoping.Data.AmazonCLoneContextSQLite context) {
        _context = context;
    }

    public Order Order { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var value = HttpContext.Session.GetString("cart_order");
        if (value == null)
        {
            Order = new Order();
            HttpContext.Session.SetString("cart_order", JsonSerializer.Serialize(Order));
        }
        else
        {
            Order = JsonSerializer.Deserialize<Order>(value);
        }
    }

}