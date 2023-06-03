﻿using RuiRumos74252.Models;
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
        private readonly string containerId = "blogcontainer74252";

        private readonly Microsoft.Azure.Cosmos.Container container;

        public AzureCosmosDBService()
        {
            var client = new CosmosClient(endpoint, key);
            var database = client.GetDatabase(databaseId);
            container = database.GetContainer(containerId);
        }

        public void AddComment(Comment comment)
        {
            container.CreateItemAsync(comment);
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
            await container.CreateItemAsync(blogPost, new PartitionKey(blogPost.Id.ToString()));
        }

        public async Task<BlogPost> GetBlogPostAsync(string id)
        {
            var response = await container.ReadItemAsync<BlogPost>(id, new PartitionKey(id));
            return response.Resource;
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