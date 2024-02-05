using PlatformaEducationalaAPI.Models;
using PlatformaEducationalaAPI.Repositories.CourseRepository;
using PlatformaEducationalaAPI.Repositories.UserRepository;
using PlatformaEducationalaAPI.Services.CourseService;
using System.Text;
using System.Security.Cryptography;
using PlatformaEducationalaAPI.Services.BlogPostService;
using PlatformaEducationalaAPI.Repositories.BlogPostRepository;

namespace PlatformaEducationalaAPI.Services.UserService
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly ICourseRepository _courseRepository;
		private readonly ICourseService _courseService;
		private readonly IBlogPostRepository _blogPostRepository;

		public UserService(IUserRepository userRepository, ICourseRepository courseRepository, ICourseService courseService, IBlogPostRepository blogPostRepository)
		{
			_userRepository = userRepository;
			_courseRepository = courseRepository;
			_courseService = courseService;
			_blogPostRepository = blogPostRepository;
		}

		// function hash password
		public static string HashPassword(string password)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// Convert the input string to a byte array and compute the hash.
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

				// Convert byte array to a string
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}

		public bool ChangeEmail(int id, string email)
		{
			if (_userRepository.GetUserByEmail(email) == null)
			{
				_userRepository.ChangeEmail(id, email);
				return true;
			}
			return false;
		}

		public string CreateUser(string username, string email, string password)
		{
			// check if the email is already used
			if (_userRepository.GetUserByEmail(email) != null)
			{
				return "Email already used";
			}
			// check if the username is already used
			if (_userRepository.GetUserByUsername(username) != null)
			{
				return "Username already used";
			}
			// create the user
			var user = new User
			{
				Username = username,
				Email = email,
				PasswordHash = HashPassword(password),
				Role = "User"
			};
			_userRepository.AddUser(user);
			return "User created";
		}

		public void DeleteBlogPostsOfUser(int id)
		{
			var blogPosts = _blogPostRepository.GetBlogPostsByUserId(id);

			foreach (var blogPost in blogPosts)
			{
				_blogPostRepository.removeBlogPost(blogPost);
			}
		}

		public void DeleteCoursesOfUser(int id)
		{
			var courses = _courseRepository.GetCoursesWhereUserIsTeacher(id);
			foreach (var course in courses)
			{
				_courseService.deleteCourse(course.CourseId);
			}
		}

		public bool DeleteUser(int id)
		{
			var User = _userRepository.GetUserById(id);
			if (User == null)
			{
				return false;
			}
			
			DeleteCoursesOfUser(id);

			DeleteBlogPostsOfUser(id);

			UnenrollUserFromAllCourses(id);

			_userRepository.DeleteUser(User);

			return true;
		}

		public IEnumerable<CourseDTO> GetCoursesWhereUserIsEnrolled(int id)
		{
			// get the courses where the user is enrolled
			return _courseRepository.GetEnrolledCoursesByUserId(id);
		}

		public UserDTO GetUserById(int id)
		{
			return _userRepository.GetUserById(id);
		}

		public User GetUserByUsername(string username)
		{
			return _userRepository.GetUserByUsername(username);
		}

		public string Login(string username, string password)
		{
			// check if the email exists
			var user = _userRepository.GetUserByUsername(username);
			if (user == null)
			{
				return "Username does not exist";
			}
			// check if the password is correct
			if (user.PasswordHash != HashPassword(password))
			{
				return "Wrong password";
			}
			return "Login successful";
		}

		public void UnenrollUserFromAllCourses(int id)
		{
			// get the courses where the user is enrolled
			var courses = _courseRepository.GetEnrolledCoursesByUserId(id);
			// unenroll the user from all the courses
			foreach (var course in courses)
			{
				_courseService.UnenrollUserFromCourse(id, course.CourseId);
			}
		}
	}
}
