using PlatformaEducationala_DAW.Models;

namespace PlatformaEducationala_DAW.Repositories.UserRepository
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
