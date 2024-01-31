using PlatformaEducationalaAPI.Models;

namespace PlatformaEducationalaAPI.Repositories.BlogPostRepository
{
	public interface IBlogPostRepository
	{
		//Get all the blog posts
		public IEnumerable<BlogPostDTO> GetAllBlogPosts();
		public BlogPostDTO GetBlogPostbyId(int id);
		public BlogPostDTO CreateBlogPost(BlogPostDTO newBlogPost);
		void removeBlogPost(BlogPostDTO post);
		void UpdateBlogPost(BlogPostDTO editedBlogPost);
		public IEnumerable<BlogPostDTO> GetBlogPostsByUserId(int id);
	}
}
