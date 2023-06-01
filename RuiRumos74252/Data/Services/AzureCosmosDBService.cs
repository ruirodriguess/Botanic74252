using RuiRumos74252.Models;
using System.ComponentModel;
using Microsoft.Azure.Cosmos;

namespace RuiRumos74252.Data.Services
{
    public class AzureCosmosDBService : IAzureCosmosDBService
    {
        private const string EndpointUrl = "YourCosmosDBEndpointUrl";
        private const string PrimaryKey = "YourCosmosDBPrimaryKey";
        private const string DatabaseName = "YourDatabaseName";
        private const string ContainerName = "YourContainerName";

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
    }
}
