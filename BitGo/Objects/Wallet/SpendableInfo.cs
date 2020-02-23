using BitGo.Objects.Address;
using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Wallet
{
    public class SpendableInfo
    {
        [JsonProperty("coin")]
        public string Coin { get; internal set; }
        [JsonProperty("maximumSpendable")]
        public string MaximumSpendable { get; internal set; }
    }
}
