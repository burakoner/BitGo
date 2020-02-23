using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Enterprise
{
    public class EnterpriseInfoList
    {
        [JsonProperty("enterprises")]
        public EnterpriseInfoStringTags[] Enterprises { get; internal set; }
    }
}
