using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScarletScreen.Model;
using System.Linq;
using System.Threading.Tasks;

namespace ScarletScreen.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly AccountsContext _context;

        public ProfileModel(AccountsContext context)
        {
            _context = context;
        }

        public string Username { get; private set; }
        public string ProfilePicturePath { get; private set; }
        public string Bio { get; private set; }

        public async Task OnGetAsync()
        {
            Username = Request.Query["username"];

            if (string.IsNullOrEmpty(Username))
            {
                RedirectToPage("/Login");
                return; // Make sure to return after redirecting
            }

            // Retrieve the user from the database
            var user = await _context.users.FirstOrDefaultAsync(u => u.user == Username);

            if (user != null)
            {
                // Assign profile information to properties
                ProfilePicturePath = user.ProfilePicturePath;
                Bio = user.Bio;
            }
            else
            {
                // Handle the case where the user is not found
                // For simplicity, you might want to display an error message
                TempData["ErrorMessage"] = "User not found.";
                RedirectToPage("/Login");
            }
        }
    }
}

