using Microsoft.AspNetCore.Mvc;
using RuiRumos74252.Data.Services;
using RuiRumos74252.Models;

namespace RuiRumos74252.Controllers
{
    public class BlogController : Controller
    {
        private readonly AzureBlobStorageService _blobStorageService;
        private readonly AzureCosmosDBService _cosmosDBService;

        public BlogController()
        {
            _blobStorageService = new AzureBlobStorageService();
            _cosmosDBService = new AzureCosmosDBService();
        }

        public ActionResult Index()
        {
            var blogPosts = _cosmosDBService.GetAllBlogPosts();
            return View(blogPosts);
        }

        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _cosmosDBService.AddComment(comment);
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
                // Save the new blog post using your Azure Cosmos DB service
                await _cosmosDBService.CreateBlogPostAsync(blogPost);
                return RedirectToAction("Index");
            }

            // If the model state is invalid, return the view with validation errors
            return View(blogPost);
        }

        public ActionResult Edit(string id)
        {
            // Retrieve the blog post using the ID
            var blogPost = _cosmosDBService.GetBlogPostAsync(id);

            if (blogPost != null)
            {
                // Return a view to edit the blog post
                return View(blogPost);
            }

            // If the blog post is not found, return an error or redirect to another page
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult Edit(BlogPost blogPost)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Update the existing blog post using your Azure Cosmos DB service
        //        _cosmosDBService.UpdateBlogPostAsync(blogPost);
        //        return RedirectToAction("Index");
        //    }

        //    // If the model state is invalid, return the view with validation errors
        //    return View(blogPost);
        //}

        [HttpPost]
        public ActionResult Delete(string id)
        {
            // Delete the blog post using the ID
            _cosmosDBService.DeleteBlogPostAsync(id);
            return RedirectToAction("Index");
        }
    }
}
