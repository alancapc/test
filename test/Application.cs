using Examples;
using Examples.Json;

namespace test
{
    using Serilog;
    using Interfaces;
    using Utilities.Interfaces;
    using Examples.Threads;

    public class Application : IApplication
    {
        private readonly ILogger _logger;
        private readonly IUtility _utility;
        private readonly IThreading _threading;
        private readonly IJson _json;
        private readonly IInitialiseLookups _initlInitialiseLookups;

        public Application(ILogger logger, IUtility utility, IThreading threading, IInitialiseLookups initlInitialiseLookups, IJson json)
        {
            _logger = logger;
            _utility = utility;
            _threading = threading;
            _initlInitialiseLookups = initlInitialiseLookups;
            _json = json;
        }

        public void Run()
        {
            //_initlInitialiseLookups.GeneratePostDeploymentScripts();

            _json.DeserialiseJson();
            _utility.WaitUserInput();
        }
    }
}
