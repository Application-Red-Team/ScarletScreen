using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ScarletScreen.Model;

namespace ScarletScreen.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AccountsContext _context;

        public LoginModel(AccountsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            var user = _context.users.FirstOrDefault(u => u.user == Username && u.pass == Password);

            if (user != null)
            {
                // If the user is found in the database, redirect to the profile page
                return RedirectToPage("/Profile", new { username = Username });
            }
            else
            {
                // If the user is not found, display an error or redirect to the login page
                TempData["ErrorMessage"] = "Invalid username or password.";
                return Page();
            }
        }
    }
}

