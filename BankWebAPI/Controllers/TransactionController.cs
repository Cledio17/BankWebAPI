using BankWebAPI.Data;
using BankWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            if (TransactionDBManager.Insert(transaction))
            {
                Account account = AccountDBManager.GetById(transaction.fromId);
                Account account2 = AccountDBManager.GetById(transaction.toId);
                TransactionDBManager.Update(transaction, account, account2);
                return Ok("Successfully inserted");
            }
            return BadRequest("Error in data insertion");
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            Transaction transaction = TransactionDBManager.GetById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }
    }
}
