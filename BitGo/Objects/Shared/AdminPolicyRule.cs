using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Shared
{
    public class AdminPolicyRule
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }
        [JsonProperty("lockDate")]
        public DateTime LockDate { get; internal set; }
        [JsonProperty("mutabilityConstraint")]
        public string MutabilityConstraint { get; internal set; }
        [JsonProperty("coin")]
        public string Coin { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("condition")]
        public AdminPolicyRuleCondition Condition { get; internal set; }
        [JsonProperty("action")]
        public AdminPolicyRuleAction Action { get; internal set; }
    }
}
