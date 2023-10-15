using BankWebAPI_JS.Data;
using BankWebAPI_JS.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI_JS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        [HttpGet("view")]
        public IActionResult GetView()
        {
            return PartialView("AccountView");
        }
        [HttpPost]
        public IActionResult Post([FromBody] Account account)
        {
            if (AccountDBManager.Insert(account))
            {
                return Ok("Successfully inserted");
            }
            return BadRequest("Error in data insertion");
        }
        [HttpGet("get/{id}")]
        public IActionResult Get(string id)
        {
            Account account = AccountDBManager.GetById(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }
        [HttpGet("delete/{id}")]
        public IActionResult Delete(string id)
        {
            if (AccountDBManager.Delete(id))
            {
                return Ok("Successfully Deleted");
            }
            return BadRequest("Could not delete");
        }
        [HttpPut("update/{id}")]
        public IActionResult Update(string id, [FromBody] Account account)
        {
            if (AccountDBManager.Update(id, account.acctName, account.acctBal))
            {
                return Ok("Successfully updated");
            }
            return BadRequest("Could not update");
        }
    }
}
