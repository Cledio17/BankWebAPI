using BankWebAPI.Data;
using BankWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(string id)
        {
            User user = UserDBManager.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteByUsername(string id)
        {
            if (UserDBManager.DeleteByUsername(id))
            {
                return Ok("Successfully Deleted");
            }
            return BadRequest("Could not delete");
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteByEmail(string id)
        {
            if (UserDBManager.DeleteByEmail(id))
            {
                return Ok("Successfully Deleted");
            }
            return BadRequest("Could not delete");
        }
        [HttpPut]
        public IActionResult Update(User user)
        {
            if (UserDBManager.Update(user))
            {
                return Ok("Successfully updated");
            }
            return BadRequest("Could not update");
        }
    }
}
