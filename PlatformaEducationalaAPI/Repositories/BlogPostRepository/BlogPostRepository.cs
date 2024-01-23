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

		public void CreateBlogPost(BlogPost post)
		{
			_context.BlogPosts.Add(post);
			_context.SaveChanges();
		}

		public BlogPost CreateBlogPost(string blogPostTitle, string blogPostContent, string blogPostImage, int userId)
		{
			var blogPost = new BlogPost
			{
				Title = blogPostTitle,
				Content = blogPostContent,
				ImageUrl = blogPostImage,
				UserId = userId
			};

			_context.BlogPosts.Add(blogPost);
			_context.SaveChanges();

			return blogPost;
		}

		public IEnumerable<BlogPost> GetAllBlogPosts()
		{
			return _context.BlogPosts
				.ToList();
		}

		public BlogPost GetBlogPostbyId(int id)
		{
			return _context.BlogPosts
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

		public void UpdateBlogPost(BlogPost updatedBlogPost)
		{
			var existingPost = GetBlogPostbyId(updatedBlogPost.BlogPostId);

			if(existingPost != null)
			{
				existingPost.Title = updatedBlogPost.Title;
				existingPost.Content = updatedBlogPost.Content;
				existingPost.ImageUrl = updatedBlogPost.ImageUrl;

				_context.BlogPosts.Update(existingPost);
				_context.SaveChanges();
			}
		}

		public void UpdateBlogPost(int id, string blogPostTitle, string blogPostContent, string blogPostImage)
		{
			var existingPost = GetBlogPostbyId(id);

			if (existingPost != null)
			{
				existingPost.Title = blogPostTitle;
				existingPost.Content = blogPostContent;
				existingPost.ImageUrl = blogPostImage;

				_context.BlogPosts.Update(existingPost);
				_context.SaveChanges();
			}
		}
	}
}
