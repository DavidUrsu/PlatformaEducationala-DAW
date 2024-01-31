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

		public IEnumerable<CourseDTO> GetAllCourses()
		{
			return _context.Courses.Select(c => new CourseDTO
			{
				CourseId = c.CourseId,
				CourseName = c.CourseName,
				CourseDescription = c.CourseDescription,
				CoursePrice = c.CoursePrice,
				CourseSalePrice = c.CourseSalePrice,
				CourseImage = c.CourseImage,
				ProfessorUserId = c.ProfessorUserId,
				ProfessorName = _context.Users.FirstOrDefault(u => u.UserId == c.ProfessorUserId).Username,
				CourseDate = c.CourseDate
			}).ToList();
		}

		public Course GetCourseById(int id)
		{
			return _context.Courses.Find(id);
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
				CourseImage = e.Course.CourseImage,
				CoursePrice = e.Course.CoursePrice,
				CourseSalePrice = e.Course.CourseSalePrice,
				ProfessorUserId = e.Course.ProfessorUserId,
				ProfessorName = _context.Users.FirstOrDefault(u => u.UserId == e.Course.ProfessorUserId).Username
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

		public void UpdateCourse(CourseDTO updatedCourse)
		{
			var existingCourse = GetCourseById(updatedCourse.CourseId);
			if (existingCourse != null)
			{
				existingCourse.CourseName = updatedCourse.CourseName;
				existingCourse.CourseDescription = updatedCourse.CourseDescription;
				existingCourse.CoursePrice = updatedCourse.CoursePrice;
				existingCourse.CourseSalePrice = updatedCourse.CourseSalePrice;
				existingCourse.CourseImage = updatedCourse.CourseImage;
				_context.SaveChanges();
			}
		}

		public CourseDTO CreateCourse(CourseDTO newCourse)
		{
			var course = new Course
			{
				CourseName = newCourse.CourseName,
				CourseDescription = newCourse.CourseDescription,
				CoursePrice = newCourse.CoursePrice,
				CourseSalePrice = newCourse.CourseSalePrice,
				CourseImage = newCourse.CourseImage,
				ProfessorUserId = newCourse.ProfessorUserId
			};

			_context.Courses.Add(course);
			_context.SaveChanges();

			return new CourseDTO
			{
				CourseId = course.CourseId,
				CourseName = course.CourseName,
				CourseDescription = course.CourseDescription,
				CoursePrice = course.CoursePrice,
				CourseSalePrice = course.CourseSalePrice,
				CourseImage = course.CourseImage,
				ProfessorUserId = course.ProfessorUserId,
				ProfessorName = _context.Users.FirstOrDefault(u => u.UserId == course.ProfessorUserId).Username
			};
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
