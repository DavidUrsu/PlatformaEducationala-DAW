using PlatformaEducationalaAPI.Models;
using PlatformaEducationalaAPI.Repositories.BlogPostRepository;

namespace PlatformaEducationalaAPI.Services.BlogPostService
{
	public class BlogPostService : IBlogPostService
	{
		private readonly IBlogPostRepository _blogPostRepository;

		public BlogPostService(IBlogPostRepository repository)
		{
			_blogPostRepository = repository;
		}

		public void CreateBlogPost(BlogPost post)
		{
			_blogPostRepository.CreateBlogPost(post);
		}

		public BlogPost CreateBlogPost(string blogPostTitle, string blogPostContent, string blogPostImage, int userId)
		{
			return _blogPostRepository.CreateBlogPost(blogPostTitle, blogPostContent, blogPostImage, userId);
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

		public void UpdateBlogPost(int blogPostId, string blogPostTitle, string BlogPostContent, string BlogPostImage)
		{
			_blogPostRepository.UpdateBlogPost(blogPostId, blogPostTitle, BlogPostContent, BlogPostImage);
		}
	}
}
