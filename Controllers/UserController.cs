using Microsoft.AspNetCore.Mvc;
using PlatformaEducationala_DAW.Models;
using System.Text;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PlatformaEducationala_DAW.Controllers
{
    public class UserController : Controller
    {
        private readonly PlatformaDbContext _context;
        public UserController(PlatformaDbContext context)
        {
               _context = context;
        }

        public IActionResult Index()
        {
            // Verific daca este logat
            if (Request.Cookies["id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                // send data to view
                ViewBag.User = Request.Cookies["id"];

                // get the data of the user from the database
                User user = _context.Users.SingleOrDefault(u => u.UserId == int.Parse(Request.Cookies["id"]));

                ViewBag.UserId = user.UserId;
                ViewBag.Username = user.Username;
                ViewBag.Email = user.Email;

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

        // function hash password
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
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
            var user = _context.Users.SingleOrDefault(u => u.UserId == int.Parse(Request.Cookies["id"]));

            if (user != null)
            {
				// Get all courses where the user is the teacher
				var courses = _context.Courses.Where(c => c.ProfessorUserId == int.Parse(Request.Cookies["id"])).ToList();

				foreach (var course in courses)
				{
					// Delete all the enrollments of the course
					var enrollments = _context.Enrollments.Where(e => e.CourseId == course.CourseId).ToList();
					foreach (var enrollment in enrollments)
					{
						_context.Enrollments.Remove(enrollment);
					}

					// Delete course
					_context.Courses.Remove(course);
				}
				_context.SaveChanges();

				// Get all blog posts by the user
				var posts = _context.BlogPosts.Where(bp => bp.UserId == int.Parse(Request.Cookies["id"])).ToList();

				foreach (var post in posts)
				{
					_context.BlogPosts.Remove(post);
				}
				_context.SaveChanges();


				//delete all the enrollments of the user
				var enrollments1 = _context.Enrollments.Where(e => e.UserId == int.Parse(Request.Cookies["id"])).ToList();
                foreach (var enrollment in enrollments1)
                {
					_context.Enrollments.Remove(enrollment);
				}

				_context.SaveChanges();

				// Delete the user
				_context.Users.Remove(user);
                _context.SaveChanges();

                // Delete the "id" cookie
                Response.Cookies.Delete("id");
            }

            // Redirect to the home page
            return RedirectToAction("Index", "Home");
        }

        // Change email
        [HttpPost]
        public IActionResult ChangeEmail(string newEmail)
        {
            // Get the user from the database
            var user = _context.Users.SingleOrDefault(u => u.UserId == int.Parse(Request.Cookies["id"]));

            // Check if the mail is in use
            var userWithSameEmail = _context.Users.FirstOrDefault(u => u.Email == newEmail);
            if (userWithSameEmail != null)
            {
                ViewBag.Message = "Email-ul este deja folosit!";
                return RedirectToAction("Index", "User");
            }

            if (user != null)
            {
                // Change the email
                user.Email = newEmail;
                _context.SaveChanges();
            }

            // Redirect to the user page
            return RedirectToAction("Index", "User");
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

            // Verific daca exista deja un user cu acelasi username
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user != null)
            {
                ViewBag.Message = "Username-ul exista deja!";
                return View();
            }

            //verific daca exista deja un user cu acelasi email
            var userWithSameEmail = _context.Users.FirstOrDefault(u => u.Email == email);
            if (userWithSameEmail != null)
            {
                ViewBag.Message = "Email-ul exista deja!";
                return View();
            }

            // Creez un nou user
            User newUser = new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
                Email = email,
                Role = "User"
            };

            // Adaug user-ul in baza de date
            _context.Users.Add(newUser);
            _context.SaveChanges();

            //get the id of the user
            int user_id = _context.Users.SingleOrDefault(u => u.Username == username).UserId;

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
                ViewBag.Message = "Toate campurile sunt obligatorii!";
                return View();
            }

            // Verific daca exista un user cu acelasi username
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            if (user == null)
            {
                ViewBag.Message = "Username-ul nu exista!";
                return View();
            }

            // Verific daca parola este corecta
            if (user.PasswordHash != HashPassword(password))
            {
                ViewBag.Message = "Parola este gresita!";
                return View();
            }

            // Setez cookie-ul
            Response.Cookies.Append("id", user.UserId.ToString());

            return RedirectToAction("Index", "User");
        }

        // user enrollments
        public IActionResult Enrollments()
        {
            // Verific daca este logat
            if (Request.Cookies["id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
				// get the user with their enrollments
				var user = _context.Users
					.Where(u => u.UserId == int.Parse(Request.Cookies["id"]))
					.Include(u => u.Enrollments)
						.ThenInclude(e => e.Course)
					.FirstOrDefault();

				// select the courses from the enrollments
				var courses = user.Enrollments.Select(e => e.Course).ToList();
				ViewBag.Courses = courses;

				return View();
            }
        }   
    }
}
