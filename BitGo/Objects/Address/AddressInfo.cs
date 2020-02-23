using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Address
{

    public class AddressInfo
    {
        [JsonProperty("id")]
        public string AddressId { get; internal set; }
        [JsonProperty("address")]
        public string Address { get; internal set; }
        [JsonProperty("chain")]
        public int Chain { get; internal set; }
        [JsonProperty("index")]
        public int Index { get; internal set; }
        [JsonProperty("coin")]
        public string Coin { get; internal set; }
        [JsonProperty("lastNonce")]
        public int LastNonce { get; internal set; }
        [JsonProperty("wallet")]
        public string WalletId { get; internal set; }
        [JsonProperty("label")]
        public string Label { get; internal set; }
        /// <summary>
        /// Enum:"p2sh" "p2sh-p2wsh" "p2wsh"
        /// </summary>
        [JsonProperty("addressType")]
        public string AddressType { get; internal set; }
        [JsonProperty("coinSpecific")]
        public  AddressCoinSpecific CoinSpecific { get; internal set; }
    }
}
