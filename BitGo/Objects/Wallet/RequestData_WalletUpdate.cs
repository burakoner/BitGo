using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Wallet
{
    public class RequestData_WalletUpdate
    {
        [JsonProperty("label")]
        public string Label { get; internal set; }
        [JsonProperty("approvalsRequired")]
        public int ApprovalsRequired { get; internal set; }
        [JsonProperty("disableTransactionNotifications")]
        public bool DisableTransactionNotifications { get; internal set; }
    }
}
