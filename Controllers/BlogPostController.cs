using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformaEducationala_DAW.Models;
using PlatformaEducationala_DAW.Services.BlogPostService;

namespace PlatformaEducationala_DAW.Controllers
{
	public class BlogPostController : Controller
	{
		private readonly IBlogPostService _blogPostService;

		public BlogPostController(IBlogPostService blogPostService)
		{
			_blogPostService = blogPostService;
		}

		public IActionResult Index()
		{
			ViewBag.Posts = _blogPostService.GetAllBlogPosts();
			return View();
		}

		//get the details of a blog post
		public IActionResult Post(int id)
		{
			// check is there is a post with the given id
			var post = _blogPostService.GetBlogPostById(id);
			if (post == null)
			{
				return RedirectToAction("Index", "BlogPost");
			}
			else
			{
				ViewBag.Post = post;
				return View();
			}
		}

		// create blog post
		public IActionResult Create()
		{
			//check if the user is logged in
			if (Request.Cookies["id"] == null)
			{
				return RedirectToAction("Login", "User");
			}
			else
			{
				return View();
			}
		}

		//create a new blog post
		[HttpPost]
		public IActionResult Create(string blogPostTitle, string blogPostContent, string blogPostImage)
		{
			var UserId = Request.Cookies["id"];
			//check if the user is logged in
			if (UserId == null)
			{
				return RedirectToAction("Login", "User");
			}
			else
			{
				//check if the fields are not empty
				if (blogPostTitle == null || blogPostContent == null || blogPostImage == null)
				{
					return RedirectToAction("Index", "BlogPost");
				}
				else
				{
					//create the new post
					_blogPostService.CreateBlogPost(blogPostTitle, blogPostContent, blogPostImage, int.Parse(UserId));

					return RedirectToAction("Index", "BlogPost");
				}
			}
		}

		//delete a blog post
		public IActionResult Delete(int id)
		{
			var UserId = Request.Cookies["id"];
			//check if the user is logged in
			if (UserId == null)
			{
				return RedirectToAction("Login", "User");
			}
			else
			{
				_blogPostService.DeleteBlogPost(id);
				return RedirectToAction("Index", "BlogPost");
			}
		}

		//edit a blog post
		public IActionResult Edit(int id)
		{
			var UserId = Request.Cookies["id"];
			//check if the user is logged in
			if (UserId == null)
			{
				return RedirectToAction("Login", "User");
			}
			else
			{
				//check if there is a post with the given id
				var post = _blogPostService.GetBlogPostById(id);
				if (post == null)
				{
					return RedirectToAction("Index", "BlogPost");
				}
				else
				{
					//check if the user is the author of the post
					if (post.UserId == int.Parse(UserId))
					{
						ViewBag.Post = post;
						return View();
					}
					else
					{
						return RedirectToAction("Index", "BlogPost");
					}
				}
			}
		}

		//edit a blog post
		[HttpPost]
		public IActionResult Edit(int id, string blogPostTitle, string BlogPostContent, string BlogPostImage)
		{
			var UserId = Request.Cookies["id"];
			//check if the user is logged in
			if (UserId == null)
			{
				return RedirectToAction("Login", "User");
			}
			else
			{
				//check if there is a post with the given id
				var post = _blogPostService.GetBlogPostById(id);
				if (post == null)
				{
					return RedirectToAction("Index", "BlogPost");
				}
				else
				{
					//check if the user is the author of the post
					if (post.UserId == int.Parse(UserId))
					{
						//check if the fields are not empty
						if (blogPostTitle == null || BlogPostContent == null || BlogPostImage == null)
						{
							return RedirectToAction("Index", "BlogPost");
						}
						else
						{
							_blogPostService.UpdateBlogPost(post, blogPostTitle, BlogPostContent, BlogPostImage);

							return RedirectToAction("Index", "BlogPost");
						}
					}
					else
					{
						return RedirectToAction("Index", "BlogPost");
					}
				}
			}
		}
	}
}
