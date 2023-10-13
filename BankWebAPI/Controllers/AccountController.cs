using BankWebAPI.Data;
using BankWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
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
        [HttpPut]
        public IActionResult Update(Account account)
        {
            if (AccountDBManager.Update(account))
            {
                return Ok("Successfully updated");
            }
            return BadRequest("Could not update");
        }
    }
}
