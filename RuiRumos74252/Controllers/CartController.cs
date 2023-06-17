using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RuiRumos74252.Data;
using RuiRumos74252.Models;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RuiRumos74252.Controllers
{
    [Authorize]
    public class CartController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CartController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            // Recupera o CartItems da database e apresenta na View
            var cartItems = _context.CartItems.Include(ci => ci.Product).ToList();
            return View(cartItems);
        }

        public IActionResult AddToCart(int productId, int quantity, double price, string picture)
        {
            // Recupera os produtos da database baseado no product ID
            var product = _context.Product.Find(productId);

            // Cria um novo cart item com o produto e quantidade
            var cartItem = new CartItem
            {
                ProductId = productId,
                Quantity = quantity,
                Product = product,  
                Price = price,
                Picture = picture
                
            };

            // Adiciona o cart item na database
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();

            // Redireciona para a View Index
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int cartItemId)
        {
            // Recupera o cart item da databa baseado no cart item Id
            var cartItem = _context.CartItems.Find(cartItemId);

            // Remove o cart item da database
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            // Redireciona para a View Index
            return RedirectToAction("Index");
        }

        // --------------- CHECKOUT AREA --------------- //

        private List<CartItem> GetCartItems()
        {
                var cartItems = _context.CartItems.Include(c => c.Product).ToList();
                return cartItems;

        }

        public IActionResult Checkout()
        {

            var cartItems = GetCartItems();

            // Obter o email do usuário 
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            // Gere um novo ID aleatório entre 1 e 100000
            var random = new Random();
            var orderId = random.Next(1, 100001);

            var notification = new Notification
            {
                OrderId = orderId.ToString(), 
                CustomerEmail = userEmail, 
                Products = new List<Product>(),
                Subject = "New order",
                Message = "We receive a new order. Details:"
            };

            // Preenche a lista de produtos com os produtos adquiridos
            foreach (var cartItem in cartItems)
            {
                var product = new Product
                {
                    Name = cartItem.Product.Name,
                    Price = cartItem.Product.Price,
                    
                };

                notification.Products.Add(product);
            }

            // Metodo para converter objectos em JSON
            string jsonMessage = JsonConvert.SerializeObject(notification);

            // Informações tiradas do azure
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=saruirumos74252;AccountKey=3B8VNSwQLx48BUS+I1TRZASRGRbRo/uYyB1pk2rZu87enJmtrAvAA4iCcdoXlVPjU3Z/R8z3KtVj+AStRq3k8g==;EndpointSuffix=core.windows.net";
            string queueName = "queueruirumos";

            // Create a QueueClient instance
            QueueClient queueClient = new QueueClient(connectionString, queueName);
            queueClient.CreateIfNotExists();

            // Send the JSON message to the queue
            queueClient.SendMessage(jsonMessage);

            // Clear the cart items from the database
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            // Mais tarde adicionar esta view - FEITO!
            return View("Confirmation");
        }

        public IActionResult Confirmation()
        {
            var cartItems = _context.CartItems.Include(ci => ci.Product).ToList();

            var notification = new Notification();

            return View(notification);
        }

        
    }
}
