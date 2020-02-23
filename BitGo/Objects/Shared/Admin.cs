using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Shared
{
    public class Admin
    {
        [JsonProperty("policy")]
        public AdminPolicy Policy { get; internal set; }
    }
}
