using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ScarletScreen.Model;
using Microsoft.EntityFrameworkCore;

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
            // Replace with the actual way you get the username (e.g., from the logged-in user's session)
            string username = "Cheetahportal";

            // Find the user in the database
            var user = await _context.users.FirstOrDefaultAsync(u => u.user == username);

            if (user != null)
            {
                // Update profile picture if a new one is provided
                if (ProfilePicture != null)
                {
                    // Get the user's pictures directory
                    var picturesDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                    // Generate a unique filename for the profile picture
                    string fileName = $"{username}_profile_picture{Path.GetExtension(ProfilePicture.FileName)}";
                    string filePath = Path.Combine(picturesDirectory, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProfilePicture.CopyToAsync(stream);
                    }

                    // Update the user's profile picture path in the database
                    user.ProfilePicturePath = filePath;
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