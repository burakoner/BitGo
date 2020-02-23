using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Enterprise
{

    public class EnterpriseInfoStringTags : EnterpriseInfo
    {
        [JsonProperty("tags")]
        public string[] Tags { get; internal set; }
    }
}
