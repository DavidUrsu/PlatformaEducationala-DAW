using PlatformaEducationala_DAW.Models;

namespace PlatformaEducationala_DAW.Services.BlogPostService
{
	public interface IBlogPostService
	{
		IEnumerable<BlogPost> GetAllBlogPosts();
		BlogPost GetBlogPostById(int id);
		void CreateBlogPost(string blogPostTitle, string blogPostContent, string blogPostImage, int UserId);
		void DeleteBlogPost(int id);
		void UpdateBlogPost(BlogPost post, string blogPostTitle, string BlogPostContent, string BlogPostImage);
	}
}
