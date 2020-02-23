using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Enterprise
{
    public class RequestData_EnterpriseRemoveUser
    {
        [JsonProperty("username")]
        public string Username { get; internal set; }
    }
}
