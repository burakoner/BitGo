using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.User
{
    public class RequestData_UserUnlockSession
    {
        [JsonProperty("duration")]
        public int Duration { get; internal set; }
        [JsonProperty("otp")]
        public string OTP { get; internal set; }
    }
}
