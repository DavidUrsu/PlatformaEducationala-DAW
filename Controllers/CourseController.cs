using Microsoft.AspNetCore.Mvc;
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
    }
}
