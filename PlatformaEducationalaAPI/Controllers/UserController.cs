using Microsoft.AspNetCore.Mvc;
using PlatformaEducationalaAPI.Models;
using Microsoft.EntityFrameworkCore;
using PlatformaEducationalaAPI.Services.UserService;
using PlatformaEducationalaAPI.Services.CourseService;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace PlatformaEducationalaAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
			_userService = userService;
		}

		// logout
		[HttpPost("logout")]
		public IActionResult Logout()
		{
			// Delete the "id" cookie
			Response.Cookies.Delete("id");

			return Ok("Logged out");
		}

		// Delete user
		[HttpDelete]
		public IActionResult DeleteUser()
		{
			// Get the user from the database
			var UserId = Request.Cookies["id"];
			var user = _userService.GetUserById(int.Parse(UserId));

			if (user != null)
			{
				if (_userService.DeleteUser(int.Parse(UserId)))
				{
					// Delete the "id" cookie
					Response.Cookies.Delete("id");
				}
			}

			return NoContent();
		}

		// Change email
		[HttpPut("changeEmail")]
		public IActionResult ChangeEmail(string newEmail)
		{
			// Get the user from the database
			var UserId = Request.Cookies["id"];
			if (UserId == null)
			{
				return Unauthorized();
			}
			else
			{
				//change email
				if (_userService.ChangeEmail(int.Parse(UserId), newEmail))
				{
					return Ok("Email has been successfully changed");
				}
				else
				{
					return BadRequest("Email already exists");
				}
			}
		}


		// register
		[HttpPost("register")]
		public IActionResult Register(string username, string password, string email)
		{
			// check if the fields are valid
			if (username == null || password == null || email == null)
			{
				return BadRequest("All fields are required");
			}

			string message = _userService.CreateUser(username, email, password);

			if (message != "User created")
			{
				return BadRequest(message);
			}

			//get the id of the user
			int user_id = _userService.GetUserByUsername(username).UserId;
			// Set the "id" cookie
			Response.Cookies.Append("id", user_id.ToString());

			return CreatedAtAction(nameof(Register), new { id = user_id });
		}

		// login
		[HttpPost("login")]
		public IActionResult Login(string username, string password)
		{
			// check if the fields are valid
			if (username == null || password == null)
			{
				return BadRequest("All fields are required");
			}

			string message = _userService.Login(username, password);
			if (message != "Login successful")
			{
				return BadRequest(message);
			}

			int user_id = _userService.GetUserByUsername(username).UserId;
			// Set the "id" cookie
			Response.Cookies.Append("id", user_id.ToString());

			return Ok("Login successful");
		}

		// user enrollments
		[HttpGet("enrollments")]
		public IActionResult Enrollments()
		{
			var UserId = Request.Cookies["id"];
			// Check if the user is logged in
			if (UserId == null)
			{
				return Unauthorized();
			}
			else
			{
				var courses = _userService.GetCoursesWhereUserIsEnrolled(int.Parse(UserId));
				return Ok(courses.ToArray());
			}
		}
	}
}
