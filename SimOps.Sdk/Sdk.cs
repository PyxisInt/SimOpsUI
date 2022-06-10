using Refit;
using SimOps.Sdk.Interfaces;

namespace SimOps.Sdk
{
    public class Sdk
    {
        private readonly string _serverUrl;

        /****** Service Interfaces ******/
        public ILoginService LoginService { get; }

        /****** End of Service Interfaces******/

        public Sdk(string simOpsServer)
        {
            _serverUrl = simOpsServer;
            LoginService = RestService.For<ILoginService>(_serverUrl);
        }
    }
}