using Newtonsoft.Json;
using System.Collections.Generic;

namespace DemoQA.Service.Models.Request
{
    public class AddBookRequest
    {
        [JsonProperty("userId")]
        public string? UserId { get; set; }

        [JsonProperty("collectionOfIsbns")]
        public List<CollectionOfIsbn>? CollectionOfIsbns { get; set; }
    }
}