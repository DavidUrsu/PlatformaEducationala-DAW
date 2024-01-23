using PlatformaEducationalaAPI.Models;

namespace PlatformaEducationalaAPI.Services.CourseService
{
	public interface ICourseService
	{
		IEnumerable<Course> GetAllCourses();
		IEnumerable<int> GetTheIdOfCourses(IEnumerable<CourseDTO> Courses);
		IEnumerable<int> GetTheIdOfEnrolledCourses(int id);
		Course GetCourseById(int id);
		void deleteCourseEnrollment(int id);
		void deleteCourse(int id);
		void UpdateCourse(int CourseId, string courseName, string courseDescription, int coursePrice, int courseSalePrice, string courseImage);
		void UpdateCourse(Course course);
		Course CreateCourse(string courseName, string courseDescription, int coursePrice, string courseImage, int ProfessorId);
		void CreateCourse(Course newCourse);
		bool IsUserEnrolledToCourse(int userId, int courseId);
		void EnrollUserToCourse(int userId, int courseId);
		void UnenrollUserFromCourse(int userId, int courseId);
	}
}
