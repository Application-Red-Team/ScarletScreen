using Microsoft.AspNetCore.Mvc;

namespace ScarletScreen.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
