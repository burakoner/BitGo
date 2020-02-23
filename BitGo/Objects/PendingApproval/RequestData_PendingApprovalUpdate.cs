using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.PendingApproval
{
    public class RequestData_PendingApprovalUpdate
    {
        [JsonProperty("otp")]
        public string OTP { get; internal set; }
        [JsonProperty("state")]
        public string State { get; internal set; }
    }
}
