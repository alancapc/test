using System;
using Examples.DesignPatterns.Factory.Persons;

namespace Examples.DesignPatterns.Factory
{
    public class PersonFactory : IPersonFactory
    {
        public IPerson GetPerson(PersonType personType)
        {
            switch (personType)
            {
                case PersonType.Rural:
                    return new Rural();
                case PersonType.Urban:
                    return new Urban();
                case PersonType.Metropolitan:
                    return new Metropolitan();
                default:
                    throw new ArgumentOutOfRangeException(nameof(personType), personType, null);
            }
        }
    }
}