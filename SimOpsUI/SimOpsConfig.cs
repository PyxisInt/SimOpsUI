using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOpsUI
{
    public class SimOpsConfig
    {
        [JsonProperty("SimOps Server")]
        public string SimOpsServer { get; set; }

        [JsonProperty("VA Name")]
        public string VAName { get; set; }

        [JsonProperty("Authentication")]
        public Authentication Authentication { get; set; }
    }

    public class Authentication
    {
        [JsonProperty("Method")]
        public string Method { get; set; }

        [JsonProperty("Auth0")]
        public Auth0Config Auth0 { get; set; }

    }

    public class Auth0Config
    {
        [JsonProperty("Domain")]
        public string Domain { get; set; }

        [JsonProperty("ClientId")]
        public string ClientId { get; set; }
    }
}
