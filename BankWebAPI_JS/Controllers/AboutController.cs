﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankWebAPI_JS.Data;
using BankWebAPI_JS.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankWebAPI_JS.Controllers
{
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        [HttpGet("view")]
        public IActionResult GetView()
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                var cookieValue = Request.Cookies["SessionID"];
                User user = UserDBManager.GetByAccNo(cookieValue);
                if (cookieValue == user.acctNo)
                {
                    return PartialView("AboutViewAuthenticated");
                }

            }
            // Return the partial view as HTML
            return PartialView("AboutViewDefault");
        }
    }
}

