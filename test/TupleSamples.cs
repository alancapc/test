namespace test
{
    internal class TupleSamples
    {
        public (string name, string title, long year) GetNewTuple() => (
            name: "Alan Costa", 
            title: "Tuple Sample", 
            year: 2017);
    }
}