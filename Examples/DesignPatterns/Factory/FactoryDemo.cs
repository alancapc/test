using System;
using System.Collections.Generic;

namespace Examples.DesignPatterns.Factory
{
    public class FactoryDemo : IFactoryDemo
    {
        private readonly IPersonFactory _personFactory;
        private readonly List<IPerson> _persons = new List<IPerson>();

        public FactoryDemo(IPersonFactory personFactory)
        {
            _personFactory = personFactory;
        }

        public void DemonstrateFactory()
        {
            _persons.Add(_personFactory.GetPerson(PersonType.Rural));
            _persons.Add(_personFactory.GetPerson(PersonType.Urban));

            foreach (var person in _persons) Console.WriteLine(person.GetName());
        }
    }
}