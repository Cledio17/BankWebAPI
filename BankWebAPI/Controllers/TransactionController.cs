using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
