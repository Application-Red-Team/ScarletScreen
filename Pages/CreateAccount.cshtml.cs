using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ScarletScreen.Model;

namespace ScarletScreen.Pages
{
    public class CreateAccountModel : PageModel
    {
        private readonly AccountsContext _context;

        [BindProperty]
        public Accounts Account { get; set; }

        public CreateAccountModel(AccountsContext context)
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

            return RedirectToPage("/RegistrationSuccess");
        }
    }
}
