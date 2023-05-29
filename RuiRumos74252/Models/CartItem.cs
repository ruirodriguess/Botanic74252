using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuiRumos74252.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Picture { get; set; }

        public double Price { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        // public double TotalAmount => Quantity * Price;

    }
}
