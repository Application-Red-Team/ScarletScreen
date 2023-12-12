using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ScarletScreen.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ScarletScreen.Pages
{
    public class EditProfileModel : PageModel
    {
        private readonly AccountsContext _context;

        public EditProfileModel(AccountsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Bio { get; set; }

        [BindProperty]
        public IFormFile ProfilePicture { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Get the username of the currently logged-in user
            string username = TempData["LoggedInUsername"]?.ToString();

            bool userExists = _context.users.Any(u => u.user == username);

            // Find the user in the database
            var user = await _context.users.FirstOrDefaultAsync(u => u.user == username);

            if (user != null)
            {
                // Update profile picture if a new one is provided
                if (ProfilePicture != null)
                {
                    // Get the user's pictures directory
                    var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                    // Generate a unique filename for the profile picture
                    string fileName = $"{username}_profile_picture{Path.GetExtension(ProfilePicture.FileName)}";

                    string filePath = Path.Combine(imagesDirectory, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProfilePicture.CopyToAsync(stream);
                    }

                    // Update the user's profile picture path in the database
                    user.ProfilePicturePath = $"/images/{fileName}";
                }

                // Update bio
                user.Bio = Bio;

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Redirect back to the profile page
                return RedirectToPage("/Profile", new { username });
            }

            // Handle the case where the user is not found
            TempData["ErrorMessage"] = "User not found.";
            return Page();
        }
    }
}
