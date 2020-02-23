using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitGo.Objects.Enterprise
{
    public class RequestData_EnterpriseAddUser
    {
        /// <summary>
        /// Value:"admin"
        /// </summary>
        [JsonProperty("permission")]
        public string Permission { get; internal set; }

        /// <summary>
        /// The Username of the User that should be added to the Enterprise
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; internal set; }

        [JsonProperty("usernames")]
        public string[] Usernames { get; internal set; }
    }
}
