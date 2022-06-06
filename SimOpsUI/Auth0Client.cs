using Auth0.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimOpsUI
{
    public class Auth0Client : Auth0ClientBase
    {
        public Auth0Client(Auth0ClientOptions options)
            : base(options, "winforms")
        {
            options.Browser = options.Browser ?? new WebViewBrowserChromium();
        }
    }
}
