using Newtonsoft.Json;
using System.Collections.Generic;

namespace DemoQA.Service.Models.Response
{
    public class BookInProfile
    {
        [JsonProperty("isbn")]
        public string? Isbn { get; set; }
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("subTitle")]
        public string? SubTitle { get; set; }
        [JsonProperty("author")]
        public string? Author { get; set; }
        [JsonProperty("publish_date")]
        public string? PublishDate { get; set; }
        [JsonProperty("publisher")]
        public string? Publisher { get; set; }
        [JsonProperty("pages")]
        public int Pages { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("website")]
        public string? Website { get; set; }
    }

    public class UserAccountResponse
    {
        [JsonProperty("userID")]
        public string? UserId { get; set; }
        [JsonProperty("username")]
        public string? Username { get; set; }
        [JsonProperty("books")]
        public List<BookInProfile>? Books { get; set; }
    }
}