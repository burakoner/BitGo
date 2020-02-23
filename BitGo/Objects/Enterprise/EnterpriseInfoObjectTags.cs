using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Enterprise
{

    public class EnterpriseInfoObjectTags : EnterpriseInfo
    {
        [JsonProperty("tags")]
        public EnterpriseTag[] Tags { get; internal set; }
    }

    public class EnterpriseTag
    {
        [JsonProperty("id")]
        public string EnterpriseId { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
    }
}
