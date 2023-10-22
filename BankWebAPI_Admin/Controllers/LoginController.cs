using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankWebAPI_Admin.Models;
using BankWebAPI_Admin.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankWebAPI_Admin.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpGet("defaultview")]
        public IActionResult GetDefaultView()
        {
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                var cookieValue = Request.Cookies["SessionID"];
                User user = UserDBManager.GetByAccNo(cookieValue);
                if (cookieValue == user.acctNo)
                {
                    return PartialView("LoginViewAuthenticated");
                }


            }
            // Return the partial view as HTML
            return PartialView("LoginDefaultView");
        }

        [HttpGet("authview")]
        public IActionResult GetLoginAuthenticatedView()
        {
            var cookieValue = Request.Cookies["SessionID"];
            User user = UserDBManager.GetByAccNo(cookieValue);
            if (cookieValue == user.acctNo)
            {
                return PartialView("LoginViewAuthenticated");
            }
            // Return the partial view as HTML
            return PartialView("LoginErrorView");
        }

        [HttpGet("error")]
        public IActionResult GetLoginErrorView()
        {
            return PartialView("LoginErrorView");
        }

        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] User user)
        {
            // Return the partial view as HTML
            var response = new { login = false };
            User user1 = UserDBManager.GetById(user.UserName);

            if (user != null && user1 != null && user.UserName.Equals("admin") && user.Password.Equals("admin123"))
            {
                Response.Cookies.Append("SessionID", user1.acctNo);
                response = new { login = true };
            }
            return Json(response);

        }

    }
}

