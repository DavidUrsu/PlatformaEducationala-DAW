using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformaEducationala_DAW.Models;

namespace PlatformaEducationala_DAW.Controllers
{
	public class BlogPostController : Controller
	{
		private readonly PlatformaDbContext _context;
		public BlogPostController(PlatformaDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			//get all the posts from the database
			var posts = _context.BlogPosts
							.Include(bp => bp.User)
							.ToList();

			ViewBag.Posts = posts;
			return View();
		}

		//get the details of a blog post
		public IActionResult Post(int id)
		{
			// check is there is a post with the given id
			var post = _context.BlogPosts
							.Include(bp => bp.User)
							.FirstOrDefault(bp => bp.BlogPostId == id);
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
		public IActionResult Create(string blogPostTitle, string BlogPostContent, string BlogPostImage)
		{
			//check if the user is logged in
			if (Request.Cookies["id"] == null)
			{
				return RedirectToAction("Login", "User");
			}
			else
			{
				//check if the fields are not empty
				if (blogPostTitle == null || BlogPostContent == null || BlogPostImage == null)
				{
					return RedirectToAction("Index", "BlogPost");
				}
				else
				{
					//create the new post
					var post = new BlogPost
					{
						Title = blogPostTitle,
						Content = BlogPostContent,
						ImageUrl = BlogPostImage,
						UserId = int.Parse(Request.Cookies["id"])
					};

					_context.BlogPosts.Add(post);
					_context.SaveChanges();

					return RedirectToAction("Index", "BlogPost");
				}
			}
		}

		//delete a blog post
		public IActionResult Delete(int id)
		{
			//check if the user is logged in
			if (Request.Cookies["id"] == null)
			{
				return RedirectToAction("Login", "User");
			}
			else
			{
				//check if there is a post with the given id
				var post = _context.BlogPosts
								.Include(bp => bp.User)
								.FirstOrDefault(bp => bp.BlogPostId == id);
				if (post == null)
				{
					return RedirectToAction("Index", "BlogPost");
				}
				else
				{
					//check if the user is the author of the post
					if (post.UserId == int.Parse(Request.Cookies["id"]))
					{
						_context.BlogPosts.Remove(post);
						_context.SaveChanges();
					}
					return RedirectToAction("Index", "BlogPost");
				}
			}
		}

		//edit a blog post
		public IActionResult Edit(int id)
		{
			//check if the user is logged in
			if (Request.Cookies["id"] == null)
			{
				return RedirectToAction("Login", "User");
			}
			else
			{
				//check if there is a post with the given id
				var post = _context.BlogPosts
								.Include(bp => bp.User)
								.FirstOrDefault(bp => bp.BlogPostId == id);
				if (post == null)
				{
					return RedirectToAction("Index", "BlogPost");
				}
				else
				{
					//check if the user is the author of the post
					if (post.UserId == int.Parse(Request.Cookies["id"]))
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
			//check if the user is logged in
			if (Request.Cookies["id"] == null)
			{
				return RedirectToAction("Login", "User");
			}
			else
			{
				//check if there is a post with the given id
				var post = _context.BlogPosts
								.Include(bp => bp.User)
								.FirstOrDefault(bp => bp.BlogPostId == id);
				if (post == null)
				{
					return RedirectToAction("Index", "BlogPost");
				}
				else
				{
					//check if the user is the author of the post
					if (post.UserId == int.Parse(Request.Cookies["id"]))
					{
						//check if the fields are not empty
						if (blogPostTitle == null || BlogPostContent == null || BlogPostImage == null)
						{
							return RedirectToAction("Index", "BlogPost");
						}
						else
						{
							//update the post
							post.Title = blogPostTitle;
							post.Content = BlogPostContent;
							post.ImageUrl = BlogPostImage;

							_context.SaveChanges();

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
