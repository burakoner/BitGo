using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.User
{
  public  class UserInfoResponse
    {
        [JsonProperty("user")]
        public UserInfo User { get; internal set; }
    }

    public class UserInfo
    {
        [JsonProperty("id")]
        public string UserId { get; internal set; }
        [JsonProperty("username")]
        public string Username { get; internal set; }
        [JsonProperty("name")]
        public UserInfoName Name { get; internal set; }
        [JsonProperty("email")]
        public UserInfoEmail Email { get; internal set; }
        [JsonProperty("phone")]
        public UserInfoPhone Phone { get; internal set; }
        [JsonProperty("country")]
        public string Country { get; internal set; }
        [JsonProperty("identity")]
        public UserInfoIdentity Identity { get; internal set; }
        [JsonProperty("otpDevices")]
        public OtpDevice[] OtpDevices { get; internal set; }
        // [JsonProperty("rateLimits")]
        // public object RateLimits { get; internal set; }
        [JsonProperty("disableResetOTP")]
        public bool DisableResetOTP { get; internal set; }
        [JsonProperty("currency")]
        public UserInfoCurrency Currency { get; internal set; }
        [JsonProperty("timezone")]
        public string Timezone { get; internal set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; internal set; }
        [JsonProperty("ecdhKeychain")]
        public string EcdhKeychain { get; internal set; }
        // [JsonProperty("referrer")]
        // public object Referrer { get; internal set; }
        // [JsonProperty("apps")]
        // public object Apps { get; internal set; }
        [JsonProperty("forceResetPassword")]
        public bool ForceResetPassword { get; internal set; }
        // [JsonProperty("allowedCoins")]
        // public string[] AllowedCoins { get; internal set; }
        [JsonProperty("agreements")]
        public UserInfoAgreements Agreements { get; internal set; }
        [JsonProperty("lastLogin")]
        public DateTime LastLogin { get; internal set; }
        // [JsonProperty("featureFlags")]
        // public string[] featureFlags { get; internal set; }
        [JsonProperty("enterprises")]
        public UserInfoEnterprise[] Enterprises { get; internal set; }
    }

    public class UserInfoName
    {
        [JsonProperty("full")]
        public string FullName { get; internal set; }
        [JsonProperty("first")]
        public string FirstName { get; internal set; }
        [JsonProperty("last")]
        public string LastName { get; internal set; }
    }
    public class UserInfoEmail
    {
        [JsonProperty("email")]
        public string Email { get; internal set; }
        [JsonProperty("verified")]
        public bool Verified { get; internal set; }
    }
    public class UserInfoPhone
    {
        [JsonProperty("phone")]
        public string Phone { get; internal set; }
        [JsonProperty("verified")]
        public bool Verified { get; internal set; }
    }
    public class UserInfoIdentity
    {
        [JsonProperty("civic")]
        public UserInfoIdentityCivic Civic { get; internal set; }
        [JsonProperty("kyc")]
        public KYC KYC { get; internal set; }
        [JsonProperty("verified")]
        public bool Verified { get; internal set; }
    }
    public class UserInfoIdentityCivic
    {
        [JsonProperty("state")]
        public string state { get; internal set; }
    }
    public class UserInfoCurrency
    {
        [JsonProperty("currency")]
        public string Currency { get; internal set; }
        [JsonProperty("bitcoinUnit")]
        public string BitcoinUnit { get; internal set; }
    }
    public class UserInfoAgreements
    {
        [JsonProperty("termsOfUse")]
        public int TermsOfUse { get; internal set; }
        [JsonProperty("patriotAct")]
        public int PatriotAct { get; internal set; }
        [JsonProperty("termsOfUseAcceptanceDate")]
        public DateTime TermsOfUseAcceptanceDate { get; internal set; }
    }
    public class UserInfoEnterprise
    {
        [JsonProperty("id")]
        public string EnterpriseId { get; internal set; }
        [JsonProperty("permissions")]
        public string[] Permissions { get; internal set; }
    }
}
