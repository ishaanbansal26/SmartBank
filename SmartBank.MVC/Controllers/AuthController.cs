using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartBank.AuthService.DTOs;
using SmartBank.AuthService.Services;


namespace SmartBank.MVC.Controllers
{

    public class AuthController : Controller
    {
        private readonly AuthServices _authService;

        public AuthController(AuthServices authService)
        {
            _authService = authService;
        }

        [HttpGet] //this shows the login page from login.cshtml
        public IActionResult Login()
        {
            Console.WriteLine("loginget hit");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            // Provide role options (must match roles seeded in AuthService)
            ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem("Customer", "Customer"),
                new SelectListItem("Admin", "Admin")
            };
            

            return View(new RegisterDto());
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "Customer", Text = "Customer" }
            };

            if (!ModelState.IsValid)
                return View(dto);

            var success = await _authService.RegisterAsync(dto);
            if (!success)
            {
                ViewBag.Error = "Registration failed!";
                return View(dto);
            }

            return Redirect("https://localhost:1234/Auth/Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            //it calls authsercie.login and send the login info to the api
            //gets back a JWT token if the credentials are valid

            if (token == null)
            {
                ViewBag.Error = "Invalid credentials";
                return View();
            }

            // Store token (Session or Cookie)
            HttpContext.Session.SetString("JWT", token);//stores the JWT in the session
            //No re-login on every request

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWT"); // clear login

            return Redirect("https://localhost:1234/Auth/Login"); // redirect to login page
        }
    }
}

//session is a server side storage mechanism for a specific user
//the thing we put in the HttpContext.Session is there until the session is cleared out
    