using System;

namespace Examples.DesignPatterns.Factory
{
    public class FactoryDemo : IFactoryDemo
    {
        private readonly IPersonFactory _personFactory;

        public FactoryDemo(IPersonFactory personFactory)
        {
            _personFactory = personFactory;
        }

        public void DemonstrateFactory()
        {
            foreach (var personType in Enum.GetValues(typeof(PersonType)))
            {
                Console.WriteLine(_personFactory.GetPerson((PersonType)personType).GetName());
            }
        }
    }
}