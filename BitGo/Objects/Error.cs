using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects
{
    public class Error
    {
        [JsonProperty("name")]
        public string Code { get; set; }
        [JsonProperty("error")]
        public string Message { get; set; }
        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        internal Error()
        {
        }
        internal Error(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        internal Error(string code, string message, string requestId)
        {
            this.Code = code;
            this.Message = message;
            this.RequestId = requestId;
        }

        public override string ToString()
        {
            return $"[{this.Code}] {this.Message}";
        }
    }

    public class ArgumentError : Error
    {
        public ArgumentError(string message) : base("Invalid Parameter", message) { }
    }
}
