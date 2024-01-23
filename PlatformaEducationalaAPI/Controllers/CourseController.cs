using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformaEducationalaAPI.Models;
using PlatformaEducationalaAPI.Services.CourseService;

namespace PlatformaEducationalaAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CourseController : ControllerBase
	{
		private readonly ICourseService _service;

		public CourseController(ICourseService service)
		{
			_service = service;
		}

		// Get all the courses
		[HttpGet(Name = "GetAllCourses")]
		public ActionResult<IEnumerable<Course>> GetAllCourses()
		{
			return Ok(_service.GetAllCourses());
		}

		//Get the enrolled courses of a user
		[HttpGet("GetEnrolledCoursesByUserId/{id}", Name = "GetEnrolledCoursesByUserId")]
		public ActionResult<IEnumerable<Course>> GetEnrolledCoursesByUserId(int id)
		{
			return Ok(_service.GetTheIdOfEnrolledCourses(id));
		}

		// Create course
		[HttpPost]
		public ActionResult<Course> Create(string courseName, string courseDescription, int coursePrice, string courseImage)
		{
			var userId = Request.Cookies["id"];
			if (userId == null)
			{
				return Unauthorized();
			}
			else if (courseName == null || courseDescription == null || courseImage == null)
			{
				// check if the fields are valid
				return BadRequest();
			}
			else
			{
				var newCourse = _service.CreateCourse(courseName, courseDescription, coursePrice, courseImage, int.Parse(userId));
				return CreatedAtAction("GetAllCourses", new { id = newCourse.CourseId }, newCourse);
			}
		}

		//detele course
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var userId = Request.Cookies["id"];
			var course = _service.GetCourseById(id);
			// check if the user is logged in
			if (userId == null)
			{
				return Unauthorized();
			}
			//check if the course exists
			else if (course == null)
			{
				return NotFound();
			}
			else
			{
				//check if the course professor is the same as the logged in user
				if (course.ProfessorUserId != int.Parse(userId))
				{
					return Unauthorized();
				}

				// delete course
				_service.deleteCourse(id);

				return NoContent();
			}
		}

		//edit course post
		[HttpPut("{id}")]
		public IActionResult Edit(int id, string courseName, string courseDescription, int coursePrice, int courseSalePrice, string courseImage)
		{
			//check if the user is logged in
			var userId = Request.Cookies["id"];
			if(userId == null)
			{
				return Unauthorized();
			}
			else if(courseName == null || courseDescription == null || courseImage == null)
			{
				//check if the fields are valid
				return BadRequest();
			}
			else
			{
				// check if there is a course with the given id
				var existingCourse = _service.GetCourseById(id);
				if (existingCourse == null)
				{
					return NotFound();
				}
				else
				{
					//check if the course professor is the same as the logged in user
					if (existingCourse.ProfessorUserId != int.Parse(userId))
					{
						return Unauthorized();
					}

					// update course
					_service.UpdateCourse(coursePrice, courseName, courseDescription, coursePrice, courseSalePrice, courseImage);

					return NoContent();
				}
			}
			
		}

		//enroll user to the Course
		
		[HttpPost("{id}/enroll")]
		public IActionResult Enroll(int id)
		{
			var userId = Request.Cookies["id"];
			var course = _service.GetCourseById(id);

			// check if the user is logged in
			if (userId == null || course == null)
			{
				return BadRequest();
			}
			else
			{
				// enroll user
				_service.EnrollUserToCourse(int.Parse(userId), id);

				return Ok();
			}
		}

		//unenroll user from the Course
		[HttpPost("{id}/unenroll")]
		public IActionResult Unenroll(int id)
		{
			var userId = Request.Cookies["id"];
			var course = _service.GetCourseById(id);
			// check if the user is logged in
			if (userId == null || course == null)
			{
				return BadRequest();
			}
			else
			{
				//unenroll user
				_service.UnenrollUserFromCourse(int.Parse(userId), id);

				return Ok();
			}
		}
	}
}
