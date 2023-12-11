using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScarletScreen.Model;
using System.Linq;
using System.Threading.Tasks;

namespace ScarletScreen.Pages
{
    public class ChangePasswordModel : PageModel
    {
        private readonly AccountsContext _context;

        public ChangePasswordModel(AccountsContext context)
        {
            _context = context;
        }

        public async Task OnPostAsync(string username, string currentPassword, string newPassword)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.user == username && u.pass == currentPassword);

            if (user != null)
            {
                user.pass = newPassword;

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Redirect or show a success message
                TempData["SuccessMessage"] = "Password changed successfully.";
                RedirectToPage("/Profile", new { username });
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid username or password.";
                RedirectToPage("/ChangePassword");
            }
        }
    }
}
