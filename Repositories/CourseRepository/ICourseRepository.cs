using PlatformaEducationala_DAW.Models;

namespace PlatformaEducationala_DAW.Repositories.CourseRepository
{
	public interface ICourseRepository
	{
		IEnumerable<Course> GetAllCourses();
		IEnumerable<Course> GetEnrolledCoursesById(int id);
		Course GetCourseById(int id);
		void AddCourse(Course course);
		void DeleteCourse(int id);
		IEnumerable<Enrollment> GetEnrollmentsByCourseId(int id);
		void DeleteEnrollment(Enrollment enrollment);
		void UpdateCourse(int CourseId, string courseName, string courseDescription, int coursePrice, int courseSalePrice, string courseImage);
		void CreateCourse(string courseName, string courseDescription, int coursePrice, string courseImage, int ProfessorId);
		void addEnrollment(Enrollment enrollment);
		ICollection<Course> GetCoursesWhereUserIsTeacher(int id);
	}
}
