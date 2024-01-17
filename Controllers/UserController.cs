using Microsoft.AspNetCore.Mvc;
using PlatformaEducationala_DAW.Models;
using Microsoft.EntityFrameworkCore;
using PlatformaEducationala_DAW.Services.UserService;
using PlatformaEducationala_DAW.Services.CourseService;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace PlatformaEducationala_DAW.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
			_userService = userService;
		}

		public IActionResult Index()
        {
            var userId = Request.Cookies["id"];
            // Verific daca este logat
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                // Get the user from the database
                ViewBag.User = _userService.GetUserById(int.Parse(userId));
                
                return View();
            }
        }

        // login
        public IActionResult Login()
        {
            return View();
        }

        // register
        public IActionResult Register()
        {
            return View();
        }

        // logout
        [HttpPost]
        public IActionResult Logout()
        {
            // Sterg cookie-ul
            Response.Cookies.Delete("id");

            return RedirectToAction("Index", "Home");
        }

        // Delete user
        [HttpPost]
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

            // Redirect to the home page
            return RedirectToAction("Index", "Home");
        }

        // Change email
        [HttpPost]
        public IActionResult ChangeEmail(string newEmail)
        {
            // Get the user from the database
            var UserId = Request.Cookies["id"];
            if (UserId == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                //change email
                if (_userService.ChangeEmail(int.Parse(UserId), newEmail))
                {
					ViewBag.Message = "Email-ul a fost schimbat cu succes!";
				}
				else
                {
					ViewBag.Message = "Email-ul exista deja!";
				}

                // Redirect to the user page
                return RedirectToAction("Index", "User");
            }
        }


        // register
        [HttpPost]
        public IActionResult Register(string username, string password, string email)
        {
            // verific daca campurile sunt completate
            if (username == null || password == null || email == null)
            {
                ViewBag.Message = "Toate campurile sunt obligatorii!";
                return View();
            }

            string message = _userService.CreateUser(username, email, password);

            if (message != "User created")
            {
				ViewBag.Message = message;
				return View();
			}

            //get the id of the user
            int user_id = _userService.GetUserByUsername(username).UserId;
            // Setez cookie-ul
            Response.Cookies.Append("id", user_id.ToString());

            return RedirectToAction("Index", "User");
        }

        // login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // verific daca campurile sunt completate
            if (username == null || password == null)
            {
                ViewBag.Message = "All the fields are required!";
                return View();
            }

            string message = _userService.Login(username, password);
            if (message != "Login successful")
            {
                ViewBag.Message = message;
				return View();
            }

            int user_id = _userService.GetUserByUsername(username).UserId;
            // Setez cookie-ul
            Response.Cookies.Append("id", user_id.ToString());

            return RedirectToAction("Index", "User");
        }

        // user enrollments
        public IActionResult Enrollments()
        {
            var UserId = Request.Cookies["id"];
            // Verific daca este logat
            if (UserId == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
				ViewBag.Courses = _userService.GetCoursesWhereUserIsEnrolled(int.Parse(UserId));

				return View();
            }
        }
    }
}
