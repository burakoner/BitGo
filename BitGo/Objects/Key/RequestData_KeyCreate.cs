using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Key
{
    public class RequestData_KeyCreate
    {
        [JsonProperty("encryptedPrv")]
        public string EncryptedPrv { get; internal set; }
        [JsonProperty("enterprise")]
        public string EnterpriseId { get; internal set; }
        [JsonProperty("newFeeAddress")]
        public bool UseNewFeeAddress { get; internal set; }
        [JsonProperty("pub")]
        public string Pub { get; internal set; }
        [JsonProperty("source")]
        public string Source { get; internal set; }
    }
}
