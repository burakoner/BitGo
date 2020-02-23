using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Transfer
{
    public class TransferInfoHistory
    {
        [JsonProperty("date")]
        public DateTime Date { get; internal set; }
        [JsonProperty("user")]
        public string UserId { get; internal set; }
        [JsonProperty("action")]
        public string Action { get; internal set; }
        [JsonProperty("comment")]
        public string Comment { get; internal set; }
    }
}
