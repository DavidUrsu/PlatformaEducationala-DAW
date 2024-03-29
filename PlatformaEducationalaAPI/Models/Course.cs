﻿namespace PlatformaEducationalaAPI.Models
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

        // Navigation property for the professor
        public User Professor { get; set; }

        // Navigation property for enrollments
        public ICollection<Enrollment> Enrollments { get; set; }

        public Course()
        {
            CourseDate = DateTime.Now;
        }
    }
}
