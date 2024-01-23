using PlatformaEducationalaAPI.Models;

namespace PlatformaEducationalaAPI.Repositories.BlogPostRepository
{
	public interface IBlogPostRepository
	{
		//Get all the blog posts
		public IEnumerable<BlogPost> GetAllBlogPosts();
		public BlogPost GetBlogPostbyId(int id);
		public void CreateBlogPost(BlogPost post);
		public BlogPost CreateBlogPost(string blogPostTitle, string blogPostContent, string blogPostImage, int userId);
		void removeBlogPost(BlogPost post);
		void UpdateBlogPost(BlogPost updatedBlogPost);
		void UpdateBlogPost(int id, string blogPostTitle, string blogPostContent, string blogPostImage);
		public IEnumerable<BlogPost> GetBlogPostsByUserId(int id);
	}
}
