using PlatformaEducationalaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PlatformaEducationalaAPI.Repositories.BlogPostRepository
{
	public class BlogPostRepository : IBlogPostRepository
	{
		private readonly PlatformaDbContext _context;

		public BlogPostRepository(PlatformaDbContext context)
		{
			_context = context;
		}

		public BlogPostDTO CreateBlogPost(BlogPostDTO newBlogPost)
		{
			var blogPost = new BlogPost
			{
				Title = newBlogPost.Title,
				Content = newBlogPost.Content,
				ImageUrl = newBlogPost.ImageUrl,
				UserId = newBlogPost.UserId
			};
			_context.BlogPosts.Add(blogPost);
			_context.SaveChanges();

			// Return the new blog post
			return GetBlogPostbyId(blogPost.BlogPostId);
		}

		public IEnumerable<BlogPostDTO> GetAllBlogPosts()
		{
			return _context.BlogPosts
					.Select(bp => new BlogPostDTO
					{
						BlogPostId = bp.BlogPostId,
						Title = bp.Title,
						Content = bp.Content,
						ImageUrl = bp.ImageUrl,
						DatePosted = bp.DatePosted,
						UserId = bp.UserId,
						Username = _context.Users.FirstOrDefault(u => u.UserId == bp.UserId).Username
					})
					.ToList();
		}

		public BlogPostDTO GetBlogPostbyId(int id)
		{
			return _context.BlogPosts.ToLookup(bp => bp.BlogPostId)
				.Select(bp => new BlogPostDTO
				{
					BlogPostId = bp.First().BlogPostId,
					Title = bp.First().Title,
					Content = bp.First().Content,
					ImageUrl = bp.First().ImageUrl,
					DatePosted = bp.First().DatePosted,
					UserId = bp.First().UserId,
					Username = _context.Users.FirstOrDefault(u => u.UserId == bp.First().UserId).Username
				})
				.FirstOrDefault(bp => bp.BlogPostId == id);
		}

		public IEnumerable<BlogPostDTO> GetBlogPostsByUserId(int id)
		{
			return _context.BlogPosts
					.Where(bp => bp.UserId == id)
					.Select(bp => new BlogPostDTO
					{
						BlogPostId = bp.BlogPostId,
						Title = bp.Title,
						Content = bp.Content,
						ImageUrl = bp.ImageUrl,
						DatePosted = bp.DatePosted,
						UserId = bp.UserId,
						Username = _context.Users.FirstOrDefault(u => u.UserId == bp.UserId).Username
					})
					.ToList();
		}

		public void removeBlogPost(BlogPostDTO post)
		{
			// find the blog post with the same id and delete it
			BlogPost blogPost = _context.BlogPosts.FirstOrDefault(bp => bp.BlogPostId == post.BlogPostId);
			if (blogPost != null)
			{
				_context.BlogPosts.Remove(blogPost);
				_context.SaveChanges();
			}

		}

		public void UpdateBlogPost(BlogPostDTO editedBlogPost)
		{
			// find the blog post with the same id and update it
			BlogPost blogPost = _context.BlogPosts.FirstOrDefault(bp => bp.BlogPostId == editedBlogPost.BlogPostId);
			if (blogPost != null)
			{
				blogPost.Title = editedBlogPost.Title;
				blogPost.Content = editedBlogPost.Content;
				blogPost.ImageUrl = editedBlogPost.ImageUrl;
				_context.SaveChanges();
			}
		}
	}
}
