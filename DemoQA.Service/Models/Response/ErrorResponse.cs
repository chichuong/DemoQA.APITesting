using Newtonsoft.Json;

namespace DemoQA.Service.Models.Response
{
    public class ErrorResponse
    {
        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}