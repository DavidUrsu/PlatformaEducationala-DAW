using PlatformaEducationala_DAW.Models;

namespace PlatformaEducationala_DAW.Repositories.BlogPostRepository
{
	public interface IBlogPostRepository
	{
		//Get all the blog posts
		public IEnumerable<BlogPost> GetAllBlogPosts();
		public BlogPost GetBlogPostbyId(int id);
		public void CreateBlogPost(string blogPostTitle, string blogPostContent, string blogPostImage, int UserId);
		void removeBlogPost(BlogPost post);
		void UpdateBlogPost(BlogPost post, string blogPostTitle, string BlogPostContent, string BlogPostImage);
		public IEnumerable<BlogPost> GetBlogPostsByUserId(int id);
	}
}
