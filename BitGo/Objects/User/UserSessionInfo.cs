using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.User
{
    public class UserSession
    {
        [JsonProperty("session")]
        public UserSessionInfo Session { get; internal set; }
    }
    public class UserSessionInfo
    {
        [JsonProperty("id")]
        public string SessionId { get; internal set; }
        [JsonProperty("client")]
        public string Client { get; internal set; }
        [JsonProperty("user")]
        public string UserId { get; internal set; }
        [JsonProperty("scope")]
        public string[] Scope { get; internal set; }
        [JsonProperty("created")]
        public DateTime Created { get; internal set; }
        [JsonProperty("expires")]
        public DateTime Expires { get; internal set; }
        [JsonProperty("ip")]
        public string Ip { get; internal set; }
        [JsonProperty("ipRestrict")]
        public string[] IpRestrict { get; internal set; }
        [JsonProperty("origin")]
        public string Origin { get; internal set; }
        [JsonProperty("isExtensible")]
        public bool IsExtensible { get; internal set; }
        [JsonProperty("unlock")]
        public UserSessionInfoUnlock Unlock { get; internal set; }
    }
    public class UserSessionInfoUnlock
    {
        [JsonProperty("time")]
        public DateTime Time { get; internal set; }
        [JsonProperty("expires")]
        public DateTime Expires { get; internal set; }
        [JsonProperty("txValueLimit")]
        public long TxValueLimit { get; internal set; }
        [JsonProperty("txValue")]
        public long TxValue { get; internal set; }
        // [JsonProperty("spendingLimits")]
        // public object SpendingLimits { get; internal set; }
    }
}
