using PlatformaEducationala_DAW.Models;

namespace PlatformaEducationala_DAW.Services.CourseService
{
	public interface ICourseService
	{
		IEnumerable<Course> GetAllCourses();
		IEnumerable<int> GetTheIdOfCourses(IEnumerable<Course> Courses);
		IEnumerable<int> GetTheIdOfEnrolledCourses(int id);
		Course GetCourseById(int id);
		void deleteCourseEnrollment(int id);
		void deleteCourse(int id);
		void UpdateCourse(int CourseId, string courseName, string courseDescription, int coursePrice, int courseSalePrice, string courseImage);
		void CreateCourse(string courseName, string courseDescription, int coursePrice, string courseImage, int ProfessorId);
		bool IsUserEnrolledToCourse(int userId, int courseId);
		void EnrollUserToCourse(int userId, int courseId);
		void UnenrollUserFromCourse(int userId, int courseId);
	}
}
