using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.PendingApproval
{

    public class PendingApprovalInfoList
    {
        [JsonProperty("pendingApprovals")]
        public PendingApprovalInfo PendingApprovals { get; internal set; }
    }
}
