using RuiRumos74252.Models;
using System.ComponentModel;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace RuiRumos74252.Data.Services
{
    public class AzureCosmosDBService : IAzureCosmosDBService
    {
        private const string EndpointUrl = "https://cosmossql74252.documents.azure.com:443/";
        private const string PrimaryKey = "EZMnueBVnxq565ZpjHJ91I48vdPBA2iQLJc361s4alUPqnBp0J943flMUOR33XRbtByVPPacVrCZACDbx0posw==";
        private const string DatabaseName = "blogdb74252";
        private const string ContainerName = "blogcontainer74252";

        private readonly CosmosClient _cosmosClient;
        private readonly Microsoft.Azure.Cosmos.Container _container;

        public AzureCosmosDBService()
        {
            _cosmosClient = new CosmosClient(EndpointUrl, PrimaryKey);
            var database = _cosmosClient.GetDatabase(DatabaseName);
            _container = database.GetContainer(ContainerName);
        }

        public List<BlogPost> GetAllBlogPosts()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var resultSetIterator = _container.GetItemQueryIterator<BlogPost>(query);

            var blogPosts = new List<BlogPost>();
            while (resultSetIterator.HasMoreResults)
            {
                var response = resultSetIterator.ReadNextAsync().Result;
                blogPosts.AddRange(response.ToList());
            }

            return blogPosts;
        }

        public void AddComment(Comment comment)
        {
            _container.CreateItemAsync(comment);
        }

        public async Task CreateBlogPostAsync(BlogPost blogPost)
        {
            await _container.CreateItemAsync(blogPost, new PartitionKey(blogPost.Id));
        }

        public async Task<BlogPost> GetBlogPostAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<BlogPost>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        //public async Task UpdateBlogPostAsync(BlogPost blogPost)
        //{
        //    await _container.ReplaceItemAsync(blogPost, blogPost.Id, new PartitionKey(blogPost.Id));
        //}

        public async Task DeleteBlogPostAsync(string id)
        {
            await _container.DeleteItemAsync<BlogPost>(id, new PartitionKey(id));
        }
    }
}
