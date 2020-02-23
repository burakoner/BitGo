using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Key
{
    public class KeyInfoList
    {
        [JsonProperty("keys")]
        public KeyInfo[] Keys { get; internal set; }
        [JsonProperty("limit")]
        public int Limit { get; internal set; }
    }
}
