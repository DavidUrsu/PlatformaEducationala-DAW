using PlatformaEducationala_DAW.Models;
using PlatformaEducationala_DAW.Repositories.BlogPostRepository;

namespace PlatformaEducationala_DAW.Services.BlogPostService
{
	public class BlogPostService : IBlogPostService
	{
		private readonly IBlogPostRepository _blogPostRepository;

		public BlogPostService(IBlogPostRepository repository)
		{
			_blogPostRepository = repository;
		}

		public void CreateBlogPost(string blogPostTitle, string blogPostContent, string blogPostImage, int UserId)
		{
			_blogPostRepository.CreateBlogPost(blogPostTitle, blogPostContent, blogPostImage, UserId);
		}

		public void DeleteBlogPost(int id)
		{
			var blogPost = _blogPostRepository.GetBlogPostbyId(id);
			if (blogPost != null)
			{
				_blogPostRepository.removeBlogPost(blogPost);
			}
		}

		public IEnumerable<BlogPost> GetAllBlogPosts()
		{
			return _blogPostRepository.GetAllBlogPosts();
		}

		public BlogPost GetBlogPostById(int id)
		{
			return _blogPostRepository.GetBlogPostbyId(id);
		}

		public void UpdateBlogPost(BlogPost post, string blogPostTitle, string BlogPostContent, string BlogPostImage)
		{
			_blogPostRepository.UpdateBlogPost(post, blogPostTitle, BlogPostContent, BlogPostImage);
		}
	}
}
