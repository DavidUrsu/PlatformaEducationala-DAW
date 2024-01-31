using PlatformaEducationalaAPI.Models;

namespace PlatformaEducationalaAPI.Services.BlogPostService
{
	public interface IBlogPostService
	{
		IEnumerable<BlogPostDTO> GetAllBlogPosts();
		BlogPostDTO GetBlogPostById(int id);
		BlogPostDTO CreateBlogPost(BlogPostDTO newBlogPost);
		void DeleteBlogPost(int id);
		void UpdateBlogPost(BlogPostDTO editedBlogPost);
	}
}
