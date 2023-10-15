using BankWebAPI_JS.Data;
using BankWebAPI_JS.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI_JS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (UserDBManager.Insert(user))
            {
                return Ok("Successfully inserted");
            }
            return BadRequest("Error in data insertion");
        }

        [HttpGet("get/{id}")]
        public IActionResult Get(string id)
        {
            User user = UserDBManager.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user) { StatusCode = 200 };
        }

        [HttpGet("getacc/{id}")]
        public IActionResult GetByAccNo(string id) 
        {
            User user = UserDBManager.GetByAccNo(id);
            if(user == null) 
            { 
                return NotFound();
            }
            return new ObjectResult(user) { StatusCode=200 };
        }

        [HttpGet("deleteByName/{id}")]
        public IActionResult DeleteByUsername(string id)
        {
            if (UserDBManager.DeleteByUsername(id))
            {
                return Ok("User successfully deleted");
            }
            return BadRequest("Could not delete user");
        }

        [HttpGet("deleteByEmail/{email}")]
        public IActionResult DeleteByEmail(string email)
        {
            if (UserDBManager.DeleteByEmail(email))
            {
                return Ok("Successfully Deleted");
            }
            return BadRequest("Could not delete");
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(string id, [FromBody] User user)
        {
            if (UserDBManager.Update(id, user.Password, user.Email, user.PhoneNumber))
            {
                return Ok("Successfully updated");
            }
            return BadRequest("Could not update");
        }
    }
}
