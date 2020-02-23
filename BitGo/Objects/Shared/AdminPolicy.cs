using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Shared
{
    public class AdminPolicy
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }
        [JsonProperty("date")]
        public DateTime Date { get; internal set; }
        [JsonProperty("label")]
        public string Label { get; internal set; }
        [JsonProperty("latest")]
        public bool Latest { get; internal set; }
        [JsonProperty("version")]
        public int Version { get; internal set; }
        [JsonProperty("rules")]
        public AdminPolicyRule[] Rules { get; internal set; }
    }
}
