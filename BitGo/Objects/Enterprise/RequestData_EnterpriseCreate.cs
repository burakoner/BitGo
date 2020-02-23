using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Enterprise
{
    public class RequestData_EnterpriseCreate
    {
        /// <summary>
        /// required
        /// </summary>
        [JsonProperty("name")]
        public string Name  { get; internal set; }

        /// <summary>
        /// Phone number for emergencies
        /// </summary>
        [JsonProperty("emergencyPhone")]
        public string EmergencyPhone { get; internal set; }

        /// <summary>
        /// The URL the enterprises web site
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; internal set; }
    }
}
