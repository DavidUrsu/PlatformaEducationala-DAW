using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformaEducationala_DAW.Models;

namespace PlatformaEducationala_DAW.Controllers
{
    public class CourseController : Controller
    {
        private readonly PlatformaDbContext _context;
        public CourseController(PlatformaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses.ToList();
            ViewBag.Courses = courses;

            // check if the user is logged in
            if (Request.Cookies["id"] != null)
            {
				// get the enrolled courses of the user
				var userEnrollments = _context.Users
					.Where(u => u.UserId == int.Parse(Request.Cookies["id"]))
					.Include(u => u.Enrollments)
					.ThenInclude(e => e.Course)
					.FirstOrDefault();
                // get just the id of the courses
                var enrolledCourses = userEnrollments.Enrollments.Select(e => e.CourseId).ToList();
                ViewBag.EnrolledCourses = enrolledCourses;
            }

            return View();
        }

        // Create course
        public IActionResult Create()
        {
            // check if the user is logged in
            if (Request.Cookies["id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View();
            }
        }

        //Edit course
        public IActionResult Edit(int id)
        {
            // check if the user is logged in
            if (Request.Cookies["id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            //check if the course exists
            else if(_context.Courses.Find(id) == null) {
                return RedirectToAction("Index", "Course");
            }
            else
            {
                //check if the course professor is the same as the logged in user
                var course = _context.Courses.Find(id);
                if (course.ProfessorUserId != int.Parse(Request.Cookies["id"]))
                {
					return RedirectToAction("Index", "Course");
				}

                // put the course in the ViewBag
                ViewBag.Course = course;
                return View();
            }
        }

        //detele course
        public IActionResult Delete(int id)
        {
			// check if the user is logged in
			if (Request.Cookies["id"] == null)
            {
				return RedirectToAction("Login", "User");
			}
			//check if the course exists
			else if (_context.Courses.Find(id) == null)
            {
				return RedirectToAction("Index", "Course");
			}
			else
            {
				//check if the course professor is the same as the logged in user
				var course = _context.Courses.Find(id);
				if (course.ProfessorUserId != int.Parse(Request.Cookies["id"]))
                {
					return RedirectToAction("Index", "Course");
				}

                //delete all the enrollments of the course
                var enrollments = _context.Enrollments.Where(e => e.CourseId == id).ToList();
                foreach (var enrollment in enrollments)
                {
					_context.Enrollments.Remove(enrollment);
				}

				// delete course
				_context.Courses.Remove(course);
				_context.SaveChanges();

				return RedirectToAction("Index", "Course");
			}
		}   

        //edit course post
        [HttpPost]
        public IActionResult Edit(int CourseId, string courseName, string courseDescription, int coursePrice, int courseSalePrice, string courseImage)
        {
			// check if the user is logged in
			if (Request.Cookies["id"] == null)
            {
				return RedirectToAction("Login", "User");
			}
			//check if the course exists
			else if(_context.Courses.Find(CourseId) == null)
            {
				return RedirectToAction("Index", "Course");
			}
			else
            {
				//check if the course professor is the same as the logged in user
				var course = _context.Courses.Find(CourseId);
				if (course.ProfessorUserId != int.Parse(Request.Cookies["id"]))
                {
                    return RedirectToAction("Index", "Course");
                }
                
                course.CourseName = courseName;
                course.CourseDescription = courseDescription;
                course.CoursePrice = coursePrice;
                course.CourseSalePrice = courseSalePrice;
                course.CourseImage = courseImage;
                _context.SaveChanges();

                return RedirectToAction("Index", "Course");
            }
        }

        // create course post
        [HttpPost]
        public IActionResult Create(string courseName, string courseDescription, int coursePrice, string courseImage)
        {
            // check if the user is logged in
            if (Request.Cookies["id"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                //check if the items are not empty
                if (courseName == null || courseDescription == null || coursePrice == 0 || courseImage == null)
                {
                    ViewBag.Error = "Please fill in all the fields!";
                    return RedirectToAction("Create", "Course");
                }

                // create course
                Course course = new Course();
                course.CourseName = courseName;
                course.CourseDescription = courseDescription;
                course.CoursePrice = coursePrice;
                course.CourseSalePrice = coursePrice;
                course.CourseImage = courseImage;
                course.ProfessorUserId = int.Parse(Request.Cookies["id"]);

                // add course to database
                _context.Courses.Add(course);
                _context.SaveChanges();

                return RedirectToAction("Index", "Course");
            }
        }

        //enroll user to the Course
        public IActionResult Enroll(int id)
        {
			// check if the user is logged in
			if (Request.Cookies["id"] == null)
            {
				return RedirectToAction("Login", "User");
			}
			//check if the course exists
			else if (_context.Courses.Find(id) == null)
            {
				return RedirectToAction("Index", "Course");
			}
			else
            {
				//check if the user is already enrolled
				var user = _context.Users
					.Where(u => u.UserId == int.Parse(Request.Cookies["id"]))
					.Include(u => u.Enrollments)
					.ThenInclude(e => e.Course)
					.FirstOrDefault();
				foreach (var enrollment in user.Enrollments)
                {
					if (enrollment.CourseId == id)
                    {
						return RedirectToAction("Index", "Course");
					}
				}

				// enroll user
				Enrollment newEnrollment = new Enrollment();
				newEnrollment.UserId = int.Parse(Request.Cookies["id"]);
				newEnrollment.CourseId = id;
				_context.Enrollments.Add(newEnrollment);
				_context.SaveChanges();

				return RedirectToAction("Index", "Course");
			}
		}

        //unenroll user from the Course
		public IActionResult Unenroll(int id)
        {
            // check if the user is logged in
            if (Request.Cookies["id"] == null)
            {
				return RedirectToAction("Login", "User");
			}
            //check if the course exists
            else if (_context.Courses.Find(id) == null)
            {
                return RedirectToAction("Index", "Course");
            }
            else
            {
                //check if the user is already enrolled
				var user = _context.Users
					.Where(u => u.UserId == int.Parse(Request.Cookies["id"]))
					.Include(u => u.Enrollments)
					.ThenInclude(e => e.Course)
					.FirstOrDefault();
				foreach (var enrollment in user.Enrollments)
                {
					if (enrollment.CourseId == id)
                    {
						// unenroll user
						_context.Enrollments.Remove(enrollment);
						_context.SaveChanges();
						return RedirectToAction("Index", "Course");
					}
				}

				return RedirectToAction("Index", "Course");
            }
        }
    }
}
