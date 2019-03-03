using Examples.DesignPatterns.Factory;
using test.Interfaces;
using Utilities.Interfaces;

namespace test
{
    public class Application : IApplication
    {
        private readonly IUtility _utility;
        private readonly IFactoryDemo _factoryDemo;

        public Application(IUtility utility, IFactoryDemo factoryDemo)
        {
            _utility = utility;
            _factoryDemo = factoryDemo;
        }

        public void Run()
        {
            _factoryDemo.DemonstrateFactory();

            _utility.WaitUserInput();
        }
    }
}