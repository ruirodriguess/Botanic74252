using Microsoft.AspNetCore.Mvc;
using RuiRumos74252.Data.Services;
using RuiRumos74252.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Authorization;

namespace RuiRumos74252.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        
        private readonly IAzureCosmosDBService _cosmosDBService;

        public BlogController(IAzureCosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        public async Task<ActionResult> Index()
        {
            var blogPosts = await _cosmosDBService.GetAllBlogPostsAsync();
            return View(blogPosts);
        }

        [HttpPost]
        public ActionResult AddComment(Comment comment, string blogPostId)
        {
            if (ModelState.IsValid)
            {
                _cosmosDBService.AddComment(comment, blogPostId);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            // Return a view to create a new blog post
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDBService.CreateBlogPostAsync(blogPost);
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string id)
        {
            var blogPost = await _cosmosDBService.GetBlogPostAsync(id);

            if (blogPost != null)
            {
                return View(blogPost);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                _cosmosDBService.UpdateBlogPostAsync(blogPost);
                return RedirectToAction("Index");
            }

            return View(blogPost);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            _cosmosDBService.DeleteBlogPostAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(string id)
        {
            var blogPost = await _cosmosDBService.GetBlogPostAsync(id);

            if (blogPost != null)
            {
                return View(new List<BlogPost> { blogPost }); // Passa uma lista com um único objeto BlogPost
            }

            return RedirectToAction("Index");
        }
    }

}
