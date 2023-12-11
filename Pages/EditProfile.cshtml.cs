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

        public async Task<IActionResult> OnPostAsync()
        {
            // Replace with the actual way you get the username (e.g., from the logged-in user's session)
            string username = "Cheetahportal";

            // Find the user in the database
            var user = await _context.users.SingleOrDefaultAsync(u => u.user == username);

            if (user != null)
            {
                // Update bio
                user.Bio = Bio;

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Redirect back to the profile page
                return RedirectToPage("/Profile", new { username });
            }

            // Handle the case where the user is not found
            // For simplicity, you might want to display an error message
            TempData["ErrorMessage"] = "User not found.";
            return Page();
        }
    }

}