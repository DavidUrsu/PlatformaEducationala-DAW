using PlatformaEducationalaAPI.Models;

namespace PlatformaEducationalaAPI.Repositories.CourseRepository
{
	public interface ICourseRepository
	{
		IEnumerable<Course> GetAllCourses();
		IEnumerable<CourseDTO> GetEnrolledCoursesByUserId(int id);
		Course GetCourseById(int id);
		void AddCourse(Course course);
		void DeleteCourse(int id);
		IEnumerable<Enrollment> GetEnrollmentsByCourseId(int id);
		void DeleteEnrollment(Enrollment enrollment);
		void UpdateCourse(int CourseId, string courseName, string courseDescription, int coursePrice, int courseSalePrice, string courseImage);
		void UpdateCourse(Course course);
		Course CreateCourse(string courseName, string courseDescription, int coursePrice, string courseImage, int ProfessorId);
		void addEnrollment(Enrollment enrollment);
		ICollection<Course> GetCoursesWhereUserIsTeacher(int id);
	}
}
