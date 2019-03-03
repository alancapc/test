using Examples.Json;
using test.Interfaces;
using Utilities.Interfaces;

namespace test
{
    public class Application : IApplication
    {
        private readonly IJson _json;
        private readonly IUtility _utility;

        public Application(IUtility utility, IJson json)
        {
            _utility = utility;
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