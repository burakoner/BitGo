using BitGo.Objects.Address;
using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Wallet
{
    public class UnspentsInfoList
    {
        [JsonProperty("coin")]
        public string coin { get; internal set; }
        [JsonProperty("unspents")]
        public UnspentsInfo[] Unspents { get; internal set; }
        [JsonProperty("nextBatchPrevId")]
        public string NextBatchPrevId { get; internal set; }
        [JsonProperty("resultMessage")]
        public string ResultMessage { get; internal set; }
    }
}
