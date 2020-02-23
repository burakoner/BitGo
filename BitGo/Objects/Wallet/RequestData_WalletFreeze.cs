using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Enterprise
{
    public class RequestData_WalletFreeze
    {
        [JsonProperty("duration")]
        public int Duration  { get; internal set; }
    }
}
