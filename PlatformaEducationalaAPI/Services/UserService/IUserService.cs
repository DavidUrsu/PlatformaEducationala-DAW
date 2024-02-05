using PlatformaEducationalaAPI.Models;

namespace PlatformaEducationalaAPI.Services.UserService
{
	public interface IUserService
	{
		// Returns the a User object by id
		UserDTO GetUserById(int id);

		// Deletes the each course of the user by the id of the user
		void DeleteCoursesOfUser(int id);

		// Deletes each blog post of the user by the id of the user
		void DeleteBlogPostsOfUser(int id);

		// Unenrolls the user from all courses by the id of the user
		void UnenrollUserFromAllCourses(int id);

		// Changes the email of the user by the id of the user
		// Returns true if the email was changed successfully
		// Returns false if the email is already used by another user
		bool ChangeEmail(int id, string email);

		// Creates a new user
		// Returns a string with the result of the operation
		// "Email already used" if the email is already used by another user
		// "Username already used" if the username is already used by another user
		// "User created" if the user was created successfully
		string CreateUser(string username, string email, string password);

		// Returns a User object by username
		User GetUserByUsername(string username);

		// Searches the database for a user with the given username and password
		// Returns a string with the result of the operation
		// "Username does not exist" if the user was not found
		// "Wrong password" if the password is incorrect
		// "Login successful" if the user was found
		string Login(string username, string password);

		// Returns a list of courses where the user is enrolled by the id of the user
		IEnumerable<CourseDTO> GetCoursesWhereUserIsEnrolled(int id);

		//Deletes the user by the id of the user
		//Deletes all the courses of the user
		//Deletes all the enrollments of the user
		//Delets all the blogs of the user
		bool DeleteUser(int id);
	}
}
