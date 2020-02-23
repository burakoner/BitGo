using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Transfer
{
    public class TransferInfoInputOutput
    {
        [JsonProperty("id")]
        public string InputId { get; internal set; }
        [JsonProperty("address")]
        public string Address { get; internal set; }
        [JsonProperty("value")]
        public long Value { get; internal set; }
        [JsonProperty("valueString")]
        public string valueString { get; internal set; }
        [JsonProperty("blockHeight")]
        public long BlockHeight { get; internal set; }
        [JsonProperty("date")]
        public DateTime Date { get; internal set; }
        [JsonProperty("coinbase")]
        public bool Coinbase { get; internal set; }
        [JsonProperty("wallet")]
        public string WalletId { get; internal set; }
        [JsonProperty("fromWallet")]
        public string FromWalletId { get; internal set; }
        [JsonProperty("chain")]
        public long Chain { get; internal set; }
        [JsonProperty("index")]
        public long Index { get; internal set; }
        [JsonProperty("redeemScript")]
        public string RedeemScript { get; internal set; }
        [JsonProperty("witnessScript")]
        public string WitnessScript { get; internal set; }
        [JsonProperty("isSegwit")]
        public bool IsSegwit { get; internal set; }
    }
}
