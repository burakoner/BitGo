using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.User
{
    public class RequestData_UserLogin
    {
        [JsonProperty("email")]
        public string Email { get; internal set; }
        [JsonProperty("password")]
        public string Password { get; internal set; }
        [JsonProperty("otp")]
        public string OTP { get; internal set; }
    }
}
