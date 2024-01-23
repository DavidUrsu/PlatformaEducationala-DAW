namespace PlatformaEducationalaAPI.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        // Foreign key to User
        public int UserId { get; set; }
        public User User { get; set; }

        // Foreign key to Course
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
