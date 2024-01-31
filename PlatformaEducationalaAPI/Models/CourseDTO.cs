namespace PlatformaEducationalaAPI.Models
{
	public class CourseDTO
	{
		public int CourseId { get; set; }
		public required string CourseName { get; set; }
		public required string CourseDescription { get; set; }
		public int CoursePrice { get; set; }
		public int CourseSalePrice { get; set; }
		public required string CourseImage { get; set; }
		public DateTime CourseDate { get; set; }
		public int ProfessorUserId { get; set; }
		public string? ProfessorName { get; set; }
	}
}