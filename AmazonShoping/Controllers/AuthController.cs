using AmazonShoping.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmazonShoping.Controllers
{
    public class AuthController : Controller
    {
        // GET: Login
        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");
            if (!string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("you Products page");
                return LocalRedirect("/Products/Index");
            }

            Console.WriteLine("you got Login page");
            return View();
        }

        // POST: Login/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User user, [FromQuery] string redirectTo)
        {
            if (user.Username == "admin" && user.Password == "admin")
            {
                //handle redirectTo get param
                if (!string.IsNullOrEmpty(redirectTo))
                {
                    Console.WriteLine($"redirectTo: {redirectTo}");
                    return Redirect(redirectTo);
                }

                Console.WriteLine($"Login Success: {user.Username}");
                HttpContext.Session.SetString("Username", user.Username);
                return LocalRedirect("/Products/Index");
            }

            ViewBag.Message = "Invalid User Name or Password";
            Console.WriteLine($"Login Failed: {user.Username}");
            return View("Index");
        }

        // GET: Login/Create
        public ActionResult Register(User user)
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}