namespace PlatformaEducationala_DAW.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public ICollection<Course> TaughtCourses { get; set; }

        public User() { }
    }
}
