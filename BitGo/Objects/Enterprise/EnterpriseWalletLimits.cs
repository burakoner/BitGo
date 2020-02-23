using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Enterprise
{
    public class EnterpriseWalletLimits
    {
        [JsonProperty("walletLimits")]
        public EnterpriseWalletLimit[] WalletLimits { get; internal set; }
    }
    public class EnterpriseWalletLimit
    {
        [JsonProperty("coin")]
        public string Coin { get; internal set; }
        [JsonProperty("count")]
        public int Count { get; internal set; }
        [JsonProperty("limit")]
        public int Limit { get; internal set; }
        [JsonProperty("isCustodial")]
        public bool IsCustodial { get; internal set; }
    }
}
