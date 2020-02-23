using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Wallet
{
    public class RequestData_GenerateWallet
    {
        [JsonProperty("label")]
        public string Label { get; internal set; }
        [JsonProperty("passphrase")]
        public string Passphrase { get; internal set; }
        [JsonProperty("userKey")]
        public string UserKey { get; internal set; }
        [JsonProperty("backupXpub")]
        public string BackupXpub { get; internal set; }
        [JsonProperty("backupXpubProvider")]
        public string BackupXpubProvider { get; internal set; }
        [JsonProperty("enterprise")]
        public string EnterpriseId { get; internal set; }
        [JsonProperty("disableTransactionNotifications")]
        public bool DisableTransactionNotifications { get; internal set; }
        [JsonProperty("passcodeEncryptionCode")]
        public string PasscodeEncryptionCode { get; internal set; }
        [JsonProperty("coldDerivationSeed")]
        public string ColdDerivationSeed { get; internal set; }
        [JsonProperty("gasPrice")]
        public int GasPrice { get; internal set; }
        [JsonProperty("disableKRSEmail")]
        public bool DisableKRSEmail { get; internal set; }
    }
}
