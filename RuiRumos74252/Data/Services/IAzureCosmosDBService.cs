using RuiRumos74252.Models;

namespace RuiRumos74252.Data.Services
{
    public interface IAzureCosmosDBService
    {
        Task <IEnumerable<BlogPost>> GetAllBlogPostsAsync();
        Task CreateBlogPostAsync(BlogPost blogPost);
        Task<BlogPost> GetBlogPostAsync(string id);
        Task UpdateBlogPostAsync(BlogPost blogPost);
        Task DeleteBlogPostAsync(string id);

        void AddComment(Comment comment, string blogPostId);
    }

}


