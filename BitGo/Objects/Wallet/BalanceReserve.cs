using BitGo.Objects.Address;
using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Wallet
{
    public class BalanceReserve
    {
        [JsonProperty("baseFee")]
        public string BaseFee { get; internal set; }
        [JsonProperty("baseReserve")]
        public string BaseReserve { get; internal set; }
        [JsonProperty("reserve")]
        public string Reserve { get; internal set; }
        [JsonProperty("minimumFunding")]
        public string MinimumFunding { get; internal set; }
        [JsonProperty("height")]
        public int Height { get; internal set; }
        [JsonProperty("date")]
        public DateTime Date { get; internal set; }
    }
}
