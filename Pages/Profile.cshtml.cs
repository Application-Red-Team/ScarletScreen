using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ScarletScreen.Pages
{
    public class ProfileModel : PageModel
    {
        public string Username { get; private set; }

        public void OnGet()
        {
            // Retrieve the username from the query parameters
            Username = Request.Query["username"];

            if (string.IsNullOrEmpty(Username))
            {
                RedirectToPage("/Login");
            }
        }
    }
}

