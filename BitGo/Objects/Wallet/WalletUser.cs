using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Wallet
{
    public class WalletUser
    {
        [JsonProperty("user")]
        public string UserId { get; internal set; }
        [JsonProperty("permissions")]
        public string[] Permissions { get; internal set; }
    }
}
