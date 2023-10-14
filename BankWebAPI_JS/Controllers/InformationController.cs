using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI_JS.Controllers
{
    [Route("api/[controller]")]
    public class InformationController : Controller
    {
        [HttpGet("view")]
        public IActionResult GetView()
        {
            return PartialView("InformationView");
        }
    }
}
