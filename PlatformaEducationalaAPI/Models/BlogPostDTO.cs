namespace PlatformaEducationalaAPI.Models
{
	public class BlogPostDTO
	{
		public int BlogPostId { get; set; }
		public required string Title { get; set; }
		public required string Content { get; set; }
		public required string ImageUrl { get; set; }
		public DateTime DatePosted { get; set; }
		public int UserId { get; set; }
		public string? Username { get; set; }
	}
}