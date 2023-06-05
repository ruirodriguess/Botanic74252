using Newtonsoft.Json;

namespace RuiRumos74252.Models
{
    public class Comment
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        [JsonProperty(PropertyName = "blogPostId")]
        public string? BlogPostId { get; set; }
        public string? Content { get; set; }
        public string? Author { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
