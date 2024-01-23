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

		public void DeleteUser(User user)
		{
			_context.Users.Remove(user);
			_context.SaveChanges();
		}

		public User GetUserByEmail(string email)
		{
			return _context.Users.FirstOrDefault(u => u.Email == email);
		}

		public User GetUserById(int id)
		{
			return _context.Users.Find(id);
		}

		public void ChangeEmail(int id, string email)
		{
			GetUserById(id).Email = email;
			_context.SaveChanges();
		}

		public User GetUserByUsername(string username)
		{
			return _context.Users.FirstOrDefault(u => u.Username == username);
		}
	}
}
