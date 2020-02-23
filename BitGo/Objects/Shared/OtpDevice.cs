using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Shared
{
   public class OtpDevice
    {
        [JsonProperty("id")]
        public string OtpDeviceId { get; internal set; }
        [JsonProperty("createDate")]
        public DateTime CreateDate { get; internal set; }
        [JsonProperty("lastValidatedDate")]
        public DateTime LastValidatedDate { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("label")]
        public string Label { get; internal set; }
        [JsonProperty("verified")]
        public bool Verified { get; internal set; }
    }
}
