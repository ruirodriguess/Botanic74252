namespace RuiRumos74252.Models
{
    public class Notification
    {
        public string? OrderId { get; set; } // Alterar mais tarde para guid/id
        public string? CustomerEmail { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }

        public List<Product> Products { get; set; }

    }

}
