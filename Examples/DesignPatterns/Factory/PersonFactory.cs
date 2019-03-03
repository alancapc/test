using System;

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
                default:
                    throw new ArgumentOutOfRangeException(nameof(personType), personType, null);
            }
        }
    }
}