using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI_JS.Controllers
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
