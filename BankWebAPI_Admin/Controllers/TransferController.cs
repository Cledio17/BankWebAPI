using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : Controller
    {
        [HttpGet("view")]
        public IActionResult GetView()
        {
            return PartialView("TransferView");
        }
    }
}
