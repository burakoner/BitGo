using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Address
{
    public class RequestData_AddressCreate
    {
        /// <summary>
        /// Enum:0 1 10 11 20 21
        /// </summary>
        [JsonProperty("chain")]
        public int Chain { get; internal set; }

        /// <summary>
        /// Max 250 characters. A human-readable label which should be applied to the new address
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; internal set; }

        /// <summary>
        /// Default: false. Whether the deployment of the address forwarder contract should use a low priority fee key(ETH only)
        /// </summary>
        [JsonProperty("lowPriority")]
        public bool LowPriority { get; internal set; }

        /// <summary>
        /// number or string. Explicit gas price to use when deploying the forwarder contract(ETH only). If not given, defaults to the current estimated network gas price.
        /// </summary>
        [JsonProperty("gasPrice")]
        public string GasPrice { get; internal set; }
    }
}
