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

        // Other actions for creating, editing, and deleting blog posts
    }
}
