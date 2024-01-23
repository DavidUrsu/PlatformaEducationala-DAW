using PlatformaEducationalaAPI.Models;

namespace PlatformaEducationalaAPI.Services.BlogPostService
{
	public interface IBlogPostService
	{
		IEnumerable<BlogPost> GetAllBlogPosts();
		BlogPost GetBlogPostById(int id);
		void CreateBlogPost(BlogPost post);
		BlogPost CreateBlogPost(string blogPostTitle, string blogPostContent, string blogPostImage, int UserId);
		void DeleteBlogPost(int id);
		void UpdateBlogPost(int blogPostId, string blogPostTitle, string BlogPostContent, string BlogPostImage);
	}
}
