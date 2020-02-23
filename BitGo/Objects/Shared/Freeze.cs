using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Shared
{
    public class Freeze
    {
        [JsonProperty("time")]
        public DateTime Time { get; internal set; }
        [JsonProperty("expires")]
        public DateTime Expires { get; internal set; }
    }
}
