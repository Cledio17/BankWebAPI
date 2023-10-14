using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankWebAPI_Admin.Models;

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
                if (cookieValue == "1234567")
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
            if (Request.Cookies.ContainsKey("SessionID"))
            {
                var cookieValue = Request.Cookies["SessionID"];
                if (cookieValue == "1234567")
                {
                    return PartialView("LoginViewAuthenticated");
                }

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
            var response = new { login = false};
            

            if (user!=null && user.UserName.Equals("sajib") && user.Password.Equals("mistry"))
            {
                Response.Cookies.Append("SessionID", "1234567");
                response = new { login = true };
            }
            return Json(response);
            
        }

    }
}

