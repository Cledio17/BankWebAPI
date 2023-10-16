using BankWebAPI.Data;
using BankWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            Account account = AccountDBManager.GetById(transaction.acctNo);
            if(account != null)
            {
                if (TransactionDBManager.Insert(transaction))
                {
                    TransactionDBManager.Update(transaction, account);
                    return Ok("Transaction successful.");
                }
            }
            return BadRequest("Error: Account not found");
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            /*Transaction transaction = TransactionDBManager.GetById(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);*/
            return View();
        }
    }
}
