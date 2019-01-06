using Examples;
using Serilog;
using test.Interfaces;
using Utilities.Interfaces;
using Examples.Threads;

namespace test
{
    public class Application : IApplication
    {
        private readonly ILogger _logger;
        private readonly IUtility _utility;
        private readonly IThreading _threading;
        private readonly IJson _json;
        private readonly IInitialiseLookups _initlInitialiseLookups;
        private readonly ICSharpEight _cSharpEight;

        public Application(ILogger logger, IUtility utility, IThreading threading, IInitialiseLookups initlInitialiseLookups, IJson json, ICSharpEight cSharpEight)
        {
            _logger = logger;
            _utility = utility;
            _threading = threading;
            _initlInitialiseLookups = initlInitialiseLookups;
            _json = json;
            _cSharpEight = cSharpEight;
        }

        public void Run()
        {
            //_initlInitialiseLookups.GeneratePostDeploymentScripts();

            _cSharpEight.NullableReferenceTypes();
            _utility.WaitUserInput();
        }
    }
}
