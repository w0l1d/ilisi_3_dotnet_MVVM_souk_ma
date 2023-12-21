using System.Text.Json;
using AmazonShoping.Data;
using AmazonShoping.Models;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace AmazonShoping.Controllers;

public class CartController : Controller
{
    private readonly SoukMVVMContext _context;
    private readonly ILogger _logger;

    public CartController(SoukMVVMContext context, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }


    // GET
    public IActionResult Index()
    {
        return LocalRedirect("/Product/Index");
    }


    // GET
    public IActionResult OrderCart()
    {
        var username = HttpContext.Session.GetString("Username");

        if (string.IsNullOrEmpty(username))
        {
            _logger.Warning($"OrderCart: there is no user logged in");
            return LocalRedirect("/Auth/Index");
        }

        _logger.Information($"OrderCart: user with username: {username} is trying to order cart");

        //TODO :: if user is logged in then get user from db
        // var user = _context.User.FirstOrDefault(x => x.Username == username);
        // if (user == null)
        // {
        //     HttpContext.Session.SetString("Username", "");
        //     return LocalRedirect("/Auth/Index");
        // }


        //TODO :: if user is not logged in then redirect to login page

        // Retrieve the existing cart from the session
        var cartString = HttpContext.Session.GetString("cart_order");
        if (string.IsNullOrEmpty(cartString))
        {
            _logger.Warning($"OrderCart: there is no cart in session");
            return NotFound($"Product with Id not found in cart");
        }

        Order? cart = JsonSerializer.Deserialize<Order>(cartString);
        if (cart == null)
        {
            _logger.Warning($"OrderCart: cart could not be deserialized");
            return NotFound($"Product with Id not found in cart");
        }

        return RedirectToPage("Index", "Orders", cart);
    }

    public IActionResult RemoveFromCart(long productId)
    {
        // Retrieve the product based on productId (replace ProductService with your actual service or repository)
        Product? product = _context.Product.Find(productId);
        _logger.Information($"RemoveFromCart: user is trying to remove product with id: {productId} from cart");

        if (product == null)
        {
            _logger.Warning($"RemoveFromCart: product with id: {productId} not found");
            // Handle the case where the product with the given ID is not found
            //include product id in error
            return NotFound($"Product with Id {productId} not found");
        }

        // Retrieve the existing cart from the session
        var cartString = HttpContext.Session.GetString("cart_order");
        if (string.IsNullOrEmpty(cartString))
        {
            _logger.Warning($"RemoveFromCart: cart not found in session");
            return NotFound($"Product with Id {productId} not found in cart");
        }

        Order? cart = JsonSerializer.Deserialize<Order>(cartString);

        if (cart == null)
        {
            _logger.Warning($"RemoveFromCart: cart could not be deserialized");
            return NotFound($"Product with Id {productId} not found in cart");
        }


        var orderItem = cart!.OrderItems.FirstOrDefault(x => x.ProductId == productId, null);

        if (orderItem != null)
        {
            _logger.Warning($"RemoveFromCart: product with id: {productId} not found in cart");
            return NotFound($"Product with Id {productId} not found in cart");
        }

        // Increase the quantity of the existing product in the cart
        cart.OrderItems.Remove(orderItem);
        cart!.UpdatedAt = DateTime.Now;

        // Save the updated cart back to the session
        HttpContext.Session.SetString("cart_order", JsonSerializer.Serialize(cart));
        HttpContext.Session.SetInt32("CartItemCount", cart.OrderItems.Count);

        _logger.Information($"RemoveFromCart: product with id: {productId} removed from cart");
        Console.Write("redirecting to referer : 1" + Request.Headers["Referer"].ToString());
        // Redirect to the previous page
        return Redirect(Request.Headers["Referer"].ToString());
    }


// GET
    public IActionResult DecreaseFromCart(long productId)
    {
        // Retrieve the product based on productId (replace ProductService with your actual service or repository)
        Product? product = _context.Product.Find(productId);

        if (product == null)
        {
            // Handle the case where the product with the given ID is not found
            //include product id in error
            return NotFound($"Product with Id {productId} not found");
        }

        // Retrieve the existing cart from the session
        var cartString = HttpContext.Session.GetString("cart_order");
        if (string.IsNullOrEmpty(cartString))
        {
            return NotFound($"Product with Id {productId} not found in cart");
        }

        Order? cart = JsonSerializer.Deserialize<Order>(cartString);

        if (cart == null)
        {
            return NotFound($"Product with Id {productId} not found in cart");
        }


        var orderItem = cart!.OrderItems.FirstOrDefault(x => x.ProductId == productId, null);

        if (orderItem != null)
        {
            // Increase the quantity of the existing product in the cart
            orderItem.Quantity--;
            if (orderItem.Quantity == 0)
            {
                cart.OrderItems.Remove(orderItem);
            }

            cart!.UpdatedAt = DateTime.Now;

            // Save the updated cart back to the session
            HttpContext.Session.SetString("cart_order", JsonSerializer.Serialize(cart));
            HttpContext.Session.SetInt32("CartItemCount", cart.OrderItems.Count);

            // Redirect to the previous page
            Console.Write("redirecting to referer : 2" + Request.Headers["Referer"].ToString());

            return Redirect(Request.Headers["Referer"].ToString());
        }

        return NotFound($"Product with Id {productId} not found in cart");
    }

// GET
    public IActionResult AddToCart(long productId)
    {
        // Retrieve the product based on productId (replace ProductService with your actual service or repository)
        Product? product = _context.Product.Find(productId);

        if (product == null)
        {
            // Handle the case where the product with the given ID is not found
            return NotFound();
        }

        // Retrieve the existing cart from the session
        var cartString = HttpContext.Session.GetString("cart_order");

        var cart = string.IsNullOrEmpty(cartString) ? new Order() : JsonSerializer.Deserialize<Order>(cartString);

        var orderItem = cart!.OrderItems.FirstOrDefault(x => x.ProductId == productId);

        if (orderItem != null)
        {
            // Increase the quantity of the existing product in the cart
            orderItem.Quantity++;
            cart!.UpdatedAt = DateTime.Now;

            // Save the updated cart back to the session
            HttpContext.Session.SetString("cart_order", JsonSerializer.Serialize(cart));
            HttpContext.Session.SetInt32("CartItemCount", cart.OrderItems.Count);

            // Redirect to the previous page
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // Add the new productId to the cart
        orderItem = new OrderItem();
        orderItem.Product = product;
        orderItem.OrderId = cart!.Id;
        orderItem.ProductId = product.Id;
        orderItem.Quantity = 1;

        cart.OrderItems.Add(orderItem);
        cart.UpdatedAt = DateTime.Now;

        // Save the updated cart back to the session
        HttpContext.Session.SetString("cart_order", JsonSerializer.Serialize(cart));
        HttpContext.Session.SetInt32("CartItemCount", cart.OrderItems.Count);

        // Redirect to the previous page
        return Redirect(Request.Headers["Referer"].ToString());
    }
}