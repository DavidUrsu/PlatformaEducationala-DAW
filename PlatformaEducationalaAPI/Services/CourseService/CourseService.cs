using PlatformaEducationalaAPI.Repositories.CourseRepository;
using PlatformaEducationalaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PlatformaEducationalaAPI.Services.CourseService
{
	public class CourseService : ICourseService
	{
		private readonly ICourseRepository _courseRepository;

		public CourseService(ICourseRepository repository)
		{
			_courseRepository = repository;
		}

		public CourseDTO GetCourseById(int id)
		{
			return _courseRepository.GetCourseById(id);
		}

		public IEnumerable<CourseDTO> GetAllCourses()
		{
			return _courseRepository.GetAllCourses();
		}

		public IEnumerable<int> GetTheIdOfCourses(IEnumerable<CourseDTO> Courses)
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
			return GetTheIdOfCourses(_courseRepository.GetEnrolledCoursesByUserId(id));
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

		public void UpdateCourse(CourseDTO updatedCourse)
		{
			_courseRepository.UpdateCourse(updatedCourse);
		}

		public CourseDTO CreateCourse(CourseDTO newCourse)
		{
			return _courseRepository.CreateCourse(newCourse);
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
