using RuiRumos74252.Models;

namespace RuiRumos74252.Data.Services
{
    public interface IAzureCosmosDBService
    {
        List<BlogPost> GetAllBlogPosts();
        void AddComment(Comment comment);
    }
}
