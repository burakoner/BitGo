using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Address
{
    public class AddressCoinSpecific
    {
        [JsonProperty("memoId")]
        public string MemoId { get; internal set; }
        [JsonProperty("rootAddress")]
        public string RootAddress { get; internal set; }
        [JsonProperty("redeemScript")]
        public string RedeemScript { get; internal set; }
    }
}
