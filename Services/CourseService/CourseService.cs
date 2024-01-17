using PlatformaEducationala_DAW.Repositories.CourseRepository;
using PlatformaEducationala_DAW.Models;
using Microsoft.EntityFrameworkCore;

namespace PlatformaEducationala_DAW.Services.CourseService
{
	public class CourseService : ICourseService
	{
		private readonly ICourseRepository _courseRepository;

		public CourseService(ICourseRepository repository)
		{
			_courseRepository = repository;
		}

		public Course GetCourseById(int id)
		{
			return _courseRepository.GetCourseById(id);
		}

		public IEnumerable<Course> GetAllCourses()
		{
			return _courseRepository.GetAllCourses();
		}

		public IEnumerable<int> GetTheIdOfCourses(IEnumerable<Course> Courses)
		{
			var ids = new List<int>();
			foreach (var course in Courses)
			{
				ids.Add(course.CourseId);
			}
			return ids;
		}

		public IEnumerable<int> GetTheIdOfEnrolledCourses(int id)
		{
			return GetTheIdOfCourses(_courseRepository.GetEnrolledCoursesById(id));
		}

		public void deleteCourseEnrollment(int id)
		{
			//delete all the enrollments of the course
			var enrollments = _courseRepository.GetEnrollmentsByCourseId(id);
			foreach (var enrollment in enrollments)
			{
				_courseRepository.DeleteEnrollment(enrollment);
			}
		}

		public void deleteCourse(int id)
		{
			//delete all the enrollments of the course
			deleteCourseEnrollment(id);
			//delete the course
			_courseRepository.DeleteCourse(id);
		}

		public void UpdateCourse(int CourseId, string courseName, string courseDescription, int coursePrice, int courseSalePrice, string courseImage)
		{
			_courseRepository.UpdateCourse(CourseId, courseName, courseDescription, coursePrice, courseSalePrice, courseImage);
		}

		public void CreateCourse(string courseName, string courseDescription, int coursePrice, string courseImage, int ProfessorId)
		{
			_courseRepository.CreateCourse(courseName, courseDescription, coursePrice, courseImage, ProfessorId);
		}

		public bool IsUserEnrolledToCourse(int userId, int courseId)
		{
			var enrollments = _courseRepository.GetEnrollmentsByCourseId(courseId);
			foreach (var enrollment in enrollments)
			{
				if (enrollment.UserId == userId)
				{
					return true;
				}
			}
			return false;
		}

		public void EnrollUserToCourse(int userId, int courseId)
		{
			// check if the user is already enrolled
			if (IsUserEnrolledToCourse(userId, courseId))
			{
				return;
			}
			// create the enrollment
			Enrollment newEnrollment = new Enrollment();
			newEnrollment.UserId = userId;
			newEnrollment.CourseId = courseId;
			_courseRepository.addEnrollment(newEnrollment);
		}

		public void UnenrollUserFromCourse(int userId, int courseId)
		{
			// check if the user is already enrolled
			if (!IsUserEnrolledToCourse(userId, courseId))
			{
				return;
			}
			// delete the enrollment
			var enrollments = _courseRepository.GetEnrollmentsByCourseId(courseId);
			foreach (var enrollment in enrollments)
			{
				if (enrollment.UserId == userId)
				{
					_courseRepository.DeleteEnrollment(enrollment);
					return;
				}
			}
		}
	}
}
