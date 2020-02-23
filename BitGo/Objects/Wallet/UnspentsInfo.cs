using BitGo.Objects.Address;
using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Wallet
{
    public class UnspentsInfo
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }
        [JsonProperty("address")]
        public string Address { get; internal set; }
        [JsonProperty("value")]
        public long value { get; internal set; }
        [JsonProperty("valueString")]
        public string valueString { get; internal set; }
        [JsonProperty("blockHeight")]
        public int blockHeight { get; internal set; }
        [JsonProperty("date")]
        public DateTime Date { get; internal set; }
        [JsonProperty("coinbase")]
        public bool Coinbase { get; internal set; }
        [JsonProperty("wallet")]
        public string Wallet { get; internal set; }
        [JsonProperty("FromWallet")]
        public string FromWallet { get; internal set; }
        [JsonProperty("chain")]
        public int Chain { get; internal set; }
        [JsonProperty("index")]
        public int Index { get; internal set; }
        [JsonProperty("redeemScript")]
        public string RedeemScript { get; internal set; }
        [JsonProperty("witnessScript")]
        public string WitnessScript { get; internal set; }
        [JsonProperty("isSegwit")]
        public bool IsSegwit { get; internal set; }
    }
}
