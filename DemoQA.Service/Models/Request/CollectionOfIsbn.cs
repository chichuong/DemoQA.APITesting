using Newtonsoft.Json;

namespace DemoQA.Service.Models.Request
{
    public class CollectionOfIsbn
    {
        [JsonProperty("isbn")]
        public string? Isbn { get; set; }
    }
}