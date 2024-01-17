using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformaEducationala_DAW.Models;
using PlatformaEducationala_DAW.Services.CourseService;

namespace PlatformaEducationala_DAW.Controllers
{
	public class CourseController : Controller
	{
		private readonly ICourseService _service;

		public CourseController(ICourseService service)
		{
			_service = service;
		}

		public IActionResult Index()
		{
			var userId = Request.Cookies["id"];
			ViewBag.Courses = _service.GetAllCourses();
			// check if the user is logged in
			if (userId != null)
			{
				ViewBag.EnrolledCourses = _service.GetTheIdOfEnrolledCourses(int.Parse(userId));
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
			var course = _service.GetCourseById(id);
			var userId = Request.Cookies["id"];

			// check if the user is logged in
			if (userId == null)
			{
				return RedirectToAction("Login", "User");
			}
			//check if the course exists
			else if (course == null)
			{
				return RedirectToAction("Index", "Course");
			}
			else
			{
				//check if the course professor is the same as the logged in user
				if (course.ProfessorUserId != int.Parse(userId))
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
			var UserId = Request.Cookies["id"];
			var course = _service.GetCourseById(id);
			// check if the user is logged in
			if (UserId == null)
			{
				return RedirectToAction("Login", "User");
			}
			//check if the course exists
			else if (course == null)
			{
				return RedirectToAction("Index", "Course");
			}
			else
			{
				//check if the course professor is the same as the logged in user
				if (course.ProfessorUserId != int.Parse(UserId))
				{
					return RedirectToAction("Index", "Course");
				}

				// delete course
				_service.deleteCourse(id);

				return RedirectToAction("Index", "Course");
			}
		}

		//edit course post
		[HttpPost]
		public IActionResult Edit(int CourseId, string courseName, string courseDescription, int coursePrice, int courseSalePrice, string courseImage)
		{
			var UserId = Request.Cookies["id"];
			var course = _service.GetCourseById(CourseId);
			// check if the user is logged in
			if (UserId == null)
			{
				return RedirectToAction("Login", "User");
			}
			//check if the course exists
			else if (course == null)
			{
				return RedirectToAction("Index", "Course");
			}
			else
			{
				//check if the course professor is the same as the logged in user
				if (course.ProfessorUserId != int.Parse(UserId))
				{
					return RedirectToAction("Index", "Course");
				}

				//check if the items are not empty
				if (courseName == null || courseDescription == null || courseImage == null)
				{
					ViewBag.Error = "Please fill in all the fields!";
					return RedirectToAction("Edit", "Course", new { id = CourseId });
				}

				_service.UpdateCourse(CourseId, courseName, courseDescription, coursePrice, courseSalePrice, courseImage);

				return RedirectToAction("Index", "Course");
			}
		}

		// create course post
		[HttpPost]
		public IActionResult Create(string courseName, string courseDescription, int coursePrice, string courseImage)
		{
			var UserId = Request.Cookies["id"];
			// check if the user is logged in
			if (UserId == null)
			{
				return RedirectToAction("Login", "User");
			}
			else
			{
				//check if the items are not empty
				if (courseName == null || courseDescription == null || courseImage == null)
				{
					ViewBag.Error = "Please fill in all the fields!";
					return RedirectToAction("Create", "Course");
				}

				// create course
				_service.CreateCourse(courseName, courseDescription, coursePrice, courseImage, int.Parse(UserId));

				return RedirectToAction("Index", "Course");
			}
		}

		//enroll user to the Course
		public IActionResult Enroll(int id)
		{
			var UserId = Request.Cookies["id"];
			var course = _service.GetCourseById(id);

			// check if the user is logged in
			if (UserId == null)
			{
				return RedirectToAction("Login", "User");
			}
			//check if the course exists
			else if (course == null)
			{
				return RedirectToAction("Index", "Course");
			}
			else
			{
				// enroll user
				_service.EnrollUserToCourse(int.Parse(UserId), id);

				return RedirectToAction("Index", "Course");
			}
		}

		//unenroll user from the Course
		public IActionResult Unenroll(int id)
		{
			var UserId = Request.Cookies["id"];
			var course = _service.GetCourseById(id);
			// check if the user is logged in
			if (UserId == null)
			{
				return RedirectToAction("Login", "User");
			}
			//check if the course exists
			else if (course == null){
				return RedirectToAction("Index", "Course");
			}
			else
			{
				//unenroll user
				_service.UnenrollUserFromCourse(int.Parse(UserId), id);

				return RedirectToAction("Index", "Course");
			}
		}
	}
}
