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
        private readonly IInitialiseLookups _initlInitialiseLookups;

        public Application(ILogger logger, IUtility utility, IThreading threading, IInitialiseLookups initlInitialiseLookups)
        {
            _logger = logger;
            _utility = utility;
            _threading = threading;
            _initlInitialiseLookups = initlInitialiseLookups;
        }

        public void Run()
        {
            //_initlInitialiseLookups.GeneratePostDeploymentScripts();
            _threading.ThreadLockExample();
            _utility.WaitUserInput();
        }
    }
}
