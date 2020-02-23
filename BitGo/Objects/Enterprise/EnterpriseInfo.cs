using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Enterprise
{

    public class EnterpriseInfo
    {
        [JsonProperty("id")]
        public string EnterpriseId { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("primaryContact")]
        public string PrimaryContact { get; internal set; }
        [JsonProperty("latestSAVersionSigned")]
        public int LatestSAVersionSigned { get; internal set; }
        [JsonProperty("mutablePolicyWindow")]
        public int MutablePolicyWindow { get; internal set; }
        [JsonProperty("travelRule")]
        public bool TravelRule { get; internal set; }
        // [JsonProperty("additionalEnterpriseInfo")]
        //public object AdditionalEnterpriseInfo { get; internal set; }
        [JsonProperty("bitgoEthKey")]
        public string BitgoEthKey { get; internal set; }
        [JsonProperty("ethFeeAddress")]
        public string EthFeeAddress { get; internal set; }
        [JsonProperty("freeze")]
        public Freeze Freeze { get; internal set; }
        [JsonProperty("admin")]
        public Admin Admin { get; internal set; }
        [JsonProperty("walletLimit")]
        public WalletLimit WalletLimit { get; internal set; }
        [JsonProperty("bitgoOrg")]
        public string BitgoOrg { get; internal set; }
        [JsonProperty("canCreateColdWallet")]
        public bool CanCreateColdWallet { get; internal set; }
        [JsonProperty("canCreateHotWallet")]
        public bool CanCreateHotWallet { get; internal set; }
        [JsonProperty("canCreateCustodialWallets")]
        public bool CanCreateCustodialWallets { get; internal set; }
        [JsonProperty("canCreateOffchainWallet")]
        public bool CanCreateOffchainWallet { get; internal set; }
        [JsonProperty("kycState")]
        public string KycState { get; internal set; }
        [JsonProperty("upfrontPaymentStatus")]
        public string UpfrontPaymentStatus { get; internal set; }


        [JsonProperty("approvalsRequired")]
        public int ApprovalsRequired { get; internal set; }
        [JsonProperty("emergencyPhone")]
        public string EmergencyPhone { get; internal set; }
    }
}
