using PlatformaEducationalaAPI.Models;

namespace PlatformaEducationalaAPI.Services.CourseService
{
	public interface ICourseService
	{
		IEnumerable<CourseDTO> GetAllCourses();
		IEnumerable<int> GetTheIdOfCourses(IEnumerable<CourseDTO> Courses);
		IEnumerable<int> GetTheIdOfEnrolledCourses(int id);
		Course GetCourseById(int id);
		void deleteCourseEnrollment(int id);
		void deleteCourse(int id);
		void UpdateCourse(CourseDTO updatedCourse);
		CourseDTO CreateCourse(CourseDTO newCourse);
		bool IsUserEnrolledToCourse(int userId, int courseId);
		void EnrollUserToCourse(int userId, int courseId);
		void UnenrollUserFromCourse(int userId, int courseId);
	}
}
