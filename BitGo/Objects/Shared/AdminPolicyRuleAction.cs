using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Shared
{
    public class AdminPolicyRuleAction
    {
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("approvalsRequired")]
        public int ApprovalsRequired { get; internal set; }
        [JsonProperty("userIds")]
        public string[] UserIds { get; internal set; }
    }
}
