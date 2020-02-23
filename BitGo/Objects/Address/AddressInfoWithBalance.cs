using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Address
{

    public class AddressInfoWithBalance: AddressInfo
    {
        [JsonProperty("balance")]
        public  AddressBalance Balance { get; internal set; }
    }
}
