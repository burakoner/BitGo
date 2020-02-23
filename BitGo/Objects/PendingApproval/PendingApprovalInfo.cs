using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.PendingApproval
{

    public class PendingApprovalInfo
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }
        [JsonProperty("enterprise")]
        public string Enterprise { get; internal set; }
        [JsonProperty("walletId")]
        public string WalletId { get; internal set; }
        [JsonProperty("creator")]
        public string Creator { get; internal set; }
        [JsonProperty("createDate")]
        public DateTime CreateDate { get; internal set; }
        [JsonProperty("info")]
        public UpdateEnterpriseResponseInfo info { get; internal set; }
        /// <summary>
        /// Enum:"pending" "approved" "rejected"
        /// </summary>
        [JsonProperty("state")]
        public string State { get; internal set; }
        [JsonProperty("walletUserIds")]
        public string[] WalletUserIds { get; internal set; }
        /// <summary>
        /// number >= 1
        /// </summary>
        [JsonProperty("approvalsRequired")]
        public int approvalsRequired { get; internal set; }
    }
    public class UpdateEnterpriseResponseInfo
    {
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("updateEnterpriseRequest")]
        public UpdateEnterpriseRequest UpdateEnterpriseRequest { get; internal set; }
        [JsonProperty("updateApprovalsRequiredRequest")]
        public UpdateApprovalsRequiredRequest UpdateApprovalsRequiredRequest { get; internal set; }
    }

    public class UpdateEnterpriseRequest
    {
        [JsonProperty("action")]
        public string Action { get; internal set; }
        [JsonProperty("userId")]
        public string UserId { get; internal set; }
        [JsonProperty("permissions")]
        public string[] Permissions { get; internal set; }
    }
    public class UpdateApprovalsRequiredRequest
    {
        [JsonProperty("requestedApprovalsRequired")]
        public int RequestedApprovalsRequired { get; internal set; }
    }
}
