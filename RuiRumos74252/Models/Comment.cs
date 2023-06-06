using Newtonsoft.Json;

namespace RuiRumos74252.Models
{
    public class Comment
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        [JsonProperty(PropertyName = "blogPostId")]
        public string? BlogPostId { get; set; }
        [JsonProperty(PropertyName = "Content")]
        public string? Content { get; set; }
        [JsonProperty(PropertyName = "Author")]
        public string? Author { get; set; }
        [JsonProperty(PropertyName = "CreatedAt")]
        public DateTime? CreatedAt { get; set; }
    }
}
