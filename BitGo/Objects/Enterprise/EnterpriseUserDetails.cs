using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Enterprise
{
    public class EnterpriseUserDetails
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }
        [JsonProperty("username")]
        public string Username { get; internal set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; internal set; }
        [JsonProperty("verified")]
        public bool Verified { get; internal set; }
        [JsonProperty("email")]
        public EnterpriseUserEmail Email { get; internal set; }
        [JsonProperty("identity")]
        public EnterpriseUserIdentity identity { get; internal set; }
    }

    public class EnterpriseUserEmail
    {
        [JsonProperty("email")]
        public string Email { get; internal set; }
        [JsonProperty("verified")]
        public bool Verified { get; internal set; }
        [JsonProperty("verifyFailures")]
        public int verifyFailures { get; internal set; }
        [JsonProperty("verifySentAt")]
        public DateTime VerifySentAt { get; internal set; }
        [JsonProperty("verifyCode")]
        public string verifyCode { get; internal set; }
    }

    public class EnterpriseUserIdentity
    {
        [JsonProperty("kyc")]
        public KYC KYC { get; internal set; }
    }
}
