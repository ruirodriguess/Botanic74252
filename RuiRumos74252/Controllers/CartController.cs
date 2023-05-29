using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RuiRumos74252.Data;
using RuiRumos74252.Models;

namespace RuiRumos74252.Controllers
{
    public class CartController : Controller
    {

        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
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

    }
}
