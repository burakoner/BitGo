using BitGo.Objects.Address;
using BitGo.Objects.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Wallet
{
    public class WalletInfo
    {
        [JsonProperty("id")]
        public string WalletId { get; internal set; }
        [JsonProperty("users")]
        public WalletUser[] Users { get; internal set; }
        [JsonProperty("coin")]
        public string Coin { get; internal set; }
        [JsonProperty("label")]
        public string Label { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("enterprise")]
        public string Enterprise { get; internal set; }
        [JsonProperty("m")]
        public int M { get; internal set; }
        [JsonProperty("n")]
        public int N { get; internal set; }
        [JsonProperty("keys")]
        public string[] Keys { get; internal set; }
        // [JsonProperty("keySignatures")]
        // public KeySignatures KeySignatures { get; internal set; }
        [JsonProperty("tags")]
        public string[] Tags { get; internal set; }
        [JsonProperty("disableTransactionNotifications")]
        public bool DisableTransactionNotifications { get; internal set; }
        [JsonProperty("freeze")]
        public Freeze Freeze { get; internal set; }
        [JsonProperty("deleted")]
        public bool Deleted { get; internal set; }
        [JsonProperty("approvalsRequired")]
        public int ApprovalsRequired { get; internal set; }
        [JsonProperty("isCold")]
        public bool IsCold { get; internal set; }
        [JsonProperty("coinSpecific")]
        public WalletCoinSpecific CoinSpecific { get; internal set; }
        [JsonProperty("admin")]
        public Admin Admin { get; internal set; }
         [JsonProperty("clientFlags")]
         public string[] ClientFlags { get; internal set; }
        [JsonProperty("allowBackupKeySigning")]
        public bool AllowBackupKeySigning { get; internal set; }
        [JsonProperty("recoverable")]
        public bool Recoverable { get; internal set; }
        [JsonProperty("balance")]
        public decimal Balance { get; internal set; }
        [JsonProperty("confirmedBalance")]
        public decimal ConfirmedBalance { get; internal set; }
        [JsonProperty("spendableBalance")]
        public decimal SpendableBalance { get; internal set; }
        [JsonProperty("balanceString")]
        public string BalanceString { get; internal set; }
        [JsonProperty("confirmedBalanceString")]
        public string ConfirmedBalanceString { get; internal set; }
        [JsonProperty("spendableBalanceString")]
        public string SpendableBalanceString { get; internal set; }
        [JsonProperty("startDate")]
        public DateTime StartDate { get; internal set; }
        [JsonProperty("receiveAddress")]
        public AddressInfo ReceiveAddress { get; internal set; }
        // [JsonProperty("pendingApprovals")]
        // public object[] PendingApprovals { get; internal set; }
    }
}
