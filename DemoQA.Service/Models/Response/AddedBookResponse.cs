using Newtonsoft.Json;
using System.Collections.Generic;

namespace DemoQA.Service.Models.Response
{
    public class AddedBookIsbn
    {
        [JsonProperty("isbn")]
        public string? Isbn { get; set; }
    }

    public class AddedBookResponse
    {
        [JsonProperty("books")]
        public List<AddedBookIsbn>? Books { get; set; }
    }
}