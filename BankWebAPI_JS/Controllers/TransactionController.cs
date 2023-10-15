﻿using BankWebAPI_JS.Data;
using BankWebAPI_JS.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI_JS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        [HttpGet("view")]
        public IActionResult GetView()
        {
            return PartialView("TransactionView");
        }
        [HttpPost]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            Account account = AccountDBManager.GetById(transaction.fromId);
            Account account2 = AccountDBManager.GetById(transaction.toId);
            if (account != null && account2 != null)
            {
                if (TransactionDBManager.Insert(transaction))
                {
                    TransactionDBManager.Update(transaction, account, account2);
                    return Ok("Successfully inserted");
                }
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