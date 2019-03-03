namespace Examples.DesignPatterns.Factory
{
    public interface IPersonFactory
    {
        IPerson GetPerson(PersonType personType);
    }
}