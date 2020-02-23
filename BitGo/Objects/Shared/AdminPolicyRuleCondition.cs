using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Shared
{
    public class AdminPolicyRuleCondition
    {
        [JsonProperty("amountString")]
        public string AmountString { get; internal set; }
        [JsonProperty("timeWindow")]
        public int TimeWindow { get; internal set; }
    }
}
