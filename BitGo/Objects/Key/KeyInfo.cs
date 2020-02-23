using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Key
{
    public class KeyInfo
    {
        [JsonProperty("id")]
        public string KeyId { get; internal set; }
        [JsonProperty("encryptedPrv")]
        public string EncryptedPrv { get; internal set; }
        [JsonProperty("ethAddress")]
        public string EthAddress { get; internal set; }
        [JsonProperty("isBitGo")]
        public bool IsBitGoKey { get; internal set; }
        [JsonProperty("pub")]
        public string Pub { get; internal set; }
    }
}
