using Newtonsoft.Json;

namespace RuiRumos74252.Models
{
    public class BlogPost
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? ImageUrl { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
