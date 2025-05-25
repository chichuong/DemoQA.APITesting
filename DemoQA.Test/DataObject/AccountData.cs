using Newtonsoft.Json;

namespace DemoQA.Test.DataObject
{
    public class AccountProperties
    {
        [JsonProperty("userName")]
        public string? Username { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }

        [JsonProperty("userId")]
        public string? UserId { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}
