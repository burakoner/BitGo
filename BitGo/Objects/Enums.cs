using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects
{
    public enum PermissionType
    {
        Admin,
        View,
        Spend
    }

    public enum PendingApprovalState
    {
        Pending,
        AwaitingSignature,
        PendingBitGoAdminApproval,
        PendingFinalApproval,
    }

    public enum PendingApprovalResolveType
    {
        Approved,
        Rejected,
    }

    public enum KeySourceType
    {
        Backup,
        Bitgo,
        Cold,
        User
    }
}
