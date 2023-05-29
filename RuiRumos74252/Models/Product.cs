using System.ComponentModel.DataAnnotations;

namespace RuiRumos74252.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Picture { get; set; }
        public string? Description { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
