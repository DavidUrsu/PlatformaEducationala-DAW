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

		public BlogPostDTO CreateBlogPost(BlogPostDTO newBlogPost)
		{
			return _blogPostRepository.CreateBlogPost(newBlogPost);
		}

		public void DeleteBlogPost(int id)
		{
			var blogPost = _blogPostRepository.GetBlogPostbyId(id);
			if (blogPost != null)
			{
				_blogPostRepository.removeBlogPost(blogPost);
			}
		}

		public IEnumerable<BlogPostDTO> GetAllBlogPosts()
		{
			return _blogPostRepository.GetAllBlogPosts();
		}

		public BlogPostDTO GetBlogPostById(int id)
		{
			return _blogPostRepository.GetBlogPostbyId(id);
		}

		public void UpdateBlogPost(BlogPostDTO editedBlogPost)
		{
			_blogPostRepository.UpdateBlogPost(editedBlogPost);
		}
	}
}
