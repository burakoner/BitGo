using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Wallet
{
    public class RequestData_AddWallet
    {
        [JsonProperty("coinSpecific")]
        public Dictionary<string, Dictionary<string, string>> CoinSpecific { get; internal set; }
        [JsonProperty("enterprise")]
        public string EnterpriseId { get; internal set; }
        [JsonProperty("isCold")]
        public bool IsCold { get; internal set; }
        [JsonProperty("keys")]
        public string[] Keys { get; internal set; }
        [JsonProperty("keySignatures")]
        public Dictionary<string,string> KeySignatures { get; internal set; }
        [JsonProperty("label")]
        public string Label { get; internal set; }
        [JsonProperty("m")]
        public int RequiredSignaturesNumber { get; internal set; }
        [JsonProperty("n")]
        public int ProvidedKeysNumber { get; internal set; }
        [JsonProperty("tags")]
        public string[] Tags { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
    }
}
