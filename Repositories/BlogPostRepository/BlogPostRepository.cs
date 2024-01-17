using PlatformaEducationala_DAW.Models;
using Microsoft.EntityFrameworkCore;

namespace PlatformaEducationala_DAW.Repositories.BlogPostRepository
{
	public class BlogPostRepository : IBlogPostRepository
	{
		private readonly PlatformaDbContext _context;

		public BlogPostRepository(PlatformaDbContext context)
		{
			_context = context;
		}

		public void CreateBlogPost(string blogPostTitle, string blogPostContent, string blogPostImage, int UserId)
		{
			var blogPost = new BlogPost
			{
				Title = blogPostTitle,
				Content = blogPostContent,
				ImageUrl = blogPostImage,
				UserId = UserId
			};

			_context.BlogPosts.Add(blogPost);
			_context.SaveChanges();
		}

		public IEnumerable<BlogPost> GetAllBlogPosts()
		{
			return _context.BlogPosts
				.Include(bp => bp.User)
				.ToList();
		}

		public BlogPost GetBlogPostbyId(int id)
		{
			return _context.BlogPosts
				.Include(bp => bp.User)
				.FirstOrDefault(bp => bp.BlogPostId == id);
		}

		public IEnumerable<BlogPost> GetBlogPostsByUserId(int id)
		{
			return _context.BlogPosts
				.Where(bp => bp.UserId == id)
				.ToList();
		}

		public void removeBlogPost(BlogPost post)
		{
			_context.BlogPosts.Remove(post);
			_context.SaveChanges();
		}

		public void UpdateBlogPost(BlogPost post, string blogPostTitle, string BlogPostContent, string BlogPostImage)
		{
			post.Title = blogPostTitle;
			post.Content = BlogPostContent;
			post.ImageUrl = BlogPostImage;
			_context.SaveChanges();
		}
	}
}
