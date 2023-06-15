using Xunit;
using RuiRumos74252.Controllers;
using RuiRumos74252.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using RuiRumos74252.Data.Services;

namespace TestProject
{
    public class BlogControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewWithBlogPosts()
        {
            // Arrange
            var mockService = new Mock<IAzureCosmosDBService>();
            var controller = new BlogController(mockService.Object);
            var expectedBlogPosts = new List<BlogPost>(); // Adicione blog posts esperados para o teste

            mockService.Setup(service => service.GetAllBlogPostsAsync())
                .ReturnsAsync(expectedBlogPosts);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<BlogPost>>(viewResult.ViewData.Model);
            Assert.Equal(expectedBlogPosts, model);
        }

        // Teste para o método Create (POST)
        [Fact]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var mockService = new Mock<IAzureCosmosDBService>();
            var controller = new BlogController(mockService.Object);
            var blogPost = new BlogPost(); // Crie um objeto de blog post válido

            // Act
            var result = await controller.Create(blogPost);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }
    }
}