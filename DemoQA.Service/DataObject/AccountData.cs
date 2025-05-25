using Newtonsoft.Json;

namespace DemoQA.Service.DataObject
{
    public class AccountData
    {
        [JsonProperty("userName")]
        public string? Username { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }

        [JsonProperty("userID")]
        public string? UserId { get; set; }
    }
}