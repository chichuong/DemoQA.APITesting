using DemoQA.Service.Models.Request;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DemoQA.Test.DataObject
{
    public class BookData
    {
        [JsonProperty("userId")]
        public string? UserId { get; set; }

        [JsonProperty("collectionOfIsbns")]
        public List<CollectionOfIsbn>? CollectionOfIsbns { get; set; }
    }
}
