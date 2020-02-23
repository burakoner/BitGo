using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Wallet
{
    public class WalletInfoList
    {
        [JsonProperty("coin")]
        public string Coin { get; internal set; }
        [JsonProperty("nextBatchPrevId")]
        public string NextBatchPrevId { get; internal set; }
        [JsonProperty("wallets")]
        public WalletInfo[] Wallets { get; internal set; }
    }
}
