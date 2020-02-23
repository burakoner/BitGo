using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Shared
{
    public class KYC
    {
        [JsonProperty("failureCount")]
        public int FailureCount { get; internal set; }
        [JsonProperty("overallState")]
        public string OverallState { get; internal set; }
        [JsonProperty("required")]
        public bool Required { get; internal set; }
        [JsonProperty("available")]
        public bool Available { get; internal set; }
        [JsonProperty("data")]
        public KycState Data { get; internal set; }
        [JsonProperty("documents")]
        public KycState Documents { get; internal set; }
        [JsonProperty("residency")]
        public KycState Residency { get; internal set; }
        [JsonProperty("verified")]
        public bool Verified { get; internal set; }
    }

    public class KycState
    {
        [JsonProperty("state")]
        public string State { get; internal set; }
    }
}
