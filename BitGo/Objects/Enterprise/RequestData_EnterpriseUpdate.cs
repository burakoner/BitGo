using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Enterprise
{
    public class RequestData_EnterpriseUpdate
    {
        /// <summary>
        /// How many Enterprise Admins are required for action to fire
        /// </summary>
        [JsonProperty("approvalsRequired")]
        public int ApprovalsRequired { get; internal set; }
    }
}
