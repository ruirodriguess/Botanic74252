using RuiRumos74252.Models;
using System.ComponentModel;
using Microsoft.Azure.Cosmos;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RuiRumos74252.Data.Services
{
    public class AzureCosmosDBService : IAzureCosmosDBService
    {
        private readonly string endpoint = "https://cosmossql74252.documents.azure.com:443/";
        private readonly string key = "EZMnueBVnxq565ZpjHJ91I48vdPBA2iQLJc361s4alUPqnBp0J943flMUOR33XRbtByVPPacVrCZACDbx0posw==";
        private readonly string databaseId = "blogdb74252";
        private readonly string containerId = "blogcontainer74252"; // Para os blogs
        private readonly string containerId2 = "comments74252"; // Para os comentários

        private readonly Microsoft.Azure.Cosmos.Container container;
        private readonly Microsoft.Azure.Cosmos.Container container2;

        public AzureCosmosDBService()
        {
            var client = new CosmosClient(endpoint, key);
            var database = client.GetDatabase(databaseId);
            container = database.GetContainer(containerId);
            container2 = database.GetContainer(containerId2);
        }

        public void AddComment(Comment comment, string blogPostId)
        {
            comment.Id = Guid.NewGuid().ToString();
            comment.CreatedAt = DateTime.UtcNow;
            comment.BlogPostId = blogPostId;
            container2.CreateItemAsync(comment);
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var results = container.GetItemQueryIterator<BlogPost>(query);
            var blogPosts = new List<BlogPost>();

            while (results.HasMoreResults)
            {
                var response = await results.ReadNextAsync();
                blogPosts.AddRange(response.Resource);
            }

            return blogPosts;
        }


        public async Task CreateBlogPostAsync(BlogPost blogPost)
        {
            blogPost.Id = Guid.NewGuid().ToString(); // Generate a unique string value for the id property
            blogPost.CreatedAt = DateTime.UtcNow; // Define a data e hora atual
            await container.CreateItemAsync(blogPost, new PartitionKey(blogPost.Id));
        }

        public async Task<BlogPost> GetBlogPostAsync(string id)
        {
            var response = await container.ReadItemAsync<BlogPost>(id, new PartitionKey(id));
            var blogPost = response.Resource;

            var commentQuery = new QueryDefinition("SELECT * FROM comments c WHERE c.blogPostId = @blogPostId")
                .WithParameter("@blogPostId", blogPost.Id);
            var commentResults = container2.GetItemQueryIterator<Comment>(commentQuery);
            var comments = new List<Comment>();

            while (commentResults.HasMoreResults)
            {
                var commentResponse = await commentResults.ReadNextAsync();
                comments.AddRange(commentResponse.Resource);
            }

            blogPost.Comments = comments;

            return blogPost;
        }


        public async Task UpdateBlogPostAsync(BlogPost blogPost)
        {
            await container.ReplaceItemAsync(blogPost, blogPost.Id.ToString(), new PartitionKey(blogPost.Id.ToString()));
        }

        public async Task DeleteBlogPostAsync(string id)
        {
            await container.DeleteItemAsync<BlogPost>(id, new PartitionKey(id));
        }

    }
}
