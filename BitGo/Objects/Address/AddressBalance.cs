using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Address
{
    public class AddressBalance
    {
        [JsonProperty("balance")]
        public decimal Balance { get; internal set; }
        [JsonProperty("numTx")]
        public int NumberOfTransactions { get; internal set; }
        [JsonProperty("numUnspents")]
        public int NumberOfUnspents { get; internal set; }
        [JsonProperty("totalReceived")]
        public decimal TotalReceived { get; internal set; }
        [JsonProperty("totalSent")]
        public decimal TotalSent { get; internal set; }
    }
}
