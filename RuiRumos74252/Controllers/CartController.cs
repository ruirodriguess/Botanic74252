using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RuiRumos74252.Data;
using RuiRumos74252.Models;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Newtonsoft.Json;

namespace RuiRumos74252.Controllers
{
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
            // Retrieve the cart items from the database and display them in the view
            var cartItems = _context.CartItems.Include(ci => ci.Product).ToList();
            return View(cartItems);
        }

        public IActionResult AddToCart(int productId, int quantity, double price, string picture)
        {
            // Retrieve the product from the database based on the product ID
            var product = _context.Product.Find(productId);

            // Create a new cart item with the product and quantity
            var cartItem = new CartItem
            {
                ProductId = productId,
                Quantity = quantity,
                Product = product,  
                Price = price,
                Picture = picture
                
            };

            // Add the cart item to the database
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();

            // Redirect to the cart page or display a success message
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int cartItemId)
        {
            // Retrieve the cart item from the database based on the cart item ID
            var cartItem = _context.CartItems.Find(cartItemId);

            // Remove the cart item from the database
            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            // Redirect to the cart page or display a success message
            return RedirectToAction("Index");
        }

        // ------ CHECKOUT AREA -------

        private List<CartItem> GetCartItems()
        {
                var cartItems = _context.CartItems.Include(c => c.Product).ToList();
                return cartItems;

        }

        public IActionResult Checkout()
        {

            var cartItems = GetCartItems();

            var notification = new Notification
            {
                OrderId = "teste123", // Replace with the actual order ID
                CustomerEmail = "ruirodrigues04@outlook.pt", // Replace with the customer's email
                Products = new List<Product>()
            };

            // Populate the Products list with the purchased products
            foreach (var cartItem in cartItems)
            {
                var product = new Product
                {
                    Name = cartItem.Product.Name,
                    Price = cartItem.Product.Price,
                };

                notification.Products.Add(product);
            }

            //string recipientEmail = "ruirodrigues04@outlook.pt"; 
            //string subject = "Order Confirmation";
            //string message = "Thank you for your order!"; // Customize the message as needed

            //// Serialize the notification object into a JSON string
            //var notification = new Notification
            //{
            //    RecipientEmail = recipientEmail,
            //    Subject = subject,
            //    Message = message
            //};
            string jsonMessage = JsonConvert.SerializeObject(notification);


            string connectionString = "DefaultEndpointsProtocol=https;AccountName=saruirumos74252;AccountKey=3B8VNSwQLx48BUS+I1TRZASRGRbRo/uYyB1pk2rZu87enJmtrAvAA4iCcdoXlVPjU3Z/R8z3KtVj+AStRq3k8g==;EndpointSuffix=core.windows.net";
            string queueName = "queueruirumos";

            QueueClient queueClient = new QueueClient(connectionString, queueName);
            queueClient.CreateIfNotExists();

            queueClient.SendMessage(jsonMessage);

            // Mais tarde adicionar esta view
            return View("Confirmation");
            //return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            var cartItems = _context.CartItems.Include(ci => ci.Product).ToList();

            var notification = new Notification();

            return View(notification);
        }

    }
}
