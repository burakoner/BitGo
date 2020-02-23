using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.User
{
    public class UserLoginInfo
    {
        [JsonProperty("user")]
        public UserInfo User { get; internal set; }
        [JsonProperty("token_type")]
        public string TokenType { get; internal set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; internal set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; internal set; }
        [JsonProperty("expires_at")]
        public int ExpiresAt { get; internal set; }
        [JsonProperty("scope")]
        public string[] Scope { get; internal set; }
    }
}
