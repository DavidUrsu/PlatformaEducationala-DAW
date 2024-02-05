using PlatformaEducationalaAPI.Models;

namespace PlatformaEducationalaAPI.Repositories.UserRepository
{
	public class UserRepository : IUserRepository
	{
		private readonly PlatformaDbContext _context;

		public UserRepository(PlatformaDbContext context)
		{
			_context = context;
		}

		public void AddUser(User user)
		{
			_context.Users.Add(user);
			_context.SaveChanges();
		}

		public void DeleteUser(UserDTO user)
		{
			var userToDelete = _context.Users.Find(user.UserId);
			_context.Users.Remove(userToDelete);
			_context.SaveChanges();
		}

		public User GetUserByEmail(string email)
		{
			return _context.Users.FirstOrDefault(u => u.Email == email);
		}

		public UserDTO GetUserById(int id)
		{
			var user = _context.Users.FirstOrDefault(u => u.UserId == id);
			if (user == null)
			{
				return null;
			}
			return new UserDTO
			{
				UserId = user.UserId,
				Username = user.Username,
				Email = user.Email,
				Role = user.Role
			};
		}

		public void ChangeEmail(int id, string email)
		{
			var user = _context.Users.Find(id);
			user.Email = email;
			_context.SaveChanges();
		}

		public User GetUserByUsername(string username)
		{
			return _context.Users.FirstOrDefault(u => u.Username == username);
		}
	}
}
