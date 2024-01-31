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
		public ActionResult<IEnumerable<BlogPostDTO>> GetAllBlogPosts()
		{
			return Ok(_blogPostService.GetAllBlogPosts().ToArray());
		}

		//get the details of a blog post
		[HttpGet("{id}")]
		public ActionResult<BlogPostDTO> Post(int id)
		{
			// check if there is a post with the given id
			var post = _blogPostService.GetBlogPostById(id);
			if (post == null)
			{
				return NotFound();
			}
			return Ok(post);
		}

		//create a new blog post
		[HttpPost]
		public ActionResult<BlogPost> Create(BlogPostDTO newBlogPost)
		{
			var userId = Request.Cookies["id"];
			//check if the user is logged in
			if (userId == null)
			{
				return Unauthorized();
			}
			else
			{
				// set the user id
				newBlogPost.UserId = int.Parse(userId);
				// check if the fields are valid
				if (newBlogPost.Title == null || newBlogPost.Content == null || newBlogPost.ImageUrl == null)
				{
					return BadRequest();
				}

				var newPost = _blogPostService.CreateBlogPost(newBlogPost);
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
		[HttpPut]
		public IActionResult Edit(BlogPostDTO editedBlogPost)
		{
			var UserId = Request.Cookies["id"];
			//check if the user is logged in
			if (UserId == null)
			{
				return Unauthorized();
			}
			else if (editedBlogPost.Title == null || editedBlogPost.Content == null || editedBlogPost.ImageUrl == null)
			{
				//check if the fields are valid
				return BadRequest();
			}
			else
			{
				//check if there is a post with the given id
				var existingPost = _blogPostService.GetBlogPostById(editedBlogPost.BlogPostId);
				if (existingPost == null)
				{
					return NotFound();
				}
				else
				{
					//check if the user is the author of the post
					if (existingPost.UserId == int.Parse(UserId))
					{
						_blogPostService.UpdateBlogPost(editedBlogPost);
						return NoContent();
					}
					else
					{
						return Unauthorized();
					}
				}
			}
		}
	}
}
