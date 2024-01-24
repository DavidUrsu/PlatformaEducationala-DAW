using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformaEducationalaAPI.Models;
using PlatformaEducationalaAPI.Services.BlogPostService;

namespace PlatformaEducationalaAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BlogPostController : ControllerBase
	{
		private readonly IBlogPostService _blogPostService;

		public BlogPostController(IBlogPostService blogPostService)
		{
			_blogPostService = blogPostService;
		}

		[HttpGet(Name = "GetAllBlogPosts")]
		public ActionResult<IEnumerable<BlogPost>> GetAllBlogPosts()
		{
			return Ok(_blogPostService.GetAllBlogPosts().ToArray());
		}

		//get the details of a blog post
		[HttpGet("{id}")]
		public ActionResult<BlogPost> Post(int id)
		{
			// check is there is a post with the given id
			var post = _blogPostService.GetBlogPostById(id);
			if (post == null)
			{
				return NotFound();
			}
			return Ok(post);
		}

		//create a new blog post
		[HttpPost]
		public ActionResult<BlogPost> Create(string blogPostTitle, string blogPostContent, string blogPostImage)
		{
			var userId = Request.Cookies["id"];
			//check if the user is logged in
			if (userId == null)
			{
				return Unauthorized();
			}
			else
			{
				// check if the fields are valid
				if (blogPostTitle == null || blogPostContent == null || blogPostImage == null)
				{
					return BadRequest();
				}

				var newPost = _blogPostService.CreateBlogPost(blogPostTitle, blogPostContent, blogPostImage, int.Parse(userId));
				return CreatedAtAction("GetAllBlogPosts", new { id = newPost.BlogPostId }, newPost);
			}
		}

		//delete a blog post
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var UserId = Request.Cookies["id"];
			if (UserId == null)
			{
				return Unauthorized();
			}
			else
			{
				_blogPostService.DeleteBlogPost(id);
				return NoContent();
			}
		}

		//edit a blog post
		[HttpPut("edit/{id}", Name = "editBlogPost")]
		public IActionResult Edit(int id, string blogPostTitle, string blogPostContent, string blogPostImage)
		{
			var UserId = Request.Cookies["id"];
			//check if the user is logged in
			if (UserId == null)
			{
				return Unauthorized();
			}
			else if (blogPostTitle == null || blogPostContent == null || blogPostImage == null)
			{
				//check if the fields are valid
				return BadRequest();
			}
			else
			{
				//check if there is a post with the given id
				var existingPost = _blogPostService.GetBlogPostById(id);
				if (existingPost == null)
				{
					return NotFound();
				}
				else
				{
					//check if the user is the author of the post
					if (existingPost.UserId == int.Parse(UserId))
					{
						_blogPostService.UpdateBlogPost(id, blogPostTitle, blogPostContent, blogPostImage);
						return NoContent();
					}
					else
					{
						var message = id + "a" + blogPostTitle + "b" + blogPostContent + "c" + blogPostImage;
						return Unauthorized(message);
					}
				}
			}
		}
	}
}
