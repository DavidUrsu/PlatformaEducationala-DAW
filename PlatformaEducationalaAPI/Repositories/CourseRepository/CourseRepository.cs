using Azure.Core;
using PlatformaEducationalaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PlatformaEducationalaAPI.Repositories.CourseRepository
{
	public class CourseRepository : ICourseRepository
	{
		private readonly PlatformaDbContext _context;

		public CourseRepository(PlatformaDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Course> GetAllCourses()
		{
			return _context.Courses.ToList();
		}

		public Course GetCourseById(int id)
		{
			return _context.Courses.Find(id);
		}

		public void AddCourse(Course course)
		{
			_context.Courses.Add(course);
			_context.SaveChanges();
		}

		public void DeleteCourse(int id)
		{
			var course = GetCourseById(id);
			if (course != null)
			{
				_context.Courses.Remove(course);
				_context.SaveChanges();
			}
		}

		public IEnumerable<CourseDTO> GetEnrolledCoursesByUserId(int id)
		{
			var userEnrollments = _context.Users
					.Where(u => u.UserId == id)
					.Include(u => u.Enrollments)
					.ThenInclude(e => e.Course)
					.FirstOrDefault();

			//check if the user has any enrollments
			if (userEnrollments == null)
			{
				//return an empty list
				return new List<CourseDTO>();
			}

			return userEnrollments.Enrollments.Select(e => new CourseDTO
			{
				CourseId = e.Course.CourseId,
				CourseName = e.Course.CourseName,
				CourseDescription = e.Course.CourseDescription,
				// Map other properties
			}).ToList();
		}

		public IEnumerable<Enrollment> GetEnrollmentsByCourseId(int id)
		{
			return _context.Enrollments.Where(e => e.CourseId == id).ToList();
		}

		public void DeleteEnrollment(Enrollment enrollment)
		{
			_context.Enrollments.Remove(enrollment);
			_context.SaveChanges();
		}

		public void UpdateCourse(int CourseId, string courseName, string courseDescription, int coursePrice, int courseSalePrice, string courseImage)
		{
			var course = GetCourseById(CourseId);
			if (course != null)
			{
				course.CourseName = courseName;
				course.CourseDescription = courseDescription;
				course.CoursePrice = coursePrice;
				course.CourseSalePrice = courseSalePrice;
				course.CourseImage = courseImage;
				_context.SaveChanges();
			}
		}

		public Course CreateCourse(string courseName, string courseDescription, int coursePrice, string courseImage, int ProfessorId)
		{
			// create course
			Course course = new Course();
			course.CourseName = courseName;
			course.CourseDescription = courseDescription;
			course.CoursePrice = coursePrice;
			course.CourseSalePrice = coursePrice;
			course.CourseImage = courseImage;
			course.ProfessorUserId = ProfessorId;

			// add course to database
			_context.Courses.Add(course);
			_context.SaveChanges();

			return course;
		}

		public void addEnrollment(Enrollment enrollment)
		{
			_context.Enrollments.Add(enrollment);
			_context.SaveChanges();
		}
		
		public ICollection<Course> GetCoursesWhereUserIsTeacher(int id)
		{
			return _context.Courses.Where(c => c.ProfessorUserId == id).ToList();
		}

		public void UpdateCourse(Course course)
		{
			var courseToUpdate = GetCourseById(course.CourseId);

			if (courseToUpdate != null)
			{
				courseToUpdate.CourseName = course.CourseName;
				courseToUpdate.CourseDescription = course.CourseDescription;
				courseToUpdate.CoursePrice = course.CoursePrice;
				courseToUpdate.CourseSalePrice = course.CourseSalePrice;
				courseToUpdate.CourseImage = course.CourseImage;
				_context.SaveChanges();
			}
		}
	}
}
