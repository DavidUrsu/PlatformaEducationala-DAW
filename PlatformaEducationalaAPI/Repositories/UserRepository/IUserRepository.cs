using PlatformaEducationalaAPI.Models;

namespace PlatformaEducationalaAPI.Repositories.UserRepository
{
	public interface IUserRepository
	{
		User GetUserById(int id);
		User GetUserByEmail(string email);
		User GetUserByUsername(string username);
		void AddUser(User user);
		void DeleteUser(User user);
		void ChangeEmail(int id, string email);
	}
}
