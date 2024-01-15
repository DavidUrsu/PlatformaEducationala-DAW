namespace PlatformaEducationala_DAW.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set;}
        public int CoursePrice { get; set; }
        public int CourseSalePrice { get; set; }
        public string CourseImage { get; set; }
        public DateTime CourseDate { get; set; }
        public int ProfessorUserId { get; set; }

        public User Professor { get; set; }

        public Course()
        {
            CourseDate = DateTime.Now;
        }
    }
}
