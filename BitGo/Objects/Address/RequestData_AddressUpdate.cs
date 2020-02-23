using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Address
{
    public class RequestData_AddressUpdate
    {
        /// <summary>
        /// Max 250 characters. A human-readable label which should be applied to the new address
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; internal set; }
    }
}
