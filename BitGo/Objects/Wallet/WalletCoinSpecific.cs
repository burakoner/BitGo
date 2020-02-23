using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Wallet
{
    public class WalletCoinSpecific
    {
        [JsonProperty("creationFailure")]
        public string[] CreationFailure { get; internal set; }
        [JsonProperty("pendingChainInitialization")]
        public bool PendingChainInitialization { get; internal set; }
        [JsonProperty("rootAddress")]
        public string RootAddress { get; internal set; }
        [JsonProperty("stellarUsername")]
        public string StellarUsername { get; internal set; }
        [JsonProperty("stellarAddress")]
        public string StellarAddress { get; internal set; }
        [JsonProperty("homeDomain")]
        public string HomeDomain { get; internal set; }
    }
}
