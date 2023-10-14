using BankWebAPI_Admin.Data;
using BankWebAPI_Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankWebAPI_Admin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpGet("view")]
        public IActionResult GetView()
        {
            return PartialView("UserView");
        }
        [HttpGet("createview")]
        public IActionResult GetCreateView()
        {
            return PartialView("CreateView");
        }
        [HttpGet("editview")]
        public IActionResult GetEditView()
        {
            return PartialView("EditView");
        }
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
            return new ObjectResult(user) { StatusCode = 200};
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
