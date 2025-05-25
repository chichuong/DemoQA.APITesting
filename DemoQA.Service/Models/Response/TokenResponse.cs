using Newtonsoft.Json;
using System;

namespace DemoQA.Service.Models.Response
{
    public class TokenResponse
    {
        [JsonProperty("token")]
        public string? Token { get; set; }

        [JsonProperty("expires")]
        public DateTime? Expires { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }

        [JsonProperty("result")]
        public string? Result { get; set; }
    }
}