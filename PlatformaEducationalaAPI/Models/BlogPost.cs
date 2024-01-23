namespace PlatformaEducationalaAPI.Models
{
	public class BlogPost
	{
		public int BlogPostId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string ImageUrl { get; set; }
		public DateTime DatePosted { get; set; }
		public int UserId { get; set; }

		// Navigation properties
		public User User { get; set; }

		public BlogPost()
		{
			DatePosted = DateTime.Now;
		}
	}
}
