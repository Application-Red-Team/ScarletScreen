using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScarletScreen.Model;

namespace ScarletScreen.Pages
{
    public class RegistrationSuccessModel : PageModel
    {
        private readonly AccountsContext _context;

        [BindProperty]
        public Accounts Account { get; set; }

        public RegistrationSuccessModel(AccountsContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.users.Add(Account);
            _context.SaveChanges();

            return RedirectToPage("/Index"); // Redirect to the "AccountCreated" page
        }
    }
}
