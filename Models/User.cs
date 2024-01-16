namespace PlatformaEducationala_DAW.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        // Navigation property for courses taught by the user
        public ICollection<Course> TaughtCourses { get; set; }

        // Navigation property for blog posts
        public ICollection<BlogPost> BlogPosts { get; set; }


        // Navigation property for enrollments
        public ICollection<Enrollment> Enrollments { get; set; }

        public User() { }
    }
}
