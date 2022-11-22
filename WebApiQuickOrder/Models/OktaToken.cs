using Newtonsoft.Json;
using System;

namespace WebApiQuickOrder.Models
{
    public class OktaToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        public DateTime ExpiresAt { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        public bool IsValidAndNotExpiring
        {
            get
            {
                return !String.IsNullOrEmpty(this.AccessToken) && this.ExpiresAt > DateTime.UtcNow.AddSeconds(30);
            }
        }

        public string Scope { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
    }
}