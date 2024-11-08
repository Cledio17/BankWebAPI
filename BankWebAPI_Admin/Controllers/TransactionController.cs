﻿using BankWebAPI_Admin.Data;
using BankWebAPI_Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI_Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet]
        [Route("getbyfromid/{id}")]
        public IActionResult GetByFromId(string id)
        {
            List<Transaction> transactions = TransactionDBManager.GetByFromId(id);
            if (transactions == null)
            {
                return NotFound();
            }
            return Ok(transactions);
        }
        [HttpGet("getall")]
        public IActionResult GetAllTransactions()
        {
            List<Transaction> transactions = TransactionDBManager.GetAllTransactions();

            if (transactions == null || transactions.Count == 0)
            {
                return NotFound("No transactions found.");
            }

            return Ok(transactions);
        }
    }
}
