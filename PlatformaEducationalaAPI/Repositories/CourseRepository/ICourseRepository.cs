using PlatformaEducationalaAPI.Models;

namespace PlatformaEducationalaAPI.Repositories.CourseRepository
{
	public interface ICourseRepository
	{
		IEnumerable<CourseDTO> GetAllCourses();
		IEnumerable<CourseDTO> GetEnrolledCoursesByUserId(int id);
		Course GetCourseById(int id);
		void DeleteCourse(int id);
		IEnumerable<Enrollment> GetEnrollmentsByCourseId(int id);
		void DeleteEnrollment(Enrollment enrollment);
		void UpdateCourse(CourseDTO updatedCourse);
		CourseDTO CreateCourse(CourseDTO newCourse);
		void addEnrollment(Enrollment enrollment);
		ICollection<Course> GetCoursesWhereUserIsTeacher(int id);
	}
}
