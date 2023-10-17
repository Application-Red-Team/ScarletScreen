using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ScarletScreen.Pages
{
    public class ProfileModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                // User is logged in, retrieve and display their profile
                // Logic to retrieve profile information
                return Page();
            }
            else
            {
                // User is not logged in, redirect to account creation
                return RedirectToPage("/CreateAccount");
            }
        }



    }
}

